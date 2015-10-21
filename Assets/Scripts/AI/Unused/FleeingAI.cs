using UnityEngine;
using System.Collections;
using System;

public class FleeingAI : GeneralAI
{
    IEnumerator fleeingCo;
    bool fleeing = false;
    
    protected override void CloseEnter()
    {
        if (!fleeing)
        {
            print("Starting new");
            fleeing = true;
            fleeingCo = Flee();
            StartCoroutine(fleeingCo);
        }
    }

    protected override void CloseExit()
    {
        if (fleeing)
        {
            print("Stopping");
            fleeing = false;
            StopCoroutine(fleeingCo);
            navAgent.enabled = true;
            navAgent.ResetPath();
        }
    }

    protected override void Touched()
    {
        Kill();
    }

    protected override void Kill()
    {
        DecayAndDestroy();
    }

    IEnumerator Flee()
    {
        while (true) {
            navAgent.enabled = false;
            navAgent.enabled = true;
            moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
            navAgent.SetDestination(new Vector3(moveDirection.x, 0, moveDirection.z));
            yield return new WaitForSeconds(1f);
        }
    }
}
