using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    int currentScore = 0;

    public UnityEvent<int> OnScoreUpdated;
    [SerializeField] private int pointsPerBubble = 100;
    [SerializeField] private float dropMultiplier = 1.5f;


    private void OnEnable()
    {
        ColoredBubble.OnBubbleSpawned += OnBubbleSpawn;
        ColoredBubble.OnBubblePopped += OnBubblePopped;
        ColoredBubble.OnBubblePopped += UpdateScore;
    }
    private void OnDisable()
    {
        ColoredBubble.OnBubbleSpawned -= OnBubbleSpawn;
        ColoredBubble.OnBubblePopped -= OnBubblePopped;
        ColoredBubble.OnBubblePopped -= UpdateScore;
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

    private void UpdateScore(GameObject obj)
    {
        float pointsEarned = pointsPerBubble;
        if (obj.GetComponent<ColoredBubble>().isDropped)
        {
            pointsEarned *= dropMultiplier;
        }

        currentScore += (int)pointsEarned;

        OnScoreUpdated?.Invoke(currentScore);
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

        CheckBubbleIndex();
    }

    private void CheckBubbleIndex()
    {
        if (currentBubbleIndex >= possibleBubblePrefabs.Count)
        {
            if (nextBubbleIndex < possibleBubblePrefabs.Count)
            {
                AdvanceBubbleIndex(false);
            }
            else
            {
                currentBubbleIndex = GetRandomPossibleBubblePrefabsIndex();
            }
        }

        if (nextBubbleIndex >= possibleBubblePrefabs.Count)
        {
            nextBubbleIndex = GetRandomPossibleBubblePrefabsIndex();
        }
    }

    private int GetRandomPossibleBubblePrefabsIndex()
    {
        return Random.Range(0, possibleBubblePrefabs.Count);
    }

    public void AdvanceBubbleIndex(bool status)
    {
        if (status) return;
        currentBubbleIndex = nextBubbleIndex;
        nextBubbleIndex = GetRandomPossibleBubblePrefabsIndex();
        //print($"<color=yellow>Current Bubble Index:</color> {currentBubbleIndex} \n <color=yellow>Next Bubble Index:</color> {nextBubbleIndex}");
    }

    public Color GetCurrentBubbleColor()
    {
        //print($"<color=yellow>Current Bubble Index:</color> {currentBubbleIndex} <color=purple> Possible Bubble Prefabs Count:</color> {possibleBubblePrefabs.Count}");
        if (possibleBubblePrefabs.Count > 0)
        {
            return possibleBubblePrefabs[currentBubbleIndex].GetComponentInChildren<Image>().color;
        }


        return Color.clear;
    }
    public Color GetNextBubbleColor()
    {
        //print($"<color=yellow>Next Bubble Index:</color> {nextBubbleIndex} <color=purple> Possible Bubble Prefabs Count:</color> {possibleBubblePrefabs.Count}");
        if (possibleBubblePrefabs.Count > 0)
        {
            return possibleBubblePrefabs[nextBubbleIndex].GetComponentInChildren<Image>().color;
        }

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
