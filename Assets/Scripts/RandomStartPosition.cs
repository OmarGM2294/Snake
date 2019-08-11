using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.changePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changePosition()
    {
        Vector2 position = new Vector2(Random.Range(-9, 9), Random.Range(-4, 4));
        while(this.verifyPosition(position))
        {
            position = new Vector2(Random.Range(-9, 9), Random.Range(-4, 4));
        }
        transform.position = position;
    }

    bool verifyPosition(Vector2 position)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        bool flag = false;
        foreach (GameObject player in players)
        {
            if (player.transform.position.x == position.x && player.transform.position.y == position.y)
            {
                flag = true;
            }
        }
        return flag;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.name == "Snake")
        {
            this.changePosition();
        }
    }
}
