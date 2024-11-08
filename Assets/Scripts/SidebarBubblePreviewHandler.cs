using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidebarBubblePreviewHandler : MonoBehaviour
{
    public BubblePopGameMgr bubblePopGameMgr;
    [SerializeField] private Image currentBubbleImage;
    [SerializeField] private Image nextBubbleImage;

    private void FixedUpdate()
    {
        currentBubbleImage.color = bubblePopGameMgr.GetCurrentBubbleColor();
        nextBubbleImage.color= bubblePopGameMgr.GetNextBubbleColor();
    }
}

