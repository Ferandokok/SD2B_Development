using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    public int HP = 2;
    bool isAlive;

    public float dirX = 1f;
    
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.Translate(transform.right * dirX * speed * Time.deltaTime);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * dirX, 0.6f);

            Debug.DrawRay(transform.position, transform.right * 0.6f * dirX, Color.blue);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Wall")
                {
                    dirX *= -1f;
                }
            }
        }
    }
    
}
