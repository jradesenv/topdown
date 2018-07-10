using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private float speed;
    private Vector2 direction;
    private Animator animator;
    private Vector2 currentDirection;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private bool tookDamage = false;
    [SerializeField]
    private bool isInDangeState = false;
    private bool isAttacking = false;
    private Transform player;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Vector2.zero;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        //transform.Translate(direcao * vel * Time.deltaTime);

        UpdateAnimation(direction);
        UpdateSprite();

        //Vector2 velocity = new Vector2((transform.position.x - player.position.x) * speed * Time.deltaTime, (transform.position.y - player.position.y) * speed * Time.deltaTime);
        //rigidbody.velocity = -velocity;


        rigidbody.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position,
                                                  speed * Time.deltaTime));
    }

    void GetInputs()
    {
        //TODO get direction of player
        direction = Vector2.zero;
        currentDirection = Vector2.down;
    }

    void FixedUpdate()
    {
        //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        
        
    }

    void UpdateSprite()
    {
        if (tookDamage)
        {
            PingPongColor(8);
        }

        if (isInDangeState)
        {
            PingPongColor(1);
        }
    }

    void PingPongColor(int x)
    {
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(x * Time.time, 0.5f));
    }

    void UpdateAnimation(Vector2 dir)
    {
        //if (direction.x != 0 || direction.y != 0)
        //{
        //    animator.SetLayerWeight(1, 1);
        //}
        //else
        //{
        //    animator.SetLayerWeight(1, 0);
        //}

        //animator.SetFloat("x", dir.x);
        //animator.SetFloat("y", dir.y);

        //if (isAttacking)
        //{
        //    animator.SetTrigger("attack");
        //}
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
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            PlayerController player = collision.GetComponentInParent<PlayerController>();
            currentDirection = player.currentDirection * -1;

            StartCoroutine(KnockBack(1f, 200, currentDirection));
            DanoCor();
        }
    }

    void DanoCor()
    {
        tookDamage = true;
        StartCoroutine(LiberaCor());
    }

    IEnumerator LiberaCor()
    {
        yield return new WaitForSeconds(0.5f);
        tookDamage = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
