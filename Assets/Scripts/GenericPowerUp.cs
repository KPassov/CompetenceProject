using UnityEngine;
using System.Collections;

public class GenericPowerUp : MonoBehaviour {

	public bool spinPowerUp = true;
	public float rotationSpeed = 45.0f;

	private Renderer renderer;

	[HideInInspector] // Hides var below
	public Shader shader2;
	[HideInInspector] // Hides var below
	public Shader shader1;


	public void SuperStart(){
		renderer = GetComponent<Renderer>();
		StartCoroutine(SpinPowerUp());
		shader1 = Shader.Find("Toon/Basic");
		shader2 = Shader.Find("Toon/Basic Outline");
	}
	// Use this for initialization
	public virtual void Start () {
		SuperStart();
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator SpinPowerUp(){
		while(spinPowerUp){
			transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
			yield return null;
		}
	}

	void OnTriggerEnter(Collider other) {
		//has picked up
		if (other.gameObject.tag.Equals("Player"))
			if (renderer.material.shader == shader1)
				renderer.material.shader = shader2;
			else
				renderer.material.shader = shader1;
	}
}
