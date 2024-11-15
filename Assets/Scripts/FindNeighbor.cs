using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindNeighbor : MonoBehaviour
{

    private List<GameObject> immediateNeighbors = new List<GameObject>();

    public UnityEvent<List<GameObject>> OnAllNeighborsFound;

    private void OnEnable()
    {
        SnapToGrid.OnSnapToGrid += FindImmediateNeighbors;
    }
    private void OnDisable()
    {
        SnapToGrid.OnSnapToGrid -= FindImmediateNeighbors;
    }

    public void FindImmediateNeighbors()
    {
        StartCoroutine(CheckNeighbors());
    }
    IEnumerator CheckNeighbors()
    {
        List<Vector3> directions = new List<Vector3>
        {
            Vector3.up + -(Vector3.right / 2),
            Vector3.up + (Vector3.right / 2),
            Vector3.right,
            -Vector3.up + (Vector3.right / 2),
            -Vector3.up + -(Vector3.right / 2),
            -Vector3.right
        };
        immediateNeighbors.Clear();

        // Loop until no new balls found for 3 consecutive loops
        for (int i = 0; i < 4; i++)
        {
            // Linecast in all directions on grid
            foreach (var entry in directions)
            {
                RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position + entry * 5, transform.position + entry * 10);
                //Debug.DrawLine(transform.position + entry * 5, transform.position + entry * 10, Color.yellow, 500);

                foreach (RaycastHit2D hit in hits)
                {
                    // Ignore all hits with things further than immediate adjacent grid spots
                    if (Vector2.Distance(transform.position, hit.transform.position) > 20)
                    {
                        continue;
                    }
                    // Check hit object is a ball, is not self, and is not already recorded
                    else if (hit.transform.gameObject != gameObject
                        && hit.transform.CompareTag("Bubble")
                        && !immediateNeighbors.Contains(hit.transform.gameObject))
                    {
                        GameObject neighbor = hit.transform.gameObject;
                        Debug.DrawLine(transform.position, neighbor.transform.position, Color.magenta, 500);
                        immediateNeighbors.Add(neighbor); // Record ball

                        i = 0;// Reset loop iterator

                        break;// Go to next direction linecast
                    }
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

        OnAllNeighborsFound?.Invoke(immediateNeighbors);
    }
}
