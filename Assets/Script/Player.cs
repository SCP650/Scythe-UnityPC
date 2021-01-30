using UnityEngine;
 
public class Player : MonoBehaviour, IActorTemplate
{
    int travelSpeed;
    int health;
    int hitPower;
    GameObject actor;
    GameObject fire;    
	GameObject _Player;
	
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
    
	void Start()
	{
	   
	   _Player = GameObject.Find("_Player");
	}
	
	 void Update ()
	{
		Movement();
		Attack();
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
		//input check and move : )
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