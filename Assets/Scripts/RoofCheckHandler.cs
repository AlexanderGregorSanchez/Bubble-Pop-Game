using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RoofCheckHandler : MonoBehaviour
{
    public bool isTouchingRoof = false;
    public bool isConnectedToRoof = false;

    public void CheckForImmediateRoof()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + Vector3.up * 5, Vector3.up, 20);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Roof"))
            {
                print($"<b><color=#00ff00ff>{name}</color></b> is touching the roof");
                isTouchingRoof = true;
                isConnectedToRoof = true;
                break;
            }
        }
    }

    public void FindRoof(List<GameObject> neighbors)
    {
        if (isTouchingRoof) return;

        

    }

    public void AddToChain(RoofCheckHandler roofCheck)
    {

    }
}
