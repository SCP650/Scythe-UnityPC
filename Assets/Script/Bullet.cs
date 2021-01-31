using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour, IActorTemplate
{
 
	float travelSpeed = 6;
	int hitPower=5;
	 

	private Vector3 forward;
	public Transform target; //can be the player or innocent, will be set by EnemySpawner

	 

    void Update ()
 {
        if (target)
        {
			transform.position = Vector3.MoveTowards(transform.position, target.position, travelSpeed * Time.deltaTime);
		}
		
 }
 
 public void ActorStats(SOActorModel actorModel) //not being used
 {
 
	travelSpeed = actorModel.speed;
	hitPower = actorModel.hitPower;
 
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