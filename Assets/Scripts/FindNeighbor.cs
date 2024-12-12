using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This script finds the immediate neighbors of a bubble on the grid
public class FindNeighbor : MonoBehaviour
{
    public string bubbleTag = "Bubble";
    public List<RectTransform> directionalEndPoints = new List<RectTransform>();
    private List<GameObject> immediateNeighbors = new List<GameObject>();

    // Event triggered when all neighbors are found, also to pass on the list of neighbors
    public UnityEvent<List<GameObject>> OnAllNeighborsFound;

    public void FindImmediateNeighbors()
    {
        StartCoroutine(CheckNeighbors());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator CheckNeighbors()
    {
        // Record previous list of neighbors to see if there are new neighbors
        List<GameObject> tempNeighbors = new List<GameObject>(immediateNeighbors);

        immediateNeighbors.Clear();
        // Search for neighbors atleast twice to make sure all neighbors are found
        // The search loops until there are no new bubbles found after 2 consecutive loops
        for (int i = 0; i < 2; i++)
        {
            foreach (RectTransform entry in directionalEndPoints)
            {
                RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, entry.position);

                foreach (RaycastHit2D hit in hits)
                {
                    // Only new and valid neighboring bubbles found are recorded
                    if (hit.transform.gameObject != gameObject
                        && hit.transform.CompareTag(bubbleTag)
                        && !immediateNeighbors.Contains(hit.transform.gameObject))
                    {
                        GameObject neighbor = hit.transform.gameObject;
                        Debug.DrawLine(transform.position, neighbor.transform.position, Color.magenta, 20);
                        immediateNeighbors.Add(neighbor);

                        i = 0; // Reset the loop count to ensure a thorough search

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.01f); // Small delay between checks to allow for things to "settle down"
        }
        // If new neighbors are found, update their neighbor lists
        if (immediateNeighbors.Count > tempNeighbors.Count)
        {
            foreach(GameObject neighbor in immediateNeighbors)
            {
                if (neighbor == null) // In case the neighbor get deleted for some reason
                { 
                    continue; 
                }
                else if (!tempNeighbors.Contains(neighbor) )
                {
                    // Get the new neighbor to search for neighbors so its list is updated with this bubble
                    neighbor.GetComponent<FindNeighbor>().FindImmediateNeighbors();  
                }
            }
        }

        OnAllNeighborsFound?.Invoke(immediateNeighbors);
    }
}
