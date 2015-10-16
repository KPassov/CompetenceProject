using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDirector : MonoBehaviour {

	List<NPCAI> npcs = new List<NPCAI> ();

	float defaultNPCSpeed = 12f;
	bool playerInvis = false;

	void Start(){
		foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC")) {
			npcs.Add(npc.GetComponent<NPCAI>());
		}
		NotificationCenter.DefaultCenter.AddObserver(this, "InvisibilityTriggered");  
		NotificationCenter.DefaultCenter.AddObserver(this, "FreezeTriggered");  
	}

	public void NPCCollision(NPCAI npc, string state){
		if (!playerInvis) {
			npc.Move (state);
		}
	}
	
	public void InvisibilityTriggered(NotificationCenter.Notification notif){
		Hashtable payload = notif.data;
		StartCoroutine(Invis((float)payload["duration"]));
	}

	public void FreezeTriggered(NotificationCenter.Notification notif){
		Hashtable payload = notif.data;
		StartCoroutine(Freeze((float)payload["duration"]));
	}

	IEnumerator Invis(float seconds){
		playerInvis = true;
		yield return new WaitForSeconds(seconds); 
		playerInvis = false;
	}

	IEnumerator Freeze(float seconds){
		foreach (NPCAI npc in npcs) {
			npc.ChangeSpeed(0f);
		}
		yield return new WaitForSeconds(seconds); 
		foreach (NPCAI npc in npcs) {
			npc.ChangeSpeed(defaultNPCSpeed);
		}
	}
}
