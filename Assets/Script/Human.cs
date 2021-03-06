﻿using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour, IActorTemplate
{
 int health;
 float travelSpeed;
 int fireSpeed;
 int hitPower;
 int score;
 float time;
 
public Transform target; //player
	public float EnemyDistanceToRun = 5.0f;

	private NavMeshAgent agent;

	[SerializeField]
	private GameObject bloodParticleSystemPrefab;

	private void Start()
    {
		
		agent = GetComponent<NavMeshAgent>();
		agent.destination = target.position+ new Vector3(Random.Range(-10,10),0, Random.Range(-10, 10));
	}

    void Update ()
 {
		
		Flee();
		Attack();
 }
 
 public void ActorStats(SOActorModel actorModel)
 {
	health = actorModel.health;
	travelSpeed = actorModel.speed;
	hitPower = actorModel.hitPower;
	score = actorModel.score;
 }

public void Flee()
{
		if (target != null)
        {
			float distance = Vector3.Distance(transform.position, target.position);
			if (distance < EnemyDistanceToRun)
			{
				Vector3 disToPlayer = transform.position - target.transform.position;
				Vector3 newPos = transform.position + disToPlayer;
				agent.SetDestination(newPos);
				agent.speed = 6;
			}
		}
}

    public void Die()
 {
		Soul.singleton.numHumans++;
		if (bloodParticleSystemPrefab) Instantiate(bloodParticleSystemPrefab, transform.position, transform.rotation);
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
	else if (other.tag == "AttackBox")
    {
		if (health >= 1)
		{
			// Projectile layer
			if (LayerMask.LayerToName(other.gameObject.layer) == "Projectile")
            {
				health -= Player.S.SendDamage();
				}
			else
			{
				IActorTemplate temp = other.GetComponentInParent<IActorTemplate>();
					if (temp == null) health -= Player.S.SendDamage();
					else health -= other.GetComponentInParent<IActorTemplate>().SendDamage();
				}
		}
		if (health <= 0)
		{
			GameManager.Instance.GetComponent<ScoreManager>().SetScore(score);
				SoundManager.S.PlayDeathSound();
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