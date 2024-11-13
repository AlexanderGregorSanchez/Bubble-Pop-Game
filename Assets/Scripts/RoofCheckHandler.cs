using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RoofCheckHandler : MonoBehaviour
{
    public bool isTouchingRoof = false;
    public bool isConnectedToRoof = false;

    private void OnEnable()
    {
        ColoredBubble.OnBubblePopped += NeighborPop;
    }
    private void OnDisable()
    {
        ColoredBubble.OnBubblePopped -= NeighborPop;
    }

    public void CheckForImmediateRoof()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + Vector3.up * 5, Vector3.up, 20);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Roof"))
            {
                print($"<b><color=green>{name}</color></b> is touching the roof");
                isTouchingRoof = true;
                isConnectedToRoof = true;
                break;
            }
        }
    }

    private void NeighborPop(GameObject neighbor)
    {
        List<GameObject> neighbors = new List<GameObject>(GetComponent<ColoredBubble>().allNeighbors);
        if (neighbors.Contains(null)
            || neighbors.Contains(neighbor))
        {
            GetComponent<ColoredBubble>().allNeighbors.Remove(neighbor);
            FindRoof(GetComponent<ColoredBubble>().allNeighbors);
        }
    }

    public void FindRoof(List<GameObject> neighbors)
    {
        if (isTouchingRoof) return;

        print($"<color=green>{name}</color> is finding roof");

    }

    public void AddToChain(RoofCheckHandler roofCheck)
    {

    }
}
