using UnityEngine;
using System.Collections;

public class BombSite : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPCBomb")
        {

        }
    }
}
