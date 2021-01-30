﻿using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IActorTemplate
{
	int health;
	float travelSpeed;
	int fireSpeed;
	int hitPower;
	int score;
	float time;

	public Transform target; //can be the player or innocent, will be set by EnemySpawner
	private NavMeshAgent agent;
	private Animator animator;

    private void Start()

    {
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		
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
	 
			animator.SetTrigger("Hit");
			collision.gameObject.GetComponent<IActorTemplate>().TakeDamage(hitPower);
			
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