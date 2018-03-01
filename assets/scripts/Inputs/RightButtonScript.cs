using UnityEngine;
using System.Collections;

public class RightButtonScript : MonoBehaviour {

	public Sprite normal;
	public Sprite hover;
	public SpriteRenderer spriteRenderer;
	
	void OnMouseDown()
	{
		transform.GetComponentInParent<Selection>().MoveVectorForwards();
	}

	void OnMouseEnter()
	{
		spriteRenderer.sprite = hover;
	}
	void OnMouseExit()
	{
		spriteRenderer.sprite = normal;
	}
}
