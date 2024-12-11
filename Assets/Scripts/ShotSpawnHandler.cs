using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShotSpawnHandler : MonoBehaviour
{
    public RectTransform launchPoint;
    private RectTransform moveParent;
    private RectTransform shotParent;

    public BubblePopGameMgr bubblePopGameMgr;

    public UnityEvent OnShotSpawned;

    public RectTransform topLeftCorner;
    public RectTransform bottomRightCorner;

    public void SpawnShot()
    {
        GameObject spawnedShot = Instantiate(bubblePopGameMgr.GetCurrentBubblePrefab(), launchPoint.transform.position, Quaternion.identity, transform.parent.transform);
        spawnedShot.transform.localScale = Vector3.one;

        BubbleMovement shotMovement = spawnedShot.GetComponent<BubbleMovement>();
        shotMovement.enabled = true;
        shotMovement.SetReferences(topLeftCorner, bottomRightCorner);
        shotMovement.StartMovement(launchPoint.transform.up);

        spawnedShot.GetComponent<ColoredBubble>().bubbleParent = shotParent;

        spawnedShot.GetComponent<ColoredBubble>().isBubbleShot = true;
        
        spawnedShot.transform.SetParent(moveParent);
        OnShotSpawned?.Invoke();
    }

    public void SetShotParent(RectTransform rectTransform)
    {
        shotParent = rectTransform;
    }
    public void SetMoveParent(RectTransform rectTransform)
    {
        moveParent = rectTransform;
    }
}
