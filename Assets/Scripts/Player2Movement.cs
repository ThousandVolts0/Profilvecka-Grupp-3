using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player2Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(0, 1000)] public float speed = 10f;
    private Rigidbody2D Rigidbody;
    private Animator animator;
    public GameObject slashR;
    public GameObject slashL;
    private Animator slashAnimatorR;
    private Animator slashAnimatorL;
    public GameObject hitboxPrefab;
    string dir = "left";

    private int maxHealth = 100;
    private int health;

    public GameObject healthDisplay;
    public GameObject healthText;
    private bool isTouchingEnemy;
    private bool isDead;

    AudioSource footstep1;
    AudioSource footstep2;
    AudioSource footstep3;
    AudioSource footstep4;

    public GameObject footstep1Obj;
    public GameObject footstep2Obj;
    public GameObject footstep3Obj;
    public GameObject footstep4Obj;

    float moveInputH;
    float moveInputV;

    float footstepTimer = 0f;
    float footstepInterval = 0.5f;

    public float attackCooldown = 1f;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    AudioSource attackSound;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slashAnimatorR = slashR.GetComponent<Animator>();
        slashAnimatorL = slashL.GetComponent<Animator>();

        footstep1 = footstep1Obj.GetComponent<AudioSource>();
        footstep2 = footstep2Obj.GetComponent<AudioSource>();
        footstep3 = footstep3Obj.GetComponent<AudioSource>();
        footstep4 = footstep4Obj.GetComponent<AudioSource>();

        attackSound = GetComponent<AudioSource>();
        health = maxHealth;
    }

    private void playRandomFootstep()
    {
        int randomIndex = Random.Range(1, 4);
        switch (randomIndex)
        {
            case 1:
                footstep1.Play();
                break;

            case 2:
                footstep2.Play();
                break;

            case 3:
                footstep3.Play();
                break;

            case 4:
                footstep4.Play();
                break;
        }
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveInputH = Input.GetAxis("ArrowsOnlyH"); // Custom input manager definied under project settings.
        moveInputV = Input.GetAxis("ArrowsOnlyV"); // Custom input manager definied under project settings.

        Rigidbody.velocity = new Vector2(moveInputH * speed, moveInputV * speed);
    }

    private IEnumerator showSlash()
    {
        if (dir == "left")
        {
            yield return new WaitForSeconds(0.4f);
            slashL.SetActive(true);
            attackSound.Play();
            slashAnimatorL.SetTrigger("doSlash");
            yield return new WaitForSeconds(slashAnimatorL.GetCurrentAnimatorStateInfo(0).length - 0.45f);
            slashL.SetActive(false);
            slashAnimatorL.SetTrigger("Default");
        }
        else if (dir == "right")
        {
            yield return new WaitForSeconds(0.4f);
            slashR.SetActive(true);
            attackSound.Play();
            slashAnimatorR.SetTrigger("doSlash");
            yield return new WaitForSeconds(slashAnimatorR.GetCurrentAnimatorStateInfo(0).length - 0.45f);
            slashR.SetActive(false);
            slashAnimatorR.SetTrigger("Default");
        }
        
    }

    private IEnumerator playAnim(string name)
    {
        
        animator.SetBool("isAttacking", true);
        animator.Play(name);
        StartCoroutine(showSlash());

        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        checkState();
        attackTimer = attackCooldown;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator spawnHitbox()
    {
        Vector2 pos = new Vector2(0, 0);
        yield return new WaitForSeconds(0.25f);
        if (dir == "right")
        {
            pos = new Vector2(slashR.transform.position.x + 1f, slashR.transform.position.y);

        }
        else if (dir == "left")
        {
            pos = new Vector2(slashL.transform.position.x - 1f, slashL.transform.position.y);
        }
        GameObject obj = Instantiate(hitboxPrefab, pos, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(obj);
    }

    private void Update()
    {
        if (attackTimer <= 0f && isAttacking == false)
        {
            foreach (char character in Input.inputString)
            {
                if (character == 'ä')
                {
                    StartCoroutine(playAnim("MantisAttack"));
                    
                    StartCoroutine(spawnHitbox());
                    isAttacking = true;

                }
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
        
        if (moveInputH != 0 || moveInputV != 0)
        {
            if (footstepTimer <= 0f)
            {
                playRandomFootstep();
                footstepTimer = footstepInterval;
            }
        }

        footstepTimer -= Time.deltaTime;

        if (animator.GetBool("isAttacking") == false)
        {
            checkState();
            if (moveInputH > 0)
            {
                dir = "right";
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                
            }
            else if (moveInputH < 0)
            {
                dir = "left";
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void checkState()
    {
            if (moveInputV != 0 || moveInputH != 0)
            {
                animator.SetTrigger("MantisWalk");
            }
            else
            {
                animator.SetTrigger("MantisIdle");
            }
        
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
