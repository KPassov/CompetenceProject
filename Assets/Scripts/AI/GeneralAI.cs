using UnityEngine;
using System.Collections;

public abstract class GeneralAI : MonoBehaviour {

	protected NavMeshAgent navAgent;
	protected GameObject player;

	protected Vector3 moveDirection;
	protected Vector3 target;

	void Awake () {
    	navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = transform.position;
	}
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player")
			Touched ();
	}

    public void ChangeSpeed(float newSpeed){
        navAgent.speed = newSpeed;
    }

    public void Move(string state){
		switch(state){
            case"Close":
                CloseMove();
                break;
            case "VeryClose":
				VeryCloseMove();
                break;
            case "Idle":
                IdleMove();
                break;
            default:
                Debug.LogWarning("No state named " + state);
                break;
        }
    }


	abstract protected void CloseMove ();

	abstract protected void VeryCloseMove ();

	abstract protected void IdleMove ();

	abstract protected void Touched ();

    protected bool nearTarget(Vector3 target, Vector3 position){
        return (target - position).magnitude < 1f;
    }
}
