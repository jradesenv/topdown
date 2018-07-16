using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 direction;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool isAttacking = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateAnimation(direction);
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * speed * Time.deltaTime);        
    }

    void GetInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = Vector2.zero;

        if (y > 0)
        {
            direction += Vector2.up;
        } else if (y < 0)
        {
            direction += Vector2.down;
        }

        if (x > 0)
        {
            direction += Vector2.right;
        } else if (x < 0)
        {
            direction += Vector2.left;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
        } else
        {
            isAttacking = false;
        }
    }

    void UpdateAnimation(Vector2 dir)
    {
        if (direction.x != 0 || direction.y != 0)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);

        if (isAttacking)
        {
            animator.SetTrigger("attack");
        }
    }
}
