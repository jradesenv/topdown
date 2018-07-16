using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableController : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
    private bool tookDamage = false;
    private bool isInDangeState = false;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();

        DamageController[] damageControllers = GetComponentsInChildren<DamageController>();
        foreach (DamageController damageController in damageControllers)
        {
            Physics2D.IgnoreCollision(damageController.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateSprite();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageController damageController = collision.gameObject.GetComponent<DamageController>();
        if (damageController != null)
        {            
            if (!tookDamage)
            {
                TakeDamage(damageController);
            }
        }
    }

    void UpdateSprite()
    {
        if (tookDamage)
        {
            PingPongColor(8);
        } else if (isInDangeState)
        {
            PingPongColor(1);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void TakeDamage(DamageController damageController)
    {
        Vector2 damageDirection = (body.transform.position - damageController.transform.position).normalized;
        StartCoroutine(KnockBack(2f, 200, damageDirection * -1));
        StartDamagedEffect();
    }

    public IEnumerator KnockBack(float duracao, float poder, Vector2 direcao)
    {
        float tempo = 0;

        while (duracao > tempo)
        {
            tempo += Time.deltaTime;
            body.AddForce(new Vector2(direcao.x * -poder, direcao.y * -poder), ForceMode2D.Force);
        }

        yield return 0;
    }

    void StartDamagedEffect()
    {
        tookDamage = true;
        StartCoroutine(StopDamagedEffect());
    }

    IEnumerator StopDamagedEffect()
    {
        yield return new WaitForSeconds(0.5f);
        tookDamage = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void PingPongColor(int x)
    {
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(x * Time.time, 0.5f));
    }
}
