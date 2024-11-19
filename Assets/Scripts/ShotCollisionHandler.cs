using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShotCollisionHandler : MonoBehaviour
{
    public UnityEvent OnWallHit;
    public UnityEvent<GameObject> OnRoofHit;
    public UnityEvent<GameObject> OnBallHit;
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
                OnRoofHit?.Invoke(collision.transform.parent.GetChild(1).gameObject);
            }
            if (collision.gameObject.CompareTag("Bubble"))
            {
                OnBallHit?.Invoke(collision.transform.parent.gameObject);
            }
        }
    }
}
