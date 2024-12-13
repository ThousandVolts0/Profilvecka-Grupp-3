using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHitbox : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided GameObject is on the right layer (e.g., layer 3)
        if (collision.gameObject.layer == 3)
        {
            // Get the Enemy component from the GameObject
            EnemyBehaviour enemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            Enemy enemy = enemyScript.enemy;

            enemy.takeDamage(damage);
            
        }
    }
}
