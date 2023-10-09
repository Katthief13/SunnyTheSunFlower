using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 5f;
    public float jumpPower = 5f;


    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Player Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        }
    }
    void FixedUpdate()
    {
        //Player movement (left/right)
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Left", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
