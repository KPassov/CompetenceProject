using UnityEngine;
using System.Collections;

public class BomberScript : MonoBehaviour
{

    GameObject player;

    Vector3 moveDirection;
    NavMeshAgent navAgent;
    Vector3 bombSite;
	bool playerInvis;
	GUIScript gui;

    void Start()
	{
		gui = GameObject.FindGameObjectWithTag ("GUI").GetComponent<GUIScript>();
        bombSite = GameObject.FindGameObjectsWithTag("BombSite")[Random.Range(0, 2)].transform.position;
        navAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
		NotificationCenter.DefaultCenter.AddObserver(this, "InvisibilityTriggered");
    }

    void Update()
    {
        if ((player.transform.position - transform.position).magnitude < 4f && !playerInvis)
			MoveAwayFast ();
		else {
			if(navAgent.enabled == true)
				navAgent.SetDestination (bombSite);
		}
    }

    void MoveAwayFast()
    {
        if (navAgent.enabled == true)
        {
            moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
            navAgent.SetDestination(new Vector3(moveDirection.x, 0, moveDirection.z));
        }
    }


	
	void InvisibilityTriggered(NotificationCenter.Notification notif){
		Hashtable payload = notif.data;
		StartCoroutine(Invis((float)payload["duration"]));
	}
	
	IEnumerator Invis(float seconds){
		playerInvis = true;
		yield return new WaitForSeconds(seconds); 
		playerInvis = false;
	}
    // Killing code
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {
            GameObject.Instantiate(Resources.Load("Prefabs/Bomb"), new Vector3(this.transform.position.x, 1f, this.transform.position.z), this.transform.rotation);
            DecayAndDestroy();
        }
    }

    protected void DecayAndDestroy()
    {
        GetComponent<Collider>().enabled = false;
		gui.IncreaseHighScore (100);
		GetComponent<AudioSource> ().Play ();
        StartCoroutine(ParticlesFor(1.5f));
        StartCoroutine(Sink(2f));
    }

    private IEnumerator ParticlesFor(float seconds)
    {
        ParticleSystem parSystem = GetComponentInChildren<ParticleSystem>();
        if (parSystem != null)
        {
            parSystem.Play();
            yield return new WaitForSeconds(seconds);
            parSystem.Stop();
        }
    }

    private IEnumerator Sink(float seconds)
    {
        float stopTime = Time.time + seconds;
        navAgent.enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        while (stopTime > Time.time)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
}
