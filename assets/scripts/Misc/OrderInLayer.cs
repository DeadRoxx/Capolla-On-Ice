using UnityEngine;
using System.Collections;

public class OrderInLayer : MonoBehaviour
{
    CircleCollider2D circleCollider;
	[HideInInspector]
	public SpriteRenderer spriteRenderer;
	[HideInInspector]
    public float deltaHeight;

    void Start()
    {
        circleCollider = gameObject.GetComponentInParent<CircleCollider2D>();

		spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

		if(spriteRenderer == null)
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        deltaHeight = circleCollider.bounds.size.y/2;
    }

    void Update()
    {
        spriteRenderer.sortingOrder = -(int)Camera.main.WorldToScreenPoint(transform.position - Vector3.up * deltaHeight).y;
    }
}
