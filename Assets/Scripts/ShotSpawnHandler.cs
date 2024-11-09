using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShotSpawnHandler : MonoBehaviour
{
    public RectTransform launchPoint;
    public RectTransform shotParent;

    public BubblePopGameMgr bubblePopGameMgr;

    public UnityEvent OnShotSpawned;

    public void SpawnBall()
    {
        GameObject spawnedBall = Instantiate(bubblePopGameMgr.GetCurrentBubblePrefab(), launchPoint.transform.position, Quaternion.identity, shotParent);
        spawnedBall.transform.localScale = Vector3.one;

        spawnedBall.GetComponent<BubbleMovement>().StartMovement(launchPoint.transform.up);

        OnShotSpawned?.Invoke();
    }
}
