using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 currentDirection;

    [SerializeField]
    private float speed;
    private Vector2 direction;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private HeartController heartController;
    private bool tookDamage = false;
    private bool isAttacking = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Vector2.zero;
        heartController = GetComponent<HeartController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        //transform.Translate(direcao * vel * Time.deltaTime);

        UpdateAnimation(direction);
        UpdateSprite();
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * speed * Time.deltaTime);
    }

    void UpdateSprite()
    {
        if (tookDamage)
        {
            PingPongColor(8);
        } else if (heartController.isInDangeState)
        {
            PingPongColor(1);
        } else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void PingPongColor(int x)
    {
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(x * Time.time, 0.5f));
    }

    void GetInputs()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
            currentDirection = direction;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
            currentDirection = direction;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
            currentDirection = direction;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
            currentDirection = direction;
        }

        if (Input.GetKeyDown(KeyCode.Space))
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

    public IEnumerator KnockBack(float duracao, float poder, Vector2 direcao)
    {
        float tempo = 0;

        while (duracao > tempo)
        {
            tempo += Time.deltaTime;
            rigidbody.AddForce(new Vector2(direcao.x * -poder, direcao.y * -poder), ForceMode2D.Force);
        }

        yield return 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!tookDamage)
        {
            if (collision.gameObject.CompareTag("EnemyAttack"))
            {
                Debug.Log("enemyattack");
                StartCoroutine(KnockBack(1f, 100, currentDirection));
                heartController.TakeDamage(-1);

                DanoCor();
            }
        }
    }

    void DanoCor()
    {
        tookDamage = true;
        StartCoroutine(LiberaCor());
    }

    IEnumerator LiberaCor()
    {
        yield return new WaitForSeconds(1f);
        tookDamage = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
