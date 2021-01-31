﻿using UnityEngine;
using System.Collections;
 
public class Player : MonoBehaviour, IActorTemplate
{
	[SerializeField]
    float travelSpeed;

    int health;
	[SerializeField]
    int hitPower = 1;
    GameObject fire;
	GameObject _Player;

	Rigidbody rb;
	private Camera mainCamera;

	private bool isAttackingRight;
	private bool isAttacking;
	private bool canSwapDirection;
	private Animator playerAnimator;

	[SerializeField]
	private GameObject attackBox;

	public static Player S;

	private bool blockRotation = false;
	private bool isDashing = false;

	[SerializeField]
	private float cooldownMax = 2.0f;
	[SerializeField]
	private float dashMax = 0.5f;
	[SerializeField]
	private float dashSpeedModifier = 3.0f;
	[SerializeField]
	private TrailRenderer scytheRenderer;
	[SerializeField]
	private TrailRenderer tr;

	public int Health
    {
        get {return health;}
        set {health = value;}
    }
   
    public GameObject Fire
    {
        get {return fire;}
        set {fire = value;}
    }

	void Awake()
    {
		rb = GetComponent<Rigidbody>();
		playerAnimator = GetComponent<Animator>();

		S = this;
	}

	void Start()
	{
		_Player = GameObject.Find("_Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update ()
	{
		playerAnimator.SetBool("right", isAttackingRight);
		isAttacking = attackBox.activeSelf;

		if (!blockRotation)  Attack();
	}

	void FixedUpdate()
    {
		Movement();
    }
	
	public void ActorStats(SOActorModel actorModel)
	{
		health = actorModel.health;
		travelSpeed = actorModel.speed;
		hitPower = actorModel.hitPower;
		fire = actorModel.actorsBullets;
	}
	
	public void TakeDamage(int incomingDamage)
	{
		health -= incomingDamage;
	}
 
	public int SendDamage()
	{
		return hitPower;
	}

	void Movement()
	{
		if (!isDashing)
        {
			if (!blockRotation && Input.GetButtonDown("Dash"))
            {
				isDashing = true;
				StartCoroutine(Dash());
				return;
            }

			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			rb.MovePosition(transform.position + new Vector3(x, 0, z) * travelSpeed * Time.fixedDeltaTime);

			if (x == 0 && z == 0)
			{
				rb.velocity = Vector3.zero;
			}

			if (!blockRotation)
			{
				Vector3 fromPosition = rb.position;
				Vector3 cameraMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

				Vector3 diff = (fromPosition - cameraMousePosition);
				diff.y = 0;

				rb.MoveRotation(Quaternion.LookRotation(diff));
			}
		}
		tr.enabled = isDashing;
	}
	
	public void Die()
	{
		GameManager.Instance.LifeLost();
		Destroy(this.gameObject);
	}
	
	public void Attack()
	{
		if ((Input.GetButtonDown("Attack") || Input.GetMouseButtonDown(0)) && !isAttacking)
        {
			isAttackingRight = !isAttackingRight;
			playerAnimator.SetTrigger("attacking");
			isAttacking = true;
			StartCoroutine(AttackCooldown());
		}
	}

	private IEnumerator AttackCooldown()
    {
		scytheRenderer.enabled = true;
		float cooldownTimer = 0;
		while (cooldownMax > cooldownTimer)
        {
			cooldownTimer += Time.deltaTime;
			blockRotation = true;
			yield return null;
        }
		blockRotation = false;
		scytheRenderer.Clear();
		scytheRenderer.enabled = false;
	}
	
	private IEnumerator Dash()
    {
		tr.Clear();
		float dashTimer = 0;
		while (dashMax > dashTimer)
		{
			dashTimer += Time.deltaTime;
			rb.MovePosition(transform.position - transform.forward * dashSpeedModifier * Time.deltaTime * (dashMax / dashTimer));
			yield return null;
        }
		print("here");
		isDashing = false;
    }
}