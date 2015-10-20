using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDirector : MonoBehaviour {

	List<GeneralAI> npcs = new List<GeneralAI> ();

	bool playerInvis = false;

	void Start(){
		foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC")) {
			npcs.Add(npc.GetComponent<GeneralAI>());
		}
		NotificationCenter.DefaultCenter.AddObserver(this, "InvisibilityTriggered");
	}

	public void NPCCollision(GeneralAI npc, string state){
		if (!playerInvis) {
			npc.Action (state);
		}
	}
	
	public void InvisibilityTriggered(NotificationCenter.Notification notif){
		Hashtable payload = notif.data;
		StartCoroutine(Invis((float)payload["duration"]));
	}

	IEnumerator Invis(float seconds){
		playerInvis = true;
		yield return new WaitForSeconds(seconds); 
		playerInvis = false;
	}
}
