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
    float gunSpeed = 5f;

    public GameObject healthDisplay;
    public Camera mouseCam;
    public GameObject bulletSmall;
    public GameObject bulletBig;
    public GameObject bulletLarge;
    public GameObject gunArm;
    public GameObject healthText;
    private bool isTouchingEnemy;
    private bool isDead;
    private AudioSource buzzSound;


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        health = maxHealth;
        buzzSound = GetComponent<AudioSource>();
    }

    private void spawnBullet()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mouseCam.transform.position.z;
        Vector3 targetPos = mouseCam.ScreenToWorldPoint(mousePos);
        GameObject activeBullet = Instantiate(bulletSmall, gunArm.transform.position, Quaternion.identity);

        Debug.DrawLine(gunArm.transform.position, targetPos, Color.red, 2f);



        Vector3 direction = (targetPos - gunArm.transform.position).normalized;

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        activeBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        Rigidbody2D bulletRb = activeBullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * gunSpeed;
        }
    }

    private void Update()
    {
        bool isMoving = moveInputH != 0 || moveInputV != 0;

        if (Input.GetMouseButtonDown(0))
        {
            spawnBullet();
        }

        if (isMoving)
        {
            if (!buzzSound.isPlaying)
            {
                buzzSound.Play();
            }

            if (moveInputH < 0)
            {

                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gunArm.GetComponent<SpriteRenderer>().flipX = true;
                gunArm.transform.localPosition = new Vector2(-0.167f, gunArm.transform.localPosition.y);

            }
            else if (moveInputH > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gunArm.GetComponent<SpriteRenderer>().flipX = false;
                gunArm.transform.localPosition = new Vector2(0.167f, gunArm.transform.localPosition.y);

            }
        }
        else
        {
            if (buzzSound.isPlaying)
            {
                {
                    buzzSound.Stop();
                }
            }
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
