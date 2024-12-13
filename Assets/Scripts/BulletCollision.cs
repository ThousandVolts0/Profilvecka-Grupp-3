using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("A");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        if (collision.gameObject.layer == 3)
        {
            EnemyBehaviour enemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            Enemy enemy = enemyScript.enemy;
            if (enemyScript != null && enemy != null)
            {
                Debug.Log("Test2");
                enemy.takeDamage(damage);
            }
            else
            {
                Debug.Log("Enemy not found?");
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
