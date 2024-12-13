using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Enemy enemy;

    public void intializeEnemy(Enemy enemyInstance)
    {
        enemy = enemyInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            enemy.Update();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.name == "Player1")
            {
                Player1Movement player = collision.gameObject.GetComponent<Player1Movement>();
                if (player != null)
                {
                    enemy.isTouching = true;
                    player.OnTouchingEnemy(collision.gameObject, enemy.damage);
                }
            }
            else if (collision.gameObject.name == "Player2")
            {
                Player2Movement player = collision.gameObject.GetComponent<Player2Movement>();
                if (player != null)
                {
                    enemy.isTouching = true;
                    player.OnTouchingEnemy(collision.gameObject, enemy.damage);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.name == "Player1")
            {
                Player1Movement player = collision.gameObject.GetComponent<Player1Movement>();
                if (player != null)
                {
                    enemy.isTouching = false;
                    player.OnExitTouchingEnemy();
                }
            }
            else if (collision.gameObject.name == "Player2")
            {
                Player2Movement player = collision.gameObject.GetComponent<Player2Movement>();
                if (player != null)
                {
                    enemy.isTouching = false;
                    player.OnExitTouchingEnemy();
                }
            }
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }

    
}
