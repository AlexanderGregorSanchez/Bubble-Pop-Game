using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredBubble : MonoBehaviour
{
    public BubbleColors bubbleColor;

    public static Action<GameObject> OnBubbleSpawned;
    public static Action<GameObject> OnBubblePopped;

    private List<GameObject> matchingNeighbors = new List<GameObject>();
    private List<GameObject> nonMatchingNeighbors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        OnBubbleSpawned?.Invoke(gameObject);
    }
    private void OnDisable()
    {
        OnBubblePopped?.Invoke(gameObject);
    }

    public void SortNeighbors(List<GameObject> neighbors)
    {
        foreach (GameObject neighbor in neighbors)
        {
            ColoredBubble bubble = neighbor.GetComponent<ColoredBubble>();
            if (bubble.bubbleColor == bubbleColor
                && !matchingNeighbors.Contains(neighbor))
            {
                matchingNeighbors.Add(neighbor);
            }
            else if (bubble.bubbleColor != bubbleColor
                && !matchingNeighbors.Contains(neighbor))
            {
                nonMatchingNeighbors.Add(neighbor);
            }
            else
            {
                print("Something fucked up and neighbor passed by all checks in: ColoredBubble -> SortNeighbors() in " + name + " GameObject");
            }
        }
    }
}
