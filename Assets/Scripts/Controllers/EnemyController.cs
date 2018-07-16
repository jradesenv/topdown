using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rigidbody;
    private Transform player;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position,
                                                  speed * Time.deltaTime));
    }
}
