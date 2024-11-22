using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShotSpawnHandler : MonoBehaviour
{
    public RectTransform launchPoint;
    private RectTransform shotParent;

    public BubblePopGameMgr bubblePopGameMgr;

    public UnityEvent OnShotSpawned;

    public void SpawnShot()
    {
        GameObject spawnedShot = Instantiate(bubblePopGameMgr.GetCurrentBubblePrefab(), launchPoint.transform.position, Quaternion.identity, transform.parent.transform);
        spawnedShot.transform.localScale = Vector3.one;

        spawnedShot.GetComponent<BubbleMovement>().StartMovement(launchPoint.transform.up);
        spawnedShot.GetComponent<ColoredBubble>().isBubbleShot = true;
        
        spawnedShot.transform.SetParent(shotParent);
        OnShotSpawned?.Invoke();
    }

    public void SetShotParent(RectTransform rectTransform)
    {
        shotParent = rectTransform;
    }
}
