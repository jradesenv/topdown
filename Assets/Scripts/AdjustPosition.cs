using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPosition : MonoBehaviour {

    public bool mustUpdate;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        adjustPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (mustUpdate)
        {
            adjustPosition();
        }
    }

    void adjustPosition()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "AdjustPositionLayer";
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}
