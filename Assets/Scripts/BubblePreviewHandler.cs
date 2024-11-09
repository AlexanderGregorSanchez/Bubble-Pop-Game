using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubblePreviewHandler : MonoBehaviour
{
    public BubblePopGameMgr bubblePopGameMgr;
    [SerializeField] private Image currentBubbleImage;
    [SerializeField] private Image nextBubbleImage;

    private void FixedUpdate()
    {
        if (currentBubbleImage)
            currentBubbleImage.color = bubblePopGameMgr.GetCurrentBubbleColor();

        if (nextBubbleImage)
            nextBubbleImage.color = bubblePopGameMgr.GetNextBubbleColor();
    }
}

