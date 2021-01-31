using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
 [SerializeField]
 SOActorModel actorModel;
	[SerializeField]
	SOActorModel ShooterEnemy;
 [SerializeField]
 float spawnRate;
 [SerializeField]
 [Range(0,40)]
 int quantityInWave;
 [SerializeField] int MapLength;
 [SerializeField] int MapWidth;
 [SerializeField] Transform Player;
	[SerializeField] float WaitBetweenWaves;

private int currentEnemyCount = 1;

	void Awake()
 {
	StartCoroutine(SpawnEnemy(quantityInWave, spawnRate));
 }
 
 IEnumerator SpawnEnemy(int qty, float spwnRte)
 {
        while (true)
        {
			for (int i = 0; i < qty; i++)
			{
				currentEnemyCount += i;
				Vector3 position = new Vector3(Random.Range(-MapWidth / 2, MapWidth / 2), 0, Random.Range(-MapLength / 2, MapLength / 2));
				GameObject enemyUnit = CreateEnemy(position);
				enemyUnit.gameObject.transform.SetParent(this.transform);
		

				//enemyUnit.transform.Rotate(0, Random.Range(0, 360), 0);

				yield return new WaitForSeconds(spwnRte);
			}
			
            if (currentEnemyCount%4 == 0)
            {
				WaitBetweenWaves *= 0.8f;
				if (WaitBetweenWaves < 1) WaitBetweenWaves = 2;

			}
			yield return new WaitForSeconds(WaitBetweenWaves);
		}

  }
  
  GameObject CreateEnemy(Vector3 position)
  {
		GameObject enemy;
 
		if(currentEnemyCount %7 == 0)
        {
			  enemy = GameObject.Instantiate(ShooterEnemy.actor) as GameObject;
			enemy.transform.position = position;
			enemy.GetComponent<ShooterEnemy>().target = Player;
			enemy.GetComponent<IActorTemplate>().ActorStats(ShooterEnemy);
		}
        else
        {
            enemy = GameObject.Instantiate(actorModel.actor,position, Quaternion.identity) as GameObject;
            enemy.GetComponent<Enemy>().target = Player;
            enemy.GetComponent<IActorTemplate>().ActorStats(actorModel);
   //         enemy = GameObject.Instantiate(ShooterEnemy.actor) as GameObject;
			//enemy.GetComponent<ShooterEnemy>().target = Player;
			//enemy.GetComponent<IActorTemplate>().ActorStats(ShooterEnemy);
		}
		
		
		enemy.name = actorModel.actorName.ToString();
	return enemy;
  }
}