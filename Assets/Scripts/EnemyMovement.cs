using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player1;
    public Transform Player2;

    private Vector2 player1Pos;
    private Vector2 player2Pos;

    public float speed = 0.5f;
    private Vector3 velocity = new Vector3 (0, 0, 0);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player1Pos = Player1.position;
        player2Pos = Player2.position;

        float enemyDistanceP1 = Vector3.Distance(player1Pos, transform.position);
        float enemyDistanceP2 = Vector3.Distance(player2Pos, transform.position);

        Vector3 targetPos = (enemyDistanceP1 < enemyDistanceP2) ? player1Pos : player2Pos;

        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, speed);

        Vector3 direction = (smoothPos - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

    }
}
