using UnityEngine;
using System.Collections;

public class BombAI : GeneralAI {

    Vector3 bombSite;

    void Start()
    {
        bombSite = GameObject.FindGameObjectsWithTag("BombSite")[Random.Range(0, 1)].transform.position;
        navAgent.SetDestination(bombSite);
    }
    
    override protected void CloseMove()
    {
        navAgent.SetDestination(bombSite);
    }

    protected override void Touched()
    {
        GameObject.Instantiate(Resources.Load("Prefabs/Bomb"), this.transform.position + new Vector3(0f,1f,0f), this.transform.rotation);
        base.Touched();
    }
}
