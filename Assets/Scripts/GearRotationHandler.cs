using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GearRotationHandler : MonoBehaviour
{
    public UnityEvent OnRotationStart;
    public UnityEvent OnRotationEnd;

    int speed = 0;

    public void StartRotation(int rotationVelocity)
    {
        OnRotationStart?.Invoke();
        speed = rotationVelocity;
    }

    public void EndRotation()
    {
        speed = 0;
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        OnRotationEnd?.Invoke();
    }

    private void FixedUpdate()
    {

        //transform.rotation += Vector3.forward * speed * Time.fixedDeltaTime;
        transform.Rotate(0,0, speed * Time.fixedDeltaTime, Space.World);
    }
    IEnumerator RotateGear()
    {
        while (speed != 0)
        {
            yield return new WaitForFixedUpdate();
            //transform.Rotate(Vector3.forward, speed * Time.fixedDeltaTime,);
            transform.Rotate(0, 0, speed);
        }
    }
}
