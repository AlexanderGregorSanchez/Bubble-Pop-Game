using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubblePreviewHandler : MonoBehaviour
{
    
    public BubblePopGameMgr bubblePopGameMgr;
    [SerializeField] private Image bubblePreview;

    private void FixedUpdate()
    {
        bubblePreview.color = bubblePopGameMgr.GetCurrentBubbleColor();
    }
}
