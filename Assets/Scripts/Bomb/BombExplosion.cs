using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Effects;

public class BombExplosion : MonoBehaviour {

	public float explosionForce = 20;
	public float pushRadius = 10.0f;
	public float killRadius = 5.0f;


	void Awake()
	{
		NotificationCenter.DefaultCenter.AddObserver(this,"Explode");
	}

	void Explode(NotificationCenter.Notification notif){

		Hashtable payload = notif.data;

		//check if the notification is for us
		if(payload["explosion"] != this.gameObject)
			return;

		float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;		
		//pushed NPCs contains killed NPCs
		var pushedGO = Physics.OverlapSphere(transform.position, pushRadius); 
		var killedGO = Physics.OverlapSphere(transform.position, killRadius);
		
		
		//push all the NPCs in the push radius
		var pushedRigidbodies = new List<Rigidbody>();
		foreach (var col in pushedGO)
		{
			
			if (col.attachedRigidbody != null && !pushedRigidbodies.Contains(col.attachedRigidbody))
			{
				if(col.gameObject.CompareTag("NPC") || col.gameObject.CompareTag("Player") )
					pushedRigidbodies.Add(col.attachedRigidbody);
			}
		}
		foreach (var rb in pushedRigidbodies)
		{
			rb.AddExplosionForce(explosionForce*multiplier, transform.position, pushRadius, multiplier, ForceMode.Impulse);
		}
		
		//trigger kill method on killedNPCs
		var killedRigidbodies = new List<Rigidbody>();
		foreach (var col in killedGO)
		{
			if (col.attachedRigidbody != null && !killedRigidbodies.Contains(col.attachedRigidbody))
			{
				if(col.gameObject.CompareTag("NPC") || col.gameObject.CompareTag("Player") )
					killedRigidbodies.Add(col.attachedRigidbody);
			}
		}
		foreach (var rb in killedRigidbodies)
		{

			Debug.Log(rb.gameObject.tag);

			if(rb.gameObject.CompareTag("NPC")){
				rb.gameObject.GetComponent<GeneralAI>().Action("Kill");
			}

			if(rb.gameObject.CompareTag("Player")){
				rb.gameObject.SetActive(false);
			}
		}
	}


	void OnDestroy(){
		NotificationCenter.DefaultCenter.RemoveObserver(this,"Explode");
	}
}
