using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BubbleColors
{
    None,
    Red,
    Green,
    Blue,
    Yellow,
    Purple,
    Teal
}
public class BubblePopGameMgr : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<BubbleColors, GameObject> bubblePrefabs;
    private List<GameObject> activeBubbles = new List<GameObject>();
    private List<BubbleColors> activeColors = new List<BubbleColors>();

    public List<GameObject> possibleBubblePrefabs = new List<GameObject>();
    public int currentBubbleIndex = 0;
    int nextBubbleIndex = 0;

    private void OnEnable()
    {
        ColoredBubble.OnBubbleSpawned += OnBubbleSpawn;
        ColoredBubble.OnBubblePopped += OnBubblePopped;
    }
    private void OnDisable()
    {
        ColoredBubble.OnBubbleSpawned -= OnBubbleSpawn;
        ColoredBubble.OnBubblePopped -= OnBubblePopped;
    }

    private void OnBubbleSpawn(GameObject obj)
    {
        if (obj.GetComponent<ColoredBubble>().bubbleColor != BubbleColors.None)
        {
            activeBubbles.Add(obj);
            UpdateActiveColors();
        }
    }

    private void OnBubblePopped(GameObject obj)
    {
        if (obj.GetComponent<ColoredBubble>().bubbleColor != BubbleColors.None)
        {
            activeBubbles.Remove(obj);
            UpdateActiveColors();
        }
    }

    private void UpdateActiveColors()
    {
        List<BubbleColors> currentActiveColors = new List<BubbleColors>();
        foreach (GameObject obj in activeBubbles)
        {
            BubbleColors currentColor = obj.GetComponent<ColoredBubble>().bubbleColor;
            if (!currentActiveColors.Contains(currentColor)
                && currentColor != BubbleColors.None)
            {
                currentActiveColors.Add(currentColor);
            }
        }

        activeColors.RemoveAll(color => !currentActiveColors.Contains(color));

        foreach (var color in currentActiveColors)
        {
            if (!activeColors.Contains(color)
                && color != BubbleColors.None)
            {
                activeColors.Add(color);
            }
        }

        UpdatePossibleBubblePrefabs();
    }

    private void UpdatePossibleBubblePrefabs()
    {
        // Create a new list to store the updated possibleBubblePrefabs
        List<GameObject> updatedPossibleBubblePrefabs = new List<GameObject>();
        // Add GameObject prefabs to the updated list based on activeColors
        foreach (BubbleColors color in activeColors)
        {
            if (bubblePrefabs.ContainsKey(color))
            {
                GameObject prefab = bubblePrefabs[color];
                if (!updatedPossibleBubblePrefabs.Contains(prefab))
                {
                    updatedPossibleBubblePrefabs.Add(prefab);
                }
            }
        }
        // Assign the updated list to possibleBubblePrefabs
        possibleBubblePrefabs = updatedPossibleBubblePrefabs;
    }
    public void AdvanceBubbleIndex(bool status)
    {
        if (status) return;

        currentBubbleIndex = nextBubbleIndex;
        nextBubbleIndex = Random.Range(0, possibleBubblePrefabs.Count);
    }

    public Color GetCurrentBubbleColor()
    {
        if (possibleBubblePrefabs.Count > 0)
            return possibleBubblePrefabs[currentBubbleIndex].GetComponentInChildren<Image>().color;

        return Color.clear;
    }
    public Color GetNextBubbleColor()
    {
        if (possibleBubblePrefabs.Count > 0)
            return possibleBubblePrefabs[nextBubbleIndex].GetComponentInChildren<Image>().color;

        return Color.clear;
    }
    public GameObject GetCurrentBubblePrefab()
    {
        return possibleBubblePrefabs[currentBubbleIndex];
    }
    public GameObject GetNextBubblePrefab()
    {
        return possibleBubblePrefabs[nextBubbleIndex];
    }
}
