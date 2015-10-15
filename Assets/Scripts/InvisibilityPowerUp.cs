using UnityEngine;
using System.Collections;

public class InvisibilityPowerUp : GenericPowerUp {
	
	// Use this for initialization
#pragma warning disable 0114 
	void Start () {
		base.Start();
		shader1 = Shader.Find("Standard");
	}
#pragma warning restore 0114
	
	// Update is called once per frame
	void Update () {
	
	}
}
