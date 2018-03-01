using UnityEngine;
using System.Collections;

public enum AnimationStates
{
	idleDown = 0,
	idleUp = 1,
	idleRight = 2,
	idleLeft = 3,

	normalDown = 4,
	normalUp = 5,
	normalRight = 6,
	normalLeft = 7,

	dashDown = 8,
	dashUp = 9,
	dashRight = 10,
	dashLeft = 11
}

public class PlayerScript : MonoBehaviour 
{
	
	public ButtonClass buttons; //Buttons

	//Components
	[HideInInspector]
	public Animator _animator;
	[HideInInspector]
	public Rigidbody2D  _rigidBody;
	[HideInInspector]
	public CircleCollider2D _circleCollider;
	[HideInInspector]
	public BoxCollider2D _footCollider;
	[HideInInspector]
	public SpriteRenderer _spriteRenderer;


	public Bounds arenaBounds;

	AnimationStates animationState;
	public int state;
	
	private float moveForce = 7.5f;
	private float dashForce = 5.0f;

	//Dash
	[HideInInspector]
	public bool isDashing;
	public float dashCooldown = 0.3f;
	private float countDashCooldown;
	[HideInInspector]
	public float deltaTimeLastDash = 0.0f;

	//Move
	private Vector2 lastMoveVector;
	private Vector2 moveVector;
	private Vector2 spawnPoint;

	//public int wins;

	CameraShakeEffect cameraShakeScript;

	void Start()
	{
		//Getting Components
		_animator = gameObject.GetComponentInChildren<Animator>();
		state = Animator.StringToHash("state");

		_rigidBody = gameObject.GetComponent<Rigidbody2D>();
		_rigidBody.drag = 2.0f;

		_circleCollider = gameObject.GetComponent<CircleCollider2D>();

		_footCollider = gameObject.GetComponentInChildren<BoxCollider2D>();

		buttons = gameObject.GetComponent<ButtonClass>();

		//Getting Arena Bounds
		GameObject arena = GameObject.FindGameObjectWithTag("Arena");
		arenaBounds = arena.GetComponent<SpriteRenderer>().bounds;

		cameraShakeScript = Camera.main.gameObject.GetComponent<CameraShakeEffect>();

		//Initial LastMoveVector (Down)
		lastMoveVector = new Vector2(0, -1);
		spawnPoint = transform.position;
	}

	void OnEnable()
	{
		lastMoveVector = new Vector2(0, -1);
		
		isDashing = false;
		countDashCooldown = 0.0f;
		deltaTimeLastDash = 0.0f;

		transform.position = spawnPoint;

        animationState = AnimationStates.idleDown;
	}
	
	void Update ()
	{
		_animator.SetInteger(state,(int)animationState);

		DashCountdown();
			
		SettingInput();

		if((Input.GetKeyDown(buttons.dashKeyCode) ||  buttons.GetDashButtonDown() ) && !isDashing)
			PerformDash();
		else
			PerformMovement();
			
		VerifyIfOutsideArena();
	}

	void UpdateDashVisual()
	{
		//Update Dash Visuals
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			MusicManager.instance.PlaySFX("HurtSound", transform.position);
			PlayerScript _playerScript = other.gameObject.GetComponent<PlayerScript>();

			if(_playerScript != null)
			{
				if(isDashing || _playerScript.isDashing)
					MusicManager.instance.PlaySFX("ImpactSound", transform.position);
					cameraShakeScript.Shake();
				return;
			}
		}

		if(other.gameObject.CompareTag("ArenaBorder"))
		{
            other.gameObject.SetActive(false);
			MusicManager.instance.PlaySFX("IceBreakerSound", transform.position);
            
		}
	}

	#region Input

	void SettingInput()
	{
		moveVector = Vector2.zero;
		
		if(Input.GetKey(buttons.rightKeyCode) || buttons.GetHorizontalInput() > 0.5f)
		{
			moveVector.x = 1;

			if(isDashing)
				animationState = AnimationStates.dashRight;
				else
					if(_rigidBody.velocity.magnitude > 0.4f)
					animationState = AnimationStates.normalRight;
						else
							animationState = AnimationStates.idleRight;

		}
		else if(Input.GetKey(buttons.leftKeyCode) ||  buttons.GetHorizontalInput() < -0.5f )
		{
			moveVector.x = -1;
			
			if(isDashing)
				animationState = AnimationStates.dashLeft;
			else
				if(_rigidBody.velocity.magnitude > 0.4f)
					animationState = AnimationStates.normalLeft;
			else
				animationState = AnimationStates.idleLeft;
		}
		
		if(Input.GetKey(buttons.upKeyCode) ||  buttons.GetVerticalInput() > 0.5f)
		{
			moveVector.y = 1;
		
			if(isDashing)
				animationState = AnimationStates.dashUp;
			else
				if(_rigidBody.velocity.magnitude > 0.4f)
					animationState = AnimationStates.normalUp;
			else
				animationState = AnimationStates.idleUp;
		}
		else if(Input.GetKey(buttons.downKeyCode) ||  buttons.GetVerticalInput() < -0.5f)
		{
			moveVector.y = -1;

			if(isDashing)
				animationState = AnimationStates.dashDown;
			else
				if(_rigidBody.velocity.magnitude > 0.4f)
					animationState = AnimationStates.normalDown;
			else
				animationState = AnimationStates.idleDown;
		}

		//Setting Input to Movement Vector
		if(moveVector != Vector2.zero)
			lastMoveVector = moveVector;

	}

	#endregion

	#region Controls
	void PerformDash()
	{
		MusicManager.instance.PlaySFX("DashSound", transform.position);
		if(moveVector == Vector2.zero)
		{
			moveVector = lastMoveVector;
		}

		moveVector *= dashForce;
		_rigidBody.AddForce(moveVector, ForceMode2D.Impulse);
		
		isDashing = true;
		countDashCooldown = dashCooldown;
		deltaTimeLastDash = 0.0f;
	}

	void PerformMovement()
	{
		moveVector *= moveForce;
		_rigidBody.AddForce(moveVector, ForceMode2D.Force);
	}

	#endregion

	#region Misc

	void DashCountdown()
	{
		if(isDashing)
		{
			deltaTimeLastDash += Time.deltaTime;
			
			if(countDashCooldown > 0.0f)
				countDashCooldown -= Time.deltaTime;
		}
		
		if(countDashCooldown <= 0.0f)
		{
			isDashing = false;
		}
	}

	void VerifyIfOutsideArena()
	{

		if(!_footCollider.bounds.Intersects(arenaBounds))
		{
			MusicManager.instance.PlaySFX("FallingSound", transform.position);
			OnGameOver();
		}
	}

	public void OnGameOver()
    {
       gameObject.SetActive(false);

       if (LevelManager.instance.roundEnded)
           return;

       LevelManager.instance.OnGameEnded();
	}

	#endregion
}

