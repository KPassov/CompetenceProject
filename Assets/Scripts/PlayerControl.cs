using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Rigidbody rb;
	Renderer rend;
    public float thrust = 500f;
    public float maxSpeed = 10f;

	public GameObject grenade;
	public GameObject explosion;

	AudioController sfx;

	Material pacman;
	Material pacmanBomb;

	public Material mat;

	void Awake () {
        rb = GetComponent<Rigidbody>();	
		rend = GetComponent<Renderer>();
		grenade.SetActive(false);
		explosion.SetActive(false);

		sfx = GameObject.FindGameObjectWithTag ("SFXController").GetComponent<AudioController> ();

		pacman = Resources.Load("Materials/pacman", typeof(Material)) as Material;
		pacmanBomb = Resources.Load("Materials/pacmanbomb", typeof(Material)) as Material;

		ShowAsBomb (false);
	}
	
	void Update () {
        if(Input.GetKey("down")){
            rb.AddForce(Vector3.back * thrust);
        } 
        if(Input.GetKey("up")){
            rb.AddForce(Vector3.forward * thrust);
        }  
        if(Input.GetKey("right")){
            rb.AddForce(Vector3.right * thrust);    
        }
        if(Input.GetKey("left")){
            rb.AddForce(Vector3.left * thrust);    
        }
		if(Input.GetKeyDown("space")){
			var inventoryController = gameObject.GetComponent<PlayerInventoryController>();
			if (inventoryController.bombsInInventory > 0){
				GameObject bomb = Resources.Load("Prefabs/Bomb", typeof(GameObject)) as GameObject;
				Instantiate(bomb,transform.position,Quaternion.identity);
				inventoryController.bombsInInventory--;
			}
		}
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
                                  Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed),
                                  Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
	}

	public void ShowAsBomb(bool show){
		if (show) {
			grenade.SetActive(true);
			rend.material = pacmanBomb;
			StartCoroutine(Explode());
		} else {
			grenade.SetActive(false);
			//explosion.SetActive(false);
			rend.material = pacman;
		}
	}

	IEnumerator Explode(){
		yield return new WaitForSeconds (3.0f);
		sfx.playSound ("c4_explode1");
		explosion.SetActive (true);
		Hashtable payload = new Hashtable();
		payload["explosion"] = explosion;
		NotificationCenter.DefaultCenter.PostNotification(this, "Explode",payload);
		ShowAsBomb (false);
		yield return new WaitForSeconds (1.0f);
		explosion.SetActive (false);

	}
}
