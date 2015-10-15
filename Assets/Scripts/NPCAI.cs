using UnityEngine;
using System.Collections;

public class NPCAI : MonoBehaviour {

    NavMeshAgent navAgent;
    GameObject player;

    Vector3 moveDirection;
    Vector3 target;
    bool playerInvis = false;

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            // Die();
            gameObject.SetActive(false);
        }
    }

    // void Die(){
            // gameObject.GetComponentInChildren<ParticleSystem>().Play();
            // gameObject.SetActive(false);
    // }

    // IEnumerator DieIn(float seconds){
        
    // }

	void Awake () {
    	navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = transform.position;
        NotificationCenter.DefaultCenter.AddObserver(this, "InvisibilityTriggered");       
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

    public void InvisibilityTriggered(NotificationCenter.Notification notif){
        print("Invisible!");
        Hashtable payload = notif.data;
        StartCoroutine(Invis((float)payload["duration"]));
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
