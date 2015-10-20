using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	Transform[] spawnPoints;

	float fleeFrec = 4f;
	float minFleeSpawn = 2f;

	float waitBeforeStartSpawningBombs = 10f;
	float bombFrec = 10f;
	float minBombSpawn = 3f;

	// Use this for initialization
	void Start () {
		spawnPoints = GetComponentsInChildren<Transform> ();
		StartCoroutine (SpawnSpawner());
	}

	IEnumerator SpawnSpawner(){
		StartCoroutine (FleeSpawner ());	
		yield return new WaitForSeconds (waitBeforeStartSpawningBombs);
		StartCoroutine (BombSpawner ());
	}

	IEnumerator FleeSpawner(){
		while (true) {
			print ("Spawned FLEE, next spawn in " + fleeFrec);
			Instantiate(Resources.Load("Prefabs/NPCs/FleeingNPC"), 
			            spawnPoints[Random.Range (0,spawnPoints.Length)].position, Quaternion.identity);
			yield return new WaitForSeconds(fleeFrec);
			fleeFrec = Mathf.Max (fleeFrec - fleeFrec/10f, minFleeSpawn);
		}
	}
	IEnumerator BombSpawner(){
		while (true) {
			print ("Spawned BOMB, next spawn in " + bombFrec);
			Instantiate(Resources.Load("Prefabs/NPCs/BombNPC"), 
			                           spawnPoints[Random.Range (0,spawnPoints.Length)].position, Quaternion.identity);
			yield return new WaitForSeconds(bombFrec);
			bombFrec = Mathf.Max (bombFrec - bombFrec/10f, minBombSpawn);
		}
	}

}
