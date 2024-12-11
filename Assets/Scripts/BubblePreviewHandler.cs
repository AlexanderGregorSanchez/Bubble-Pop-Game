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
        if (bubblePopGameMgr == null) return;

        UpdateCurrentBubbleImage();
        UpdateNextBubbleImage(); 
    }
    private void UpdateCurrentBubbleImage() 
    { 
        if (currentBubbleImage == null) return;

        Color color = bubblePopGameMgr.GetCurrentBubbleColor();
        currentBubbleImage.color = (color == Color.clear) ? color : new Color(color.r, color.g, color.b, alpha);
        currentBubbleImage.sprite = bubblePopGameMgr.GetCurrentBubbleSprite();
    }
    private void UpdateNextBubbleImage() 
    { 
        if (nextBubbleImage == null) return;

        nextBubbleImage.color = bubblePopGameMgr.GetNextBubbleColor(); 
        nextBubbleImage.sprite = bubblePopGameMgr.GetNextBubbleSprite();
    }

    public void SetPreviewVisibility(bool visible)
    {
        alpha = visible ? 1 : 0;
        if (currentBubbleImage != null)
        {
            Color c = currentBubbleImage.color;
            currentBubbleImage.color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}

