using UnityEngine;
using System.Collections;

public class PanicCollider : MonoBehaviour {
	public GameObject npcDirector;
	private NPCDirector npcDirectorS;
	private GameObject player;

	void Start(){
		npcDirectorS = npcDirector.GetComponent<NPCDirector> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		transform.position = player.transform.position;
	}
	
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "NPC")
            npcDirectorS.NPCCollision(other.gameObject.GetComponent<GeneralAI>(), "CloseExit");
    }

	void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "NPC")
            npcDirectorS.NPCCollision(other.gameObject.GetComponent<GeneralAI>(), "CloseEnter");
    }
}