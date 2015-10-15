using UnityEngine;
using System.Collections;

public class InvisibilityPowerUp : GenericPowerUp {
	
	// Use this for initialization
	void Start () {
		base.Start();
		shader1 = Shader.Find("Standard");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
