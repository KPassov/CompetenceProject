using UnityEngine;
using System.Collections;

public class NPCAI : MonoBehaviour {

    NavMeshAgent navAgent;
    GameObject player;

    Vector3 moveDirection;
    Vector3 target;
    bool playerInvis = false;


	void Awake () {
    	navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = transform.position;
	}
	
    void Update(){
        if (!playerInvis && (player.transform.position - transform.position).magnitude < 7f){
            if ((player.transform.position - transform.position).magnitude < 3f)
                PanicMove();
            else
                CleverMove();
        }
    }

    public void IgnorePlayer(float seconds){
        playerInvis = true;
        print("INVIS! :)");
        StartCoroutine(Invis(seconds));
    }

    IEnumerator Invis(float seconds){
        yield return new WaitForSeconds(seconds); 
        print("VIS! :(");
        playerInvis = false;
    }

    void PanicMove(){
        moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
        navAgent.destination = new Vector3(moveDirection.x, 0, moveDirection.z);
        target = navAgent.destination;
    }

    void CleverMove(){
        if(nearTarget(target, transform.position)){
            moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
            navAgent.destination = new Vector3(moveDirection.x, 0, moveDirection.z);
            target = navAgent.destination;
        }
    }

    private bool nearTarget(Vector3 target, Vector3 position){
        return (target - position).magnitude < 1f;
    }
}
