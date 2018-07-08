using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float vel;
    private Vector2 direcao;
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        InputPersonagem();
        transform.Translate(direcao * vel * Time.deltaTime);

        Animacao(direcao);
	}

    void InputPersonagem()
    {
        direcao = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direcao += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direcao += Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direcao += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direcao += Vector2.right;
        }
    }

    void Animacao(Vector2 dir)
    {
        if (direcao.x != 0 || direcao.y != 0)
        {
            anim.SetLayerWeight(1, 1);
        } else
        {
            anim.SetLayerWeight(1, 0);
        }
        

        anim.SetFloat("x", dir.x);
        anim.SetFloat("y", dir.y);
    }
}
