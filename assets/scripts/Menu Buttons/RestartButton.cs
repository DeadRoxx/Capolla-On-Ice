using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {
	void OnGUI()
	{
		if(GUI.Button( new Rect(10, 10, 100, 20), "Restart!"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
