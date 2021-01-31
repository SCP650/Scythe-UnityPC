using UnityEngine;
using System.Collections;
 
public class Player : MonoBehaviour, IActorTemplate
{
	[SerializeField]
	private float baseSpeed;
	float currentSpeed;

	int health;
	[SerializeField]
	int maxHealth;
	float healthOverflow;
	public float healthPerSecond;
	public float HealthPerSecond
    {
		get
        {
			return healthPerSecond;
        }
        set
        {
			print($"Health per second is now {value}");
			healthPerSecond = value;
        }
    }
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
	private Vector3 attackBoxBaseScale;

	public static Player S;

	private bool blockRotation = false;
	private bool isDashing = false;
	public bool canDash { get; set; }

	[SerializeField]
	private float cooldownMax = 2.0f;
	[SerializeField]
	private float dashMax = 0.5f;
	[SerializeField]
	private TrailRenderer tr;

	[SerializeField]
	private float dashSpeedMultiplier = 4.0f;

	[SerializeField]
	private Transform scythe;
	private TrailRenderer scytheRenderer;
	private Vector3 baseScytheScale;
	
	private float swingBaseSpeed = 1;
	private float swingSpeed;

	[Header("Projectile")]
	[SerializeField]
	private GameObject projectile;
	[SerializeField]
	private float projectileForce;
	[SerializeField]
	private Transform spawnLocation;
	private bool canShootProjectile;

	[Header("Multipliers for Bonuses")]
	[SerializeField]
	private float speedMultplier = 1.5f;
	[SerializeField]
	private float swingSpeedMultiplier = 2;
	[SerializeField]
	private float scytheScaleMultiplier = 2;

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
		health = maxHealth;
		healthPerSecond = 0;

		scytheRenderer = scythe.GetComponent<TrailRenderer>();

		currentSpeed = baseSpeed;
		baseScytheScale = scythe.localScale;
		attackBoxBaseScale = attackBox.transform.localScale;
		swingSpeed = swingBaseSpeed;
	}

	void Update ()
	{
		playerAnimator.SetBool("right", isAttackingRight);
		isAttacking = attackBox.activeSelf;
		scythe.tag = isAttacking ? "AttackBox" : "Untagged";

		if (!blockRotation)  Attack();
		PassivelyHeal();
	}

	public void PassivelyHeal()
    {
		float healthBack = healthOverflow + healthPerSecond * Time.deltaTime;
		//print($"Health back on this fram is {healthBack}");
		int intHealthback = (int)healthBack;
		healthOverflow = healthBack - intHealthback;
		health += intHealthback;
		if (health > maxHealth)
        {
			health = maxHealth;
        }
		if (health <= 0)
        {
			Die();
        }
    }

	void FixedUpdate()
    {
		Movement();
    }
	
	public void ActorStats(SOActorModel actorModel)
	{
		health = actorModel.health;
		currentSpeed = actorModel.speed;
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

	public int GetMaxHealth()
    {
		return maxHealth;
    }

	void Movement()
	{
		if (!isDashing)
        {
			if (!blockRotation && Input.GetButtonDown("Dash") && canDash)
            {
				isDashing = true;
				StartCoroutine(Dash());
				return;
            }

			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			rb.MovePosition(transform.position + new Vector3(x, 0, z) * currentSpeed * Time.fixedDeltaTime);

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

			if (canShootProjectile)
            {
				GameObject projectileObj = Instantiate(projectile, spawnLocation.position, transform.rotation);
				Vector3 forceVector = projectileForce * transform.forward;
				projectileObj.GetComponent<Rigidbody>().AddForce(-forceVector, ForceMode.VelocityChange);
			}
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
			rb.MovePosition(transform.position - transform.forward * dashSpeedMultiplier * Time.deltaTime * (dashMax / dashTimer));
			yield return null;
        }
		print("here");
		isDashing = false;
    }

	public void ChangeSpeed(bool increase = true)
    {
		if (increase)
		{
			if (currentSpeed < baseSpeed * speedMultplier) currentSpeed *= speedMultplier;
		}
		else currentSpeed = baseSpeed;
	}

	public void ChangeSwingSpeed(bool increase = true)
	{
		if (increase)
		{
			if (playerAnimator.speed < swingBaseSpeed * swingSpeedMultiplier) playerAnimator.speed *= swingSpeedMultiplier;
		}
		else playerAnimator.speed = swingBaseSpeed;
	}

	public void ChangeScytheSize(bool increase = true)
	{
		if (increase)
		{
			if (scythe.localScale.z < baseScytheScale.z * scytheScaleMultiplier)
			{
				scythe.localScale = new Vector3(baseScytheScale.x, baseScytheScale.y, baseScytheScale.z * scytheScaleMultiplier);
				attackBox.transform.localScale = new Vector3(attackBoxBaseScale.x, attackBoxBaseScale.y, attackBoxBaseScale.z * scytheScaleMultiplier);
			}
		}
		else
		{
			scythe.localScale = baseScytheScale;
			attackBox.transform.localScale = attackBoxBaseScale;
		}
	}

	public void ToggleShootProojectile(bool projectileEnabled = true)
    {
		canShootProjectile = projectileEnabled;
    }
}