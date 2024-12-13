using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform p1;
    public GameObject bulletSmall;
    public GameObject bulletBig;
    public GameObject bulletLarge;

    public float bulletSpeed = 10f;
    public int bulletDamage = 30;
    public float gunCooldown = 2f;
    private bool hasCooldown = false;

    private AudioSource shootSound;
    void Start()
    {
        shootSound = GetComponent<AudioSource>();
    }

    private IEnumerator spawnBullet()
    {
        if (!hasCooldown)
        {
            hasCooldown = true;


            shootSound.Play();
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

            GameObject activeBullet = Instantiate(bulletSmall, transform.position, Quaternion.identity);

            BulletCollision bulletScript = activeBullet.AddComponent<BulletCollision>();
            bulletScript.damage = bulletDamage;

            Vector3 direction = (targetPos - transform.position).normalized;

            float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            activeBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            Rigidbody2D bulletRb = activeBullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = direction * bulletSpeed;
            }

            yield return new WaitForSeconds(gunCooldown);

            hasCooldown = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(spawnBullet());
        }
    }
}
