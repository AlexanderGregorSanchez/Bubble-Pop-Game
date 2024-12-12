using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

// This script handles the rotation of the gears
public class GearRotationHandler : MonoBehaviour
{
    public UnityEvent OnRotationStart;
    public UnityEvent OnRotationEnd;

    int speed = 0;

    public void StartRotation(int rotationVelocity)
    {
        OnRotationStart?.Invoke();
        speed = rotationVelocity;
        StartCoroutine(RotateGear());
    }

    public void EndRotation()
    {
        speed = 0;
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        OnRotationEnd?.Invoke();
    }

    IEnumerator RotateGear()
    {
        while (speed != 0)
        {
            yield return new WaitForFixedUpdate();
            transform.Rotate(0, 0, speed * Time.fixedDeltaTime, Space.World);
        }
    }
}
