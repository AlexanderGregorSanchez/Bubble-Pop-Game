using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindNeighbor : MonoBehaviour
{
    public List<RectTransform> directionalEndPoint = new List<RectTransform>();
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
        List<GameObject> tempNeighbors = new List<GameObject>(immediateNeighbors);

        immediateNeighbors.Clear();

        for (int i = 0; i < 4; i++)
        {
            foreach (var entry in directionalEndPoint)
            {
                RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, entry.position);
                //Debug.DrawLine(transform.position, entry.position, Color.yellow, 500);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.gameObject != gameObject
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
