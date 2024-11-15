using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RoofCheckHandler : MonoBehaviour
{
    public bool isTouchingRoof = false;
    public bool isConnectedToRoof = false;

    public List<GameObject> bubblesVisited = new List<GameObject>();

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
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, transform.position + Vector3.up * 10);
        //Debug.DrawLine(transform.position, transform.position + Vector3.up * 10, Color.yellow, 500);
        //print($"<b><color=green>{name}'s</color></b> roof check found {hits.Length} hits");
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Roof"))
            {
                //print($"<b><color=green>{name}</color></b> is touching the roof");
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

        isConnectedToRoof = false;

        // Ensure the roofGroup is cleared before starting the process
        bubblesVisited.Clear();
        SearchNeighborsForRoof(this);
        
        if (isConnectedToRoof)
        {
            isConnectedToRoof = false;
            //print($"<b><color=green>{name}'s</color></b> is <color=yellow>CONNECTED</color> to Roof");
            DoSomethingIfRoofFound();
        }
        else
        {
            isConnectedToRoof = false;
            //print($"<b><color=green>{name}'s</color></b> is <color=red>NOT CONNECTED</color> to Roof");
            DoSomethingIfRoofNotFound();

            gameObject.SetActive(false);
        }

    }

    public void SearchNeighborsForRoof(RoofCheckHandler origin)
    {
        if (!origin.GetComponent<RoofCheckHandler>().bubblesVisited.Contains(gameObject))
        {
            origin.GetComponent<RoofCheckHandler>().bubblesVisited.Add(gameObject);
        }

        if (isTouchingRoof)
        {
            //print($"<b><color=green>{name}'s</color></b> is <color=yellow>TOUCHING</color> to Roof");
            origin.isConnectedToRoof = true;
            return;
        }

        foreach (GameObject neighbor in GetComponent<ColoredBubble>().allNeighbors)
        {
            RoofCheckHandler neighborChecker = neighbor.GetComponent<RoofCheckHandler>();
            if (!origin.GetComponent<RoofCheckHandler>().bubblesVisited.Contains(neighbor))
            {
                neighborChecker.SearchNeighborsForRoof(origin);
            }
        }
    }

    public void DoSomethingIfRoofFound()
    {
        // Action to perform if the roof is found
    }

    public void DoSomethingIfRoofNotFound()
    {
        // Action to perform if the roof is not found

        GetComponent<ColoredBubble>().DropBubble();
    }

}
