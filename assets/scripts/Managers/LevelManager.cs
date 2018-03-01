using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform[] fasesPefab;

    public bool roundEnded;
    int roundCount = 0;

    public float roundTime = 120;
    float currentRoundTime;

    public float waitTime = 1.0f;
    private float currentWaitTime = 0.0f;

    public GUIText timerText;
    public GUIText player1Score;
    public GUIText player2Score;
    public GUIText winText;

    public GameObject[] Meshes;
    public GameObject[] players;
    public GameObject[] levelObjects;

    public Sprite spritePlayer1Wins;
    public Sprite spritePlayer2Wins;
    public Sprite spriteDraw;

    public SpriteRenderer winRenderer;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Existem Multiplas Instancia dos Level Manager");
        }

        instance = this;
    }
    
	void Start()
	{

		GameObject player1Mesh = Instantiate( Meshes[GameControl.instance.player1Number] ,players[0].transform.position, Quaternion.identity) as GameObject;
        player1Mesh.transform.parent = players[0].transform;
		player1Mesh.transform.localPosition = Vector2.zero;
		player1Mesh.AddComponent<OrderInLayer>();
        players[0].AddComponent<Player1Buttons>();
        players[0].AddComponent<PlayerScript>();
        players[0].transform.position = new Vector3(-1, -0.9f, 0);

        GameObject player2Mesh = Instantiate(Meshes[GameControl.instance.player2Number], players[1].transform.position, Quaternion.identity) as GameObject;
		player2Mesh.transform.parent = players[1].transform;
		player2Mesh.transform.localPosition = Vector2.zero;
		player2Mesh.AddComponent<OrderInLayer>();
		players[1].AddComponent<Player2Buttons>();
		players[1].AddComponent<PlayerScript>();
		players[1].transform.position = new Vector3(1, 1, 0);
	
		//START!!!
        players = GameObject.FindGameObjectsWithTag("Player");
        levelObjects = GameObject.FindGameObjectsWithTag("ArenaBorder");

        RandomizeNewFase();
	}


    void Update()
    {
        if (!roundEnded)
        {
            currentRoundTime += Time.deltaTime;

            //Getting Minutes and Seconds
            float minutes = roundTime / 60 - currentRoundTime / 60;
            float seconds;

            if(roundTime >= 60)
                seconds = 60 - currentRoundTime % 60;
            else
                seconds = roundTime - currentRoundTime % 60;

            //Minutes Text
            string minutesText;
            if(minutes < 10)
                minutesText = "0"+((int)minutes).ToString();
            else
                minutesText = ((int)minutes).ToString();

            //Seconds Text
            string secondsText;
            if(seconds < 10)
                secondsText = "0" + ( (int) seconds).ToString();
            else
                secondsText = ((int)seconds).ToString();

            //Setting Text
            timerText.text = ( minutesText + " : " + secondsText);


            if (currentRoundTime >= roundTime)
            {
                Debug.Log("Time's Up!");
                OnGameEnded();
            }

            return;
        }

        if (currentWaitTime > 0.0f)
        {
            currentWaitTime -= Time.deltaTime;
        }

        if ( currentWaitTime <= 0.0f )
        {
            Restart();
        }

    }

    void RandomizeNewFase()
    {
//        GameObject currentFase = GameObject.FindGameObjectWithTag("Fase");
//        
//        if (currentFase)
//            Destroy(currentFase);
//
//        GameObject newFase = Instantiate(fasesPefab[Random.Range(0, fasesPefab.Length)], Vector3.zero, Quaternion.identity) as GameObject;
    }

    public void OnGameEnded()
    {
        roundEnded = true;
        currentWaitTime = waitTime;

        int countPlayers = 0;
        foreach (GameObject player in players)
        {
            if(player.activeInHierarchy)
                countPlayers++;
        }

        if (countPlayers == 2)
        {
            // winText.gameObject.SetActive(true);
            // winText.text = "Draw!";
            winRenderer.gameObject.SetActive(true);
            winRenderer.sprite = spriteDraw;
        }
        else
        {
            foreach (GameObject player in players)
            {

                PlayerScript playerScript = player.GetComponent<PlayerScript>();

                if (player.activeInHierarchy)
                {
                    if (playerScript.GetComponent<Player1Buttons>())
                    {
                        GameControl.instance.player1Wins++;
                        player1Score.text = GameControl.instance.player1Wins.ToString();
                        //winText.gameObject.SetActive(true);
                        // winText.text = "Player 1 Wins!";
                        winRenderer.gameObject.SetActive(true);
                        winRenderer.sprite = spritePlayer1Wins;

                        MusicManager.instance.PlaySFX("01Wins", transform.position);
                    }
                    else
                    {
                        GameControl.instance.player2Wins++;
                        player2Score.text = GameControl.instance.player2Wins.ToString();
                        // winText.gameObject.SetActive(true);
                        // winText.text = "Player 2 Wins!";
                        winRenderer.gameObject.SetActive(true);
                        winRenderer.sprite = spritePlayer2Wins;

                        MusicManager.instance.PlaySFX("02Wins", transform.position);
                    }
                }
            }
        }
    }

    public void Restart()
    {

        //winText.gameObject.SetActive(false);
        winRenderer.gameObject.SetActive(false);

        foreach (GameObject player in players)
        {
            
            PlayerScript playerScript = player.GetComponent<PlayerScript>();

            playerScript.gameObject.SetActive(false);
            playerScript.gameObject.SetActive(true);
        }

        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (!levelObjects[i].activeInHierarchy)
                levelObjects[i].SetActive(true);
        }

        currentRoundTime = 0.0f;
        roundEnded = false;
        roundCount++;

        RandomizeNewFase();
    }
}
