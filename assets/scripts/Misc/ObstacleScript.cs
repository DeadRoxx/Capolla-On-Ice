using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleScript : MonoBehaviour 
{
	Collider2D _circleCollider;
	List<PlayerScript> playerScripts;

	void Start()
	{
		OnEnable();
	}

	void OnEnable()
	{
		_circleCollider = gameObject.GetComponent<Collider2D>();
		
		GameObject[] playersGO = GameObject.FindGameObjectsWithTag("Player");
		
		playerScripts = new List<PlayerScript>();
		
		foreach(GameObject player in playersGO)
		{
			playerScripts.Add(player.GetComponent<PlayerScript>());
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
			OnPlayerEnter();
	}

	protected virtual void OnPlayerEnter()
	{
		foreach(PlayerScript playerScript in playerScripts)
		{
			if(playerScript != null)
			{
				if(_circleCollider.bounds.Contains(playerScript._footCollider.bounds.max) &&
				   _circleCollider.bounds.Contains(playerScript._footCollider.bounds.min) ) 
				{
					playerScript.OnGameOver();
					break;
				}
			}
		}
	}
}
