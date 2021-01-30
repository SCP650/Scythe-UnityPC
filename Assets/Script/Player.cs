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
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			if (health >= 1)
			{
				if (transform.Find("energy +1(Clone)"))
				{
					Destroy(transform.Find("energy +1(Clone)").gameObject);
					health -= other.GetComponent<IActorTemplate> ().SendDamage();
				}
				else
				{
					health -= 1;
				}
			}
      
			if (health <= 0)
			{
				Die();
			}
		}
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
		float y = Input.GetAxis("Vertical");

		rb.MovePosition(transform.position + new Vector3(x, y, 0) * travelSpeed * Time.fixedDeltaTime);

		Vector3 fromPosition = rb.position;
		Vector3 cameraMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		// cameraMousePosition.z = 0;

		Vector3 diff = (fromPosition - cameraMousePosition);
		diff.z = 0;
		// float rotationX = Mathf.Atan2(difference.z, difference.y) * Mathf.Rad2Deg;

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