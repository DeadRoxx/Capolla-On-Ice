using UnityEngine;
using System.Collections;

public class Player2Buttons : ButtonClass
{
	void Start () 
	{
		base.StartPlayer2();
	}

	public override bool GetDashButtonDown ()
	{
		if( Input.GetButtonDown("J2Dash") )
		{
			return true;
		}

		return false;
	}

	public override float GetHorizontalInput ()
	{
		float value = Input.GetAxis("J2Horizontal");
		return value;
	}

	public override float GetVerticalInput ()
	{
		float value = Input.GetAxis("J2Vertical");
		return value;
	}
}
