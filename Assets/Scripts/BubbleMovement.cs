using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleMovement : MonoBehaviour
{
    public float speed = 40f;
    private Vector3 moveDirection;

    public bool isMoving;

    public UnityEvent MovementStarted;
    public UnityEvent MovementStopped;

    //public UnityEvent FallStarted;
    public void StartMovement(Vector3 launchDirection)
    {
        moveDirection = launchDirection;
        isMoving = true;
        MovementStarted?.Invoke();
    }
    public void StopMovement()
    {
        moveDirection = Vector3.zero;
        isMoving = false;
        MovementStopped?.Invoke();
    }

    public void Bounce()
    {
        if (!isMoving) return;
        moveDirection = new Vector3(moveDirection.x * -1f, moveDirection.y);
    }

    //public void StartFall()
    //{
    //    print("FALLING");
    //    moveDirection = Vector3.down;
    //    isMoving = true;
    //    FallStarted?.Invoke();
    //}

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * speed * Time.fixedDeltaTime);
    }
}
