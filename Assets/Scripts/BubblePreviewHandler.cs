using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubblePreviewHandler : MonoBehaviour
{
    public BubblePopGameMgr bubblePopGameMgr;
    [SerializeField] private Image currentBubbleImage;
    [SerializeField] private Image nextBubbleImage;

    int alpha = 1;

    private void FixedUpdate()
    {
        if (!bubblePopGameMgr) return; 

        if (currentBubbleImage)
        {
            Color c = bubblePopGameMgr.GetCurrentBubbleColor();
            currentBubbleImage.color = new Color(c.r, c.g, c.b, alpha);
        }

        if (nextBubbleImage)
        {
            nextBubbleImage.color = bubblePopGameMgr.GetNextBubbleColor();
        }
    }


    public void SetPreviewVisibility(bool visible)
    {
        Color c = currentBubbleImage.color;
        if (visible)
            alpha = 1;
        else
            alpha = 0;
    }
}

