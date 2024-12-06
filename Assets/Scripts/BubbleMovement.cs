using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleMovement : MonoBehaviour
{
    public float baseSpeed = 40f;
    private float adjustedSpeed;

    private Vector3 moveDirection;

    private RectTransform topLeftCorner;
    private RectTransform bottomRightCorner;

    public bool isMoving;

    public UnityEvent MovementStarted;
    public UnityEvent MovementStopped;

    private void Start()
    {
        CalculateAdjustedSpeed();
    }

    public void SetReferences(RectTransform topLeftRef, RectTransform botRightRef)
    {
        topLeftCorner = topLeftRef;
        bottomRightCorner = botRightRef;
    }

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

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * adjustedSpeed * Time.fixedDeltaTime);
    }

    private void CalculateAdjustedSpeed()
    {
        float canvasDiagonal = Vector2.Distance(topLeftCorner.position, bottomRightCorner.position);

        adjustedSpeed = baseSpeed * canvasDiagonal / 200f;
    }
}