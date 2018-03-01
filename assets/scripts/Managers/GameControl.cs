using UnityEngine;
using System.Collections;
using System;
using System.IO; //responsavel por abrir e fechar arquivos
using System.Runtime.Serialization.Formatters.Binary; //Responsavel por incripitar

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;

	public int player1Wins;
	public int player2Wins;

	public int player1Number = 1;
	public int player2Number = 1;

	public bool player1HaveChoiced;
	public bool player2HaveChoiced;

	void Awake()
	{
		if(instance == null)
		{
			DontDestroyOnLoad ( this.gameObject );
			instance = this;
		}
		else if(instance != this)
		{
			Destroy( this.gameObject );
		}
	}

	public void Player2Choiced(int choiceNumber)
	{
		player2HaveChoiced = true;
		player2Number = choiceNumber;

		if(player1HaveChoiced)
			Application.LoadLevel("Game");
	}

	public void Player1Choiced(int choiceNumber)
	{
		player1HaveChoiced = true;
		player1Number = choiceNumber;

		if(player2HaveChoiced)
			Application.LoadLevel("Game");
	}
}


