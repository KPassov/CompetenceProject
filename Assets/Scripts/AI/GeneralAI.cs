using UnityEngine;
using System.Collections;

public abstract class GeneralAI : MonoBehaviour {

	protected NavMeshAgent navAgent;
	protected GameObject player;
	protected AudioController sfx;

	protected Vector3 moveDirection;

	void Awake () {
    	navAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
		sfx = GameObject.FindGameObjectWithTag("SFXController").GetComponent<AudioController>();
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
            case "NoLongerClose":
                NoClose();
                break;
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

    virtual protected void NoClose()
    {
        navAgent.ResetPath();
    }
    virtual protected void CloseMove()
    {
        if (nearTarget(navAgent.destination, transform.position))
        {
            moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
            navAgent.SetDestination(new Vector3(moveDirection.x, 0, moveDirection.z));
        }
    }

    virtual protected void VeryCloseMove ()
    {
        moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
        navAgent.SetDestination(new Vector3(moveDirection.x, 0, moveDirection.z));
    }

    virtual protected void IdleMove ()
    {
       
    }

    virtual protected void Touched ()
    {
        Kill();
    }

    virtual protected void Kill()
    {
        DecayAndDestroy();
    }

    protected bool nearTarget(Vector3 target, Vector3 position){
        return (target - position).magnitude < 1.5f;
    }

	protected void DecayAndDestroy(){
        GetComponent<Collider>().enabled = false;
		sfx.playSound ("pacman_eatfruit");
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
			gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f,transform.position.z);
			yield return new WaitForEndOfFrame();
		}
		gameObject.SetActive(false);
	}
}
