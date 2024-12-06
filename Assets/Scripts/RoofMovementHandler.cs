using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RoofMovementHandler : MonoBehaviour
{
    private Vector3 initRoofPos;

    [Header("Time Delays In Seconds")]
    public float initialDropDelay = 20.0f;
    public float dropInterval = 10.0f;

    [Header("")]
    public RectTransform floor;
    [SerializeField][ReadOnly] 
    private float totalDropDistance = 0f;
    [Tooltip("The % amount of Total Drop Distance to move per drop")]
    public float dropPercent = 0.15f;
    [SerializeField][ReadOnly][Tooltip("Raw drop distance value")] 
    private float dropDistance = 0f;
    [Tooltip("Total animation duration in seconds")] 
    public float dropDuration = 1.0f;

    private Vector2 targetDropPos;

    private void Awake()
    {
        initRoofPos = transform.position;
    }

    private void Start()
    {
        totalDropDistance = Mathf.Abs(initRoofPos.y - floor.position.y);
        dropDistance = totalDropDistance * dropPercent;

        StartRoofDrop();
    }

    public void ResetRoof()
    {
        transform.position = initRoofPos;
        StopAllCoroutines();
    }

    public void StartRoofDrop()
    {
        StartCoroutine(DropRoofOverTime());
    }

    public void StopRoofDrop()
    {
        StopAllCoroutines();
    }

    IEnumerator DropRoofOverTime()
    {
        yield return new WaitForSeconds(initialDropDelay);
        while (true)
        {
            targetDropPos = new Vector3(transform.position.x, transform.position.y - dropDistance, transform.position.z);

            StartCoroutine(MoveRoof(targetDropPos));
            yield return new WaitForSeconds(dropInterval);
        }
    }

    IEnumerator MoveRoof(Vector3 targetPos)
    {
        float elapsedTime = 0f; 
        Vector3 moveStartingPos = transform.position; 
        float dropInstanceDistance = Vector3.Distance(moveStartingPos, targetPos);
        float requiredSpeed = dropInstanceDistance / dropDuration; 

        while (elapsedTime < dropDuration) 
        { 
            yield return new WaitForFixedUpdate();
            transform.position = Vector3.MoveTowards(moveStartingPos, targetPos, requiredSpeed * (elapsedTime / dropDuration)); 
            elapsedTime += Time.fixedDeltaTime; 
        }

        transform.position = targetPos;
    }
}
