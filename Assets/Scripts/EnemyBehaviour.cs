using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Enemy enemy;

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
}
