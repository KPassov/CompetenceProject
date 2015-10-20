using UnityEngine;
using System.Collections;

public class FleeingScript : MonoBehaviour {

    GameObject player;

    Vector3 moveDirection;
    NavMeshAgent navAgent;

	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        if ((player.transform.position - transform.position).magnitude < 8f) {
            if ((player.transform.position - transform.position).magnitude < 4f)
            {
                MoveAwayFast();
            } else
            {
                MoveAway();
            }
        }
    }

    void MoveAway()
    {
        if (nearPosition() && navAgent.enabled == true)
        {
            moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 5f;
            navAgent.SetDestination(new Vector3(moveDirection.x, 0, moveDirection.z));
        }
    }

    bool nearPosition()
    {
        return (navAgent.destination - transform.position).magnitude < 1f;
    }

    void MoveAwayFast()
    {
        if(navAgent.enabled == true) {
            moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
            navAgent.SetDestination(new Vector3(moveDirection.x, 0, moveDirection.z));
        }
    }



    // Killing code
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
            DecayAndDestroy();
    }

    protected void DecayAndDestroy()
    {
        GetComponent<Collider>().enabled = false;
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
