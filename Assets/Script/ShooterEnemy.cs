using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ShooterEnemy : MonoBehaviour, IActorTemplate
{
	int health;
	float travelSpeed;
	int fireSpeed = 5;
	int hitPower;
	int score;
	float time;
	GameObject bullets;


	public Transform target;  
	private NavMeshAgent agent;
	private int qty = 5;
	private Animator animator;

	[SerializeField]
	private GameObject bloodParticleSystemPrefab;


	void Start()

    {
		
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		StartCoroutine(Fire());


	}

    void Update ()
 {
		agent.destination = target.position;
	 
		Attack();
 }
 
 public void ActorStats(SOActorModel actorModel)
 {
	health = actorModel.health;
	travelSpeed = actorModel.speed;
	hitPower = actorModel.hitPower;
	score = actorModel.score;
		bullets = actorModel.actorsBullets;
 }

 
    public void Die()
	{
		Soul.singleton.numDemons++;
		GameManager.Instance.GetComponent<ScoreManager>().SetScore(score);
		if (bloodParticleSystemPrefab) Instantiate(bloodParticleSystemPrefab, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}

	IEnumerator Fire()
    {
        while (true)
		{
			yield return new WaitForSeconds(fireSpeed);
			animator.SetBool("Fire", true);
			for (int i = 0; i < qty; i++)
            {
				GameObject bullet = GameObject.Instantiate(bullets) as GameObject;
				bullet.transform.position = gameObject.transform.position + new Vector3(1,0,1);
				bullet.transform.rotation = gameObject.transform.rotation;

				bullet.GetComponent<Bullet>().target = target;
				yield return new WaitForSeconds(0.2f);
			}
			
			animator.SetBool("Fire", false);
			
		}
		
	}

	 
	//private void OnCollisionEnter(Collision collision)
	//{
	//		// if the enemy collide with player

	//		if (collision.gameObject.tag == "Player")
	//		{

	//			
	//			collision.gameObject.GetComponent<IActorTemplate>().TakeDamage(hitPower);

	//		}
	//	}

	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "AttackBox")
		{
			//print(Player.S.SendDamage());
			TakeDamage(Player.S.SendDamage());
		}
    }
 
 public void TakeDamage(int incomingDamage)
 {
	health -= incomingDamage;
	if(health < 0)
        {
			Die();
        }
 }

 
 public int SendDamage()
 {
	return hitPower;
 }

 public void Attack()
 {
	time += Time.deltaTime;
 
 }
 }