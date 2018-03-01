using UnityEngine;
using System.Collections;

public class LeftButtonScript : MonoBehaviour {

	public Sprite normal;
	public Sprite hover;
	public SpriteRenderer spriteRenderer;

	void OnMouseDown(){
		transform.GetComponentInParent<Selection>().MoveVectorBackwards();
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
