using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public int health = 10;
    public int ammo = 10;

    public float speed = 5f;
    public float jumpPower = 5f;

    public GameObject projectile;
    public Vector2 projectileOffset = new Vector2(0f, 0f);

    public AudioSource jumpSFX;

    Animator animator;

    public Vector2 inputDirection;

    private bool isJumping = false;
    private bool facingLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Player Jump
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpSFX.Play();
        }

        //Shooting
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(ShootProjectile(projectile));
        }
    }
    void FixedUpdate()
    {
        //Player movement (left/right)
        inputDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        rb.velocity = new Vector2(inputDirection.x * speed, rb.velocity.y);

        //Animation for Walking
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Is_Walking", true);
        }
        else
        {
            animator.SetBool("Is_Walking", false);
        }

        //Changes animation based on direction
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Left", true);
            facingLeft = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Left", false);
            facingLeft = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }

        if (other.gameObject.tag == "Enemy")
        {
            health--;
        }
    }

    public IEnumerator ShootProjectile(GameObject projectile)
    {
        if (ammo > 0)
        {
            ammo--;

            if (facingLeft)
            {
                Instantiate(projectile, (Vector2)transform.position + projectileOffset, Quaternion.Euler(0, 0, 180));
                animator.SetTrigger("Shoot");
                yield return null;
            }
            else
            {
                Instantiate(projectile, (Vector2)transform.position + projectileOffset, Quaternion.identity);
                animator.SetTrigger("Shoot");
                yield return null;
            }
        }
    }

}
