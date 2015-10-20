using UnityEngine;
using System.Collections;

public class BombAI : GeneralAI {

    GameObject[] bombSites;

    void Start()
    {
        bombSites = GameObject.FindGameObjectsWithTag("BombSite");
        setRandomBombSite();
    }
    
    void setRandomBombSite()
    {
        navAgent.SetDestination(bombSites[Random.Range(0, 2)].transform.position);
    }

    override protected void CloseMove()
    {
        setRandomBombSite();
    }

    protected override void NoClose()
    {
        setRandomBombSite();
    }

    protected override void Touched()
    {
        GameObject.Instantiate(Resources.Load("Prefabs/Bomb"), this.transform.position + new Vector3(0f,1f,0f), this.transform.rotation);
        base.Touched();
    }
}
