using UnityEngine;
using System.Collections;

public class NervousCollider : MonoBehaviour {
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
        if (other.gameObject.tag == "NPC")
            npcDirectorS.NPCCollision(other.gameObject.GetComponent<GeneralAI>(), "NoLongerClose");
    }

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "NPC" && (other.gameObject.transform.position - player.transform.position).magnitude > 4f) {
			npcDirectorS.NPCCollision (other.gameObject.GetComponent<GeneralAI>(), "Close");
		}
	}
}