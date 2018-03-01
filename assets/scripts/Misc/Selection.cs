using UnityEngine;
using System.Collections;

public enum CharactersChoices
{
	Alligator,
	Lucky_Lucky,
	Fox,
	Panda
}

public class Selection : MonoBehaviour
{

	ButtonClass button;
	int targetIndex = 0;
	public Sprite[] characterSprites;

	public int playerNumber;

	public SpriteRenderer displaySpriteSpriteRenderer;
    bool clicked;
    bool choiced;

	void Start()
	{
		targetIndex = 0;
		displaySpriteSpriteRenderer.sprite = characterSprites[targetIndex];
        button = gameObject.GetComponent<ButtonClass>();
	}

	public void MoveVectorForwards()
	{
		targetIndex++;

		if(targetIndex >= characterSprites.Length)
		{
			targetIndex = 0;
		}

		displaySpriteSpriteRenderer.sprite = characterSprites[targetIndex];
	}

	public void MoveVectorBackwards()
	{
		targetIndex--;

		if(targetIndex < 0)
		{
			targetIndex = characterSprites.Length -1;
		}

		displaySpriteSpriteRenderer.sprite = characterSprites[targetIndex];
	}

	void Update()
	{
        if (choiced)
            return;

        if (playerNumber == 1)
        {
            if (GameControl.instance.player2HaveChoiced)
            {
                if (targetIndex == GameControl.instance.player2Number)
                    MoveVectorForwards();
            }
        }
        else if (playerNumber == 2)
        {
            if (GameControl.instance.player1HaveChoiced)
            {
                if (targetIndex == GameControl.instance.player1Number)
                    MoveVectorForwards();
            }
        }

        if (Input.GetKeyDown(button.dashKeyCode) || button.GetDashButtonDown())
		{
			if (playerNumber == 1)
				GameControl.instance.Player1Choiced(targetIndex);
			else
				GameControl.instance.Player2Choiced(targetIndex);

            Color originalColor = displaySpriteSpriteRenderer.color;

            originalColor.r -= 0.3f;
            originalColor.g -= 0.3f;
            originalColor.b -= 0.3f;

            Mathf.Clamp(originalColor.r, 0, 1);
            Mathf.Clamp(originalColor.g, 0, 1);
            Mathf.Clamp(originalColor.b, 0, 1);

            displaySpriteSpriteRenderer.color = originalColor;
            MusicManager.instance.PlaySFX("DashSound", transform.position);
            choiced = true;
		}

        if (Input.GetKeyDown(button.rightKeyCode) || Input.GetKeyDown(button.leftKeyCode)  || button.GetHorizontalInput() > 0.5f || button.GetHorizontalInput() < -0.5f)
        {

            if (!clicked)
            {
                if (Input.GetKeyDown(button.leftKeyCode) || button.GetHorizontalInput() < -0.5f)
                {
                    MoveVectorBackwards();
                }

                if (Input.GetKeyDown(button.rightKeyCode) || button.GetHorizontalInput() > 0.5f)
                {
                    MoveVectorForwards();
                }

                clicked = true;
            }
        }
        else
        {
            clicked = false;
        }

	}


}
