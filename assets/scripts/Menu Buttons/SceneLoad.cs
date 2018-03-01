using UnityEngine;
using System.Collections;

public class SceneLoad : MonoBehaviour 
{
	public string sceneToLoad;

	void OnMouseDown()
	{
		Application.LoadLevel(sceneToLoad);
	}
}
