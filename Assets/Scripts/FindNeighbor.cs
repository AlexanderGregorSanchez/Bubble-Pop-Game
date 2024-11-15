using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindNeighbor : MonoBehaviour
{

    private List<GameObject> immediateNeighbors = new List<GameObject>();

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
        List<Vector3> directions = new List<Vector3>
        {
            Vector3.up + -(Vector3.right / 2),
            Vector3.up + (Vector3.right / 2),
            Vector3.right,
            -Vector3.up + (Vector3.right / 2),
            -Vector3.up + -(Vector3.right / 2),
            -Vector3.right
        };

        List<GameObject> tempNeighbors = new List<GameObject>(immediateNeighbors);

        immediateNeighbors.Clear();

        for (int i = 0; i < 4; i++)
        {
            foreach (var entry in directions)
            {
                RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position + entry * 5, transform.position + entry * 10);
                //Debug.DrawLine(transform.position + entry * 5, transform.position + entry * 10, Color.yellow, 500);

                foreach (RaycastHit2D hit in hits)
                {
                    if (Vector2.Distance(transform.position, hit.transform.position) > 20)
                    {
                        continue;
                    }
                    else if (hit.transform.gameObject != gameObject
                        && hit.transform.CompareTag("Bubble")
                        && !immediateNeighbors.Contains(hit.transform.gameObject))
                    {
                        GameObject neighbor = hit.transform.gameObject;
                        Debug.DrawLine(transform.position, neighbor.transform.position, Color.magenta, 500);
                        immediateNeighbors.Add(neighbor);

                        i = 0;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

        if (immediateNeighbors.Count > tempNeighbors.Count)
        {
            foreach(GameObject neighbor in immediateNeighbors)
            {
                if (neighbor == null) 
                { 
                    continue; 
                }
                else if (!tempNeighbors.Contains(neighbor) )
                {
                    neighbor.GetComponent<FindNeighbor>().FindImmediateNeighbors();
                }
            }
        }

        OnAllNeighborsFound?.Invoke(immediateNeighbors);
    }
}
