using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    public bool beginOnStart = true;
    public float timerSeconds;
    public UnityEvent OnTimerComplete;


    private void Start()
    {
        if (beginOnStart) StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timerSeconds);

        OnTimerComplete?.Invoke();
    }
}
