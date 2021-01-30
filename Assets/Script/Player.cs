using UnityEngine;
 
public class Player : MonoBehaviour, IActorTemplate
{
	[SerializeField]
    float travelSpeed;

    int health;
    int hitPower;
    GameObject fire;
	GameObject _Player;

	Rigidbody rb;
	private Camera mainCamera;

	public bool isAttackingRight;
	public bool isAttacking;
	public bool canSwapDirection;
	private Animator playerAnimator;

	[SerializeField]
	private GameObject attackBox;

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
	}

	void Start()
	{
	   _Player = GameObject.Find("_Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update ()
	{
		if (!isAttacking) {
			// isAttackingRight = !isAttackingRight;
			// canSwapDirection = false;
			// playerAnimator.SetBool("attacking", false);
		}

		playerAnimator.SetBool("right", isAttackingRight);
		isAttacking = attackBox.activeSelf;

		Attack();
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
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		rb.MovePosition(transform.position + new Vector3(x, 0, z) * travelSpeed * Time.fixedDeltaTime);

		if (x == 0 && z == 0)
		{
			rb.velocity = Vector3.zero;
		}

		if (!isAttacking)
        {
			Vector3 fromPosition = rb.position;
			Vector3 cameraMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

			Vector3 diff = (fromPosition - cameraMousePosition);
			diff.y = 0;

			rb.MoveRotation(Quaternion.LookRotation(diff));
		}

		// print(fromPosition - cameraMousePosition);
	}
	
	public void Die()
	{
		GameManager.Instance.LifeLost();
		Destroy(this.gameObject);
	}
	
	public void Attack()
	{
		if (Input.GetButtonDown("Attack") && !isAttacking)
        {
			isAttackingRight = !isAttackingRight;
			// playerAnimator.SetBool("attacking", true);
			playerAnimator.SetTrigger("attacking");
			isAttacking = true;
			// canSwapDirection = true;
			// isAttacking = true;
		}
	}
}