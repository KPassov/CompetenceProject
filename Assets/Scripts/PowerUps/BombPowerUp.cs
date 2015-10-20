using UnityEngine;
using System.Collections;

public class BombPowerUp : GenericPowerUp {

	PlayerControl p;

	#pragma warning disable 0114
	// Use this for initialization
	void Start () {
		base.Start();
		p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){

			if(powerUpActive){
				p.ShowAsBomb(true);
				Hashtable payload  = new Hashtable();

				//add bomb to the inventory

				//NotificationCenter.DefaultCenter.PostNotification(this, "BombPickedUp",payload);
			}
		}
		// fire the super method
		base.OnTriggerEnter(other);
	}
	#pragma warning restore 0114
}
