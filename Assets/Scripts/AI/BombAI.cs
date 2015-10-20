using UnityEngine;
using System.Collections;

public class BombAI : GeneralAI {

    GameObject[] bombSites;

    void Start()
    {
        bombSites = GameObject.FindGameObjectsWithTag("BombSite");
        moveRandomBombSite();
    }
    
    override protected void CloseMove()
    {
        moveRandomBombSite();
    }

    protected override void NoClose()
    {
        moveRandomBombSite();
    }

    void moveRandomBombSite()
    {
        navAgent.SetDestination(bombSites[Random.Range(0, 2)].transform.position);
    }

    protected override void Touched()
    {
        GameObject.Instantiate(Resources.Load("Prefabs/Bomb"), this.transform.position + new Vector3(0f,1f,0f), this.transform.rotation);
        base.Touched();
    }
}
