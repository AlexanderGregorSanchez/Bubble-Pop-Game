using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColoredBubble : MonoBehaviour
{
    public BubbleColors bubbleColor;

    public static Action<GameObject> OnBubbleSpawned;
    public static Action<GameObject> OnBubblePopped;

    public List<GameObject> allNeighbors;
    private List<GameObject> matchingNeighbors = new List<GameObject>();
    private List<GameObject> nonMatchingNeighbors = new List<GameObject>();

    public bool isBubbleShot = false;

    public List<GameObject> colorGroup = new List<GameObject>();
    [SerializeField] private int minGroupSize = 3;

    public UnityEvent<Sprite> OnBubbleDrop;

    public bool isDropped { get; private set; } = false;
    public bool isBurst { get; private set; } = false;

    private void Start()
    {
        OnBubbleSpawned?.Invoke(gameObject);
    }

    private void DestroyBubble()
    {
        OnBubblePopped?.Invoke(gameObject);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SortNeighbors(List<GameObject> neighbors)
    {
        allNeighbors.Clear();
        matchingNeighbors.Clear();
        nonMatchingNeighbors.Clear();

        allNeighbors = new List<GameObject>(neighbors);

        foreach (GameObject neighbor in neighbors)
        {
            if (neighbor == null)
            {
                continue;
            }
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
                Debug.LogWarning($"Categorizing neighbors failed: neighbor passed by all checks in: ColoredBubble.cs -> SortNeighbors() in {name} GameObject");
            }
        }


        if (isBubbleShot)
        {
            isBubbleShot = false;
            FindBubbleGroup();
        }
    }

    public void FindBubbleGroup()
    {
        colorGroup.Clear();
        AddToColorGroup(this);

        if (colorGroup.Count >= minGroupSize)
        {
            BurstBubbleGroup();
        }
    }

    public void AddToColorGroup(ColoredBubble origin)
    {
        if (!origin.colorGroup.Contains(gameObject))
        {
            origin.colorGroup.Add(gameObject);
        }

        foreach (GameObject neighbor in matchingNeighbors)
        {
            ColoredBubble neighborBubble = neighbor.GetComponent<ColoredBubble>();
            if (neighborBubble != null && neighborBubble.bubbleColor == bubbleColor && !origin.colorGroup.Contains(neighbor))
            {
                neighborBubble.AddToColorGroup(origin);
            }
        }
    }

    public void BurstBubbleGroup()
    {
        foreach (GameObject members in colorGroup)
        {
            members.GetComponent<ColoredBubble>().isBurst = true;
        }
        foreach (GameObject members in colorGroup)
        {
            members.GetComponent<ColoredBubble>().DestroyBubble();
        }
    }

    public void DropBubble()
    {
        if (isBurst) { return; }
        isDropped = true;
        OnBubbleDrop?.Invoke(GetComponentInChildren<Image>().sprite);

        DestroyBubble();
    }
}
