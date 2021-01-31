using System.Collections;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
 [SerializeField]
 SOActorModel actorModel;
 [SerializeField]
 float spawnRate;
 [SerializeField]
 [Range(0,30)]
 int quantity;
 [SerializeField] int MapLength;
 [SerializeField] int MapWidth;
	[SerializeField] Transform Player;
	[SerializeField] float WaitBetweenWaves;
	private int currentEnemyCount;


	void Awake()
 {
	StartCoroutine(SpawnHuman(quantity, spawnRate));
 }
 
 IEnumerator SpawnHuman(int qty, float spwnRte)
 {
		while (true)
		{
			for (int i = 0; i < qty; i++)
			{
				GameObject enemyUnit = CreateEnemy();
				enemyUnit.gameObject.transform.SetParent(this.transform);
				enemyUnit.transform.position = new Vector3(Random.Range(-MapWidth / 2, MapWidth / 2), 0, Random.Range(-MapLength / 2, MapLength / 2));

				//enemyUnit.transform.Rotate(0, Random.Range(0, 360), 0);

				yield return new WaitForSeconds(spwnRte);
			}
			currentEnemyCount += qty;
			if (currentEnemyCount % 4 == 0)
			{
				WaitBetweenWaves *= 0.8f;
				if (WaitBetweenWaves < 1) WaitBetweenWaves = 2;

			}
			yield return new WaitForSeconds(WaitBetweenWaves);
		}
	}
  
  GameObject CreateEnemy()
  {
        GameObject enemy = GameObject.Instantiate(actorModel.actor) as GameObject;
        enemy.GetComponent<IActorTemplate>().ActorStats(actorModel);
		enemy.GetComponent<Human>().target = Player;
		enemy.name = actorModel.actorName.ToString();
	return enemy;
  }
}