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
        StopTime(4f);
	}
	
    void Update(){
        if (!playerInvis && (player.transform.position - transform.position).magnitude < 7f){
            if ((player.transform.position - transform.position).magnitude < 3f)
                PanicMove();
            else
                CleverMove();
        } else {
            Idle();
        }
    }

    public void InvisPlayer(float seconds){
        StartCoroutine(Invis(seconds));
    }

    public void StopTime(float seconds){
        StartCoroutine(Freeze(seconds));
    }

    IEnumerator Freeze(float seconds){
        float oldSpeed = navAgent.speed;
        float oldAngular = navAgent.angularSpeed;
        navAgent.speed = 0f;
        navAgent.angularSpeed = 0f;
        yield return new WaitForSeconds(seconds); 
        navAgent.angularSpeed = oldAngular;
        navAgent.speed = oldSpeed;
    }

    IEnumerator Invis(float seconds){
        playerInvis = true;
        yield return new WaitForSeconds(seconds); 
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

    void Idle(){
        
    }

    private bool nearTarget(Vector3 target, Vector3 position){
        return (target - position).magnitude < 1f;
    }
}
