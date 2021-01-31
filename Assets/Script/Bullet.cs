using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour, IActorTemplate
{
 
	float travelSpeed;
	int hitPower;
	int score;
	float time;

	private Vector3 forward;
	public Transform target; //can be the player or innocent, will be set by EnemySpawner


    private void Start()
    {
		forward = transform.TransformDirection(Vector3.forward);
	}

    void Update ()
 {

		transform.position += forward * travelSpeed * Time.deltaTime;
		Attack();
 }
 
 public void ActorStats(SOActorModel actorModel)
 {
 
	travelSpeed = actorModel.speed;
	hitPower = actorModel.hitPower;
	score = actorModel.score;
 }

 
    public void Die()
	{
	 
		Destroy(this.gameObject);
	}

private void OnCollisionEnter(Collision collision)
{
		// if the enemy collide with player
 
		if (collision.gameObject.tag == "Player")
		{
	
			collision.gameObject.GetComponent<IActorTemplate>().TakeDamage(hitPower);
			
		}
		Die();
	}

	private void OnTriggerEnter(Collider other)
    {
		Die();
    }
 
 

 
 public int SendDamage()
 {
	return hitPower;
 }

 public void Attack()
 {
 
 
 }

    public void TakeDamage(int incomingDamage)
    {
        throw new System.NotImplementedException();
    }
}