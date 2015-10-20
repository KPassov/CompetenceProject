using UnityEngine;
using System.Collections;

public abstract class GeneralAI : MonoBehaviour {

	protected NavMeshAgent navAgent;
	protected GameObject player;

	protected Vector3 moveDirection;

	void Awake () {
    	navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player")
			Touched ();
	}
    
    public void Action(string state){
        print(state);
		switch(state){
            case "CloseEnter":
				CloseEnter();
                break;
            case "CloseExit":
                CloseExit();
                break;
            case "Kill":
				Kill ();
				break;
            default:
                Debug.LogWarning("No state named " + state);
                break;
        }
    }

    abstract protected void CloseEnter();
    abstract protected void CloseExit();
    abstract protected void Touched();
    abstract protected void Kill();

    protected bool nearTarget(Vector3 target, Vector3 position)
    {
        return (target - position).magnitude < 1.5f;
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
			gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f,transform.position.z);
			yield return new WaitForEndOfFrame();
		}
		gameObject.SetActive(false);
	}
}
