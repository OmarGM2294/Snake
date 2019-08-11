using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SnakeMovement : MonoBehaviour
{
    public GameObject TailPrefab;
    private Vector2 previousPosition;
    private Vector2 direction = Vector2.right;
    private List<Transform> tails = new List<Transform>();
    public float speed = 1.0f;

    private bool flagUpdated = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flagUpdated)
        {
            InvokeRepeating("Move", speed, speed);
            flagUpdated = false;
        }
        if (Input.GetAxis("Horizontal") < 0) {
            if (tails.Count == 0 || (tails.Count > 0 && direction != Vector2.right))
            {
                direction = Vector2.left;
            }
        } else if (Input.GetAxis("Horizontal") > 0) {
            if (tails.Count == 0 || (tails.Count > 0 && direction != Vector2.left))
            {
                direction = Vector2.right;
            }
        } else if (Input.GetAxis("Vertical") < 0) {
            if (tails.Count == 0 || (tails.Count > 0 && direction != Vector2.up))
            {
                direction = Vector2.down;
            }
        } else if (Input.GetAxis("Vertical") > 0) {
            if (tails.Count == 0 || (tails.Count > 0 && direction != Vector2.down))
            {
                direction = Vector2.up;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.name == "Point") {
            this.grow();
        } else {
            bool option = EditorUtility.DisplayDialog("Game Over",
            "Start again",
            "Ok",
            "Close");
            if (option) {
                SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            }
        }
    }

    void grow()
    {
        GameObject g = (GameObject)Instantiate(TailPrefab, previousPosition, Quaternion.identity);
        tails.Insert(0, g.transform);

        if (tails.Count % 2 == 0)
        {
            if (speed > 0.1f)
            {
                speed -= 0.1f;
                flagUpdated = true;
                CancelInvoke();
            }
        }
    }

    void Move()
    {
        previousPosition = transform.position;
        transform.Translate(direction);
        
        if (tails.Count > 0) {
            tails[tails.Count-1].position = previousPosition;
            tails.Insert(0, tails[tails.Count-1]);
            tails.RemoveAt(tails.Count-1);
        }
    }
}
