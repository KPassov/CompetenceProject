using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
		spawnPoints = GetComponentsInChildren<Transform> ();
		StartCoroutine (FleeSpawner ());	
		StartCoroutine (BombSpawner ());	
	}
	
	IEnumerator FleeSpawner(){
		while (true) {
			Instantiate(GameObject.Instantiate(Resources.Load("Prefabs/NPCs/FleeingNPC"), 
			            spawnPoints[Random.Range (0,spawnPoints.Length)].position, Quaternion.identity));
			yield return new WaitForSeconds(1f);
		}
	}
	IEnumerator BombSpawner(){
		while (true) {
			Instantiate(GameObject.Instantiate(Resources.Load("Prefabs/NPCs/BombNPC"), 
			                                   spawnPoints[Random.Range (0,spawnPoints.Length)].position, Quaternion.identity));
			yield return new WaitForSeconds(4f);
		}
	}

}
