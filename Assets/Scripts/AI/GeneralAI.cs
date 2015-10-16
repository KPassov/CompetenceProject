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

    public void Action(string state){
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
			case "Kill":
				Kill ();
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

	abstract protected void Kill();

    protected bool nearTarget(Vector3 target, Vector3 position){
        return (target - position).magnitude < 1f;
    }

	protected void DecayAndDestroy(){
		StartCoroutine(ParticlesFor(1.5f));
		StartCoroutine(Sink(2f));
	}

	private IEnumerator ParticlesFor(float seconds){
		ParticleSystem parSystem = GetComponentInChildren<ParticleSystem> ();
		if (parSystem != null) {
			parSystem.Play ();
			yield return new WaitForSeconds (seconds);
			parSystem.Stop ();
		}
	}

	private IEnumerator Sink(float seconds){
		float stopTime = Time.time + seconds;
		tag = "DeadNPC";
		while (stopTime > Time.time) {
			print (gameObject.transform.position);
			gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f,transform.position.z);
			yield return new WaitForEndOfFrame();
		}
		gameObject.SetActive(false);
	}
}
