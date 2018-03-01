using UnityEngine;
using System.Collections;

public class Player1Buttons : ButtonClass 
{
	void Start () 
	{
		base.StartPlayer1 ();
	}

	public override bool GetDashButtonDown ()
	{
        if (Input.GetButtonDown("J1Dash"))
        {
            return true;
        }
		
		return false;
	}
	
	public override float GetHorizontalInput ()
	{
        float value = Input.GetAxis("J1Horizontal");
		return value;
	}
	
	public override float GetVerticalInput ()
	{
        float value = Input.GetAxis("J1Vertical");
		return value;
	}
}
