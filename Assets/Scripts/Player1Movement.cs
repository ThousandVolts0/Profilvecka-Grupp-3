using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Player1Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [Range (0, 1000)] public float speed = 10f;
    private Rigidbody2D Rigidbody;
    private bool isGrounded;
    private int maxHealth = 100;
    private int health;
    float moveInputH;
    float moveInputV;

    public GameObject healthDisplay;
    public GameObject healthText;
    private bool isTouchingEnemy;
    private bool isDead;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    private void Update()
    {
        if (moveInputH < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (moveInputH > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveInputH = Input.GetAxis("KeysOnlyH"); // Custom input manager definied under project settings.
        moveInputV = Input.GetAxis("KeysOnlyV"); // Custom input manager definied under project settings.

        Rigidbody.velocity = new Vector2(moveInputH * speed, moveInputV * speed);
    }

    bool isCooldown = false;

    public void takeDamage(int damage)
    {
        health -= damage;
        TextMeshProUGUI text = healthText.GetComponent<TextMeshProUGUI>();
        text.text = "Health: " + health.ToString();

        if (health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    public IEnumerator checkDamage(int damage)
    {
        while (isTouchingEnemy && !isDead)
        {
            if (!isCooldown)
            {
                takeDamage(damage);
                isCooldown = true;
                yield return new WaitForSeconds(0.25f); // Cooldown delay
                isCooldown = false;
            }
            yield return null; // Avoid blocking the main thread
        }
    }

    public void OnTouchingEnemy(GameObject playerObj, int damage)
    {
        if (!isTouchingEnemy) // Start coroutine only when first touching
        {
            isTouchingEnemy = true;
            StartCoroutine(checkDamage(damage)); // Start the coroutine
        }
    }

    public void OnExitTouchingEnemy()
    {
        isTouchingEnemy = false;
    }


}
