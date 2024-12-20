using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorCollision : MonoBehaviour
{
    public UnityEvent OnBubbleTouchFloor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
            OnBubbleTouchFloor?.Invoke();
    }
}
