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


    virtual protected void CloseMove()
    {
        if (nearTarget(target, transform.position))
        {
            moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
            navAgent.destination = new Vector3(moveDirection.x, 0, moveDirection.z);
            target = navAgent.destination;
        }
    }

    virtual protected void VeryCloseMove ()
    {
        moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
        navAgent.destination = new Vector3(moveDirection.x, 0, moveDirection.z);
        target = navAgent.destination;
    }

    virtual protected void IdleMove ()
    {
        navAgent.destination = transform.position + new Vector3(Random.Range(-2,2), 0f, Random.Range(-2,2));
        target = navAgent.destination;
    }

    virtual protected void Touched ()
    {

    }

    virtual protected void Kill()
    {

    }

    protected bool nearTarget(Vector3 target, Vector3 position){
        return (target - position).magnitude < 1f;
    }

	protected void DecayAndDestroy(){
        GetComponent<Collider>().enabled = false;
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
