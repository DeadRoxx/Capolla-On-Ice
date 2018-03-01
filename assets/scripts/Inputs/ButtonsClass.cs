using UnityEngine;
using System.Collections;

public class ButtonClass : MonoBehaviour 
{
	public KeyCode leftKeyCode;
	public KeyCode rightKeyCode;
	public KeyCode upKeyCode;
	public KeyCode downKeyCode;

	public KeyCode dashKeyCode;

	public void StartPlayer1()
	{
		leftKeyCode = KeyCode.A;
		rightKeyCode = KeyCode.D;
		upKeyCode = KeyCode.W;
		downKeyCode = KeyCode.S;
		dashKeyCode = KeyCode.Space;
	}

	public void StartPlayer2()
	{
		leftKeyCode = KeyCode.LeftArrow;
		rightKeyCode = KeyCode.RightArrow;
		upKeyCode = KeyCode.UpArrow;
		downKeyCode = KeyCode.DownArrow;
		dashKeyCode = KeyCode.RightControl;
	}

	public virtual float GetHorizontalInput(){ return 0.0f; }
	public virtual float GetVerticalInput(){ return 0.0f; }
	public virtual bool GetDashButtonDown(){ return false; }
}
