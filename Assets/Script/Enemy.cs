<<<<<<< HEAD
﻿using UnityEngine;

public class Enemy : MonoBehaviour, IActorTemplate
{
 int health;
 int travelSpeed;
 int fireSpeed;
 int hitPower;
 int score;
 
 float time;
 
 void Update ()
 {
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
=======
﻿using UnityEngine;

public class Enemy : MonoBehaviour, IActorTemplate
{
 int health;
 int travelSpeed;
 int fireSpeed;
 int hitPower;
 int score;
 float time;

public Transform target; //can be the player or innocent

void Update ()
 {
	 Move();
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
		transform.position =  Vector3.MoveTowards(transform.position, target.position,0.2f);
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
>>>>>>> ea2f528c2b1a78ea5dd818586bc589ad7bc2ee24
 }