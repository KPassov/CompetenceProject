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
        StartCoroutine(IdleMovement());
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

    IEnumerator IdleMovement()
    {
        while (true)
        {
            float chance = Mathf.Max(npcs.Count * 10, 100);
            if (chance >= Random.Range(0, 100))
            {
                print("Idling");
                npcs[Random.Range(0, npcs.Count - 1)].Action("Idle");
            }
            yield return new WaitForSeconds(Mathf.Max(0.2f, npcs.Count / 4f));
        }
    }
}
