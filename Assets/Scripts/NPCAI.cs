using UnityEngine;
using System.Collections;

public class NPCAI : MonoBehaviour {

    NavMeshAgent navAgent;
    GameObject player;

	void Awake () {
    	navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
    void Update(){
        navAgent.destination = player.transform.position; 
    }
}
