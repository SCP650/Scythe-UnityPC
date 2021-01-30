using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IActorTemplate
{
 int health;
 float travelSpeed;
 int fireSpeed;
 int hitPower;
 int score;
 float time;


public Transform target; //can be the player or innocent
	private NavMeshAgent agent;

    private void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		
	}

    void Update ()
 {
		agent.destination = target.position;
		//Move();
		Attack();
 }
 
 public void ActorStats(SOActorModel actorModel)
 {
	health = actorModel.health;
	travelSpeed = actorModel.speed;
	hitPower = actorModel.hitPower;
	score = actorModel.score;
 }

public void Move()
{
		transform.position =  Vector3.MoveTowards(transform.position, target.position,travelSpeed);
}

    public void Die()
 {
	Destroy(this.gameObject);
 }
 
 void OnTriggerEnter(Collider other)
 {
	// if the player or their bullet hits you....
	if (other.tag == "Player")
	{
		if (health >= 1)
		{
			health -= other.GetComponent<IActorTemplate>().SendDamage();
		}
		if (health <= 0)
		{
			GameManager.Instance.GetComponent<ScoreManager>().SetScore(score);
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

 public void Attack()
 {
	time += Time.deltaTime;
 
 }
 }