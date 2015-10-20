using UnityEngine;
using System.Collections;

public class BombSite : MonoBehaviour {

    void OnTriggerEnter(Collider other) { 
        if(other.gameObject.tag == "NPC" && other.GetComponent<BombAI>() != null)
        {
            //print("Bomb has been Planted!");
            // Plant bomb
            other.gameObject.SetActive(false);
            GameObject.Instantiate(Resources.Load("Prefabs/NPCs/FleeingNPC"), other.transform.position, other.transform.rotation);
        }
    }
}