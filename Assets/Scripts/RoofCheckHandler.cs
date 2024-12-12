using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles checking if bubbles are connected to the roof and updating their state accordingly
public class RoofCheckHandler : MonoBehaviour
{
    public string roofTag = "Roof";
    public RectTransform endPoint;

    public bool isTouchingRoof = false;
    public bool isConnectedToRoof = false;

    private List<GameObject> bubblesVisited = new List<GameObject>();

    private void OnEnable()
    {
        ColoredBubble.OnBubblePopped += NeighborPop;
    }
    private void OnDisable()
    {
        ColoredBubble.OnBubblePopped -= NeighborPop;
    }
    // Check if this bubble is immediately touching the roof
    public void CheckForImmediateRoof()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, endPoint.position);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag(roofTag))
            {
                Debug.DrawLine(transform.position, hit.point, Color.yellow, 20);
                isTouchingRoof = true;
                isConnectedToRoof = true;
                break;
            }
        }
    }
    // Update records and check if this bubble is still connected to the roof when a neighbor is destroyed, either popped or dropped
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
    // Perform a depth-first search (DFS) to find if this bubble or its neighbors are touching the roof
    public void FindRoof(List<GameObject> neighbors)
    {
        if (isTouchingRoof) return;

        isConnectedToRoof = false;

        bubblesVisited.Clear();
        SearchNeighborsForRoof(this); // Start search
        
        if (isConnectedToRoof) // // Once the roof is found, reset everything for the next time a search is performed
        {
            isConnectedToRoof = false;
        }
        else // If no roof connection is found then trigger bubble drop
        {
            isConnectedToRoof = false;
            GetComponent<ColoredBubble>().DropBubble();

            gameObject.SetActive(false);
        }

    }

    // Recursively search through neighbors to check for connection to the roof
    public void SearchNeighborsForRoof(RoofCheckHandler origin)
    {
        if (!origin.GetComponent<RoofCheckHandler>().bubblesVisited.Contains(gameObject))
        {
            origin.GetComponent<RoofCheckHandler>().bubblesVisited.Add(gameObject);
        }

        // Roof is found, ending search
        if (isTouchingRoof) 
        {
            origin.isConnectedToRoof = true;
            return;
        }

        // Roof is NOT found, so search if neighbors are touching the roof
        foreach (GameObject neighbor in GetComponent<ColoredBubble>().allNeighbors)
        {
            if (neighbor == null) continue;

            RoofCheckHandler neighborChecker = neighbor.GetComponent<RoofCheckHandler>();
            if (!origin.GetComponent<RoofCheckHandler>().bubblesVisited.Contains(neighbor)
                && neighbor != null)
            {
                neighborChecker.SearchNeighborsForRoof(origin);
            }
        }
    }
}
