using UnityEngine;
using System.Collections;

public class BombAI : GeneralAI {

    GameObject[] bombSites;

    void Start()
    {
        bombSites = GameObject.FindGameObjectsWithTag("BombSite");
        moveRandomBombSite();
    }

    void moveRandomBombSite()
    {
        navAgent.SetDestination(bombSites[Random.Range(0, 2)].transform.position);
    }

    protected override void Touched()
    {
        GameObject.Instantiate(Resources.Load("Prefabs/Bomb"), this.transform.position + new Vector3(0f,1f,0f), this.transform.rotation);
        Kill();
    }

    protected override void CloseEnter()
    {
        moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
        navAgent.SetDestination(new Vector3(moveDirection.x, 0, moveDirection.z));
    }

    protected override void CloseExit()
    {
        moveRandomBombSite();
    }

    protected override void Kill()
    {
        DecayAndDestroy();
    }
}
