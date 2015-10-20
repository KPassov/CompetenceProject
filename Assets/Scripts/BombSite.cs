using UnityEngine;
using System.Collections;

public class BombSite : MonoBehaviour {

    void OnTriggerEnter(Collider other) { 
        if(other.gameObject.tag == "NPC" && other.GetComponent<BomberScript>() != null)
        {
            //print("Bomb has been Planted!");
			// Plant bomb
			GameObject.Instantiate(Resources.Load("Prefabs/TerroristBomb"), new Vector3(other.transform.position.x,1f,other.transform.position.z), other.transform.rotation);
            other.gameObject.SetActive(false);
            GameObject.Instantiate(Resources.Load("Prefabs/NPCs/FleeingNPC"), other.transform.position, other.transform.rotation);
        }
    }
}