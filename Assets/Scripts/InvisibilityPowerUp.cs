﻿using UnityEngine;
using System.Collections;

public class InvisibilityPowerUp : GenericPowerUp {


	#pragma warning disable 0114
	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		base.OnTriggerEnter(other);
		if(other.gameObject.tag == "Player"){
			//other.gameObject.GetComponent<PlayerControl>().;
		}
	}
	#pragma warning restore 0114

}