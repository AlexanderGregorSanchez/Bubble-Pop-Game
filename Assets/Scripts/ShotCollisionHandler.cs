using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShotCollisionHandler : MonoBehaviour
{
    public UnityEvent OnWallHit;
    public UnityEvent OnRoofHit;
    public UnityEvent OnBubbleHit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnWallHit?.Invoke();
            return;
        }
        if (GetComponent<BubbleMovement>().isMoving)
        {
            if (collision.gameObject.CompareTag("Roof"))
            {
                OnRoofHit?.Invoke();
                this.enabled = false;
            }
            if (collision.gameObject.CompareTag("Bubble"))
            {
                OnBubbleHit?.Invoke();
                this.enabled = false;
            }
        }
    }
}
