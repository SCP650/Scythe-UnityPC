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
    }

	void Start()
	{
	   _Player = GameObject.Find("_Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update ()
	{
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

		Vector3 fromPosition = rb.position;
		Vector3 cameraMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		Vector3 diff = (fromPosition - cameraMousePosition);
		diff.y = 0;

		// print(fromPosition - cameraMousePosition);
		rb.MoveRotation(Quaternion.LookRotation(diff));
	}
	
	public void Die()
	{
		GameManager.Instance.LifeLost();
		Destroy(this.gameObject);
	}
	
	public void Attack()
	{
		//Attack here!
	}
}