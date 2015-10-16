using UnityEngine;
using System.Collections;

public class NPCAI : MonoBehaviour {

    NavMeshAgent navAgent;
    GameObject player;

    Vector3 moveDirection;
    Vector3 target;

	void Awake () {
    	navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = transform.position;
	}
    
    public void ChangeSpeed(float newSpeed){
        navAgent.speed = newSpeed;
    }

    public void Move(string state){
		switch(state){
            case"Nervous":
                NervousMove();
                break;
            case "Panic":
                PanicMove();
                break;
            case "Idle":
                IdleMove();
                break;
            default:
                Debug.LogWarning("No state named " + state);
                break;
        }
    }

    void IdleMove(){
    }

    void PanicMove(){
        moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
        navAgent.destination = new Vector3(moveDirection.x, 0, moveDirection.z);
        target = navAgent.destination;
    }

    void NervousMove(){
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
