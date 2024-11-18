using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofMovementHandler : MonoBehaviour
{
    private Vector3 initRoofPos;

    [Header("Time Delays In Seconds")]
    public float initialDropDelay = 20.0f;
    public float dropInterval = 10.0f;

    [Header("")]
    [Tooltip("Per Interval")]
    public float dropDistance = 25.0f;
    public float dropSpeed = 50.0f;

    private Vector2 targetDropPos;
    private Vector2 targetRoundPos;

    public GameObject[] roundRoof;
    [SerializeField] private int roundCounter = 1;
    private int roofIndex = 0;

    private void Awake()
    {
        initRoofPos = transform.position;
    }

    private void Start()
    {
        targetDropPos = roundRoof[roofIndex].transform.position;
        StartRoofDrop();
    }

    public void GoToNextRound()
    {

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
        while (transform.position.y > targetDropPos.y)
        {
            yield return new WaitForFixedUpdate();

            Vector3 newPos = transform.position - targetPos;

            transform.Translate(Vector3.down * dropSpeed * Time.fixedDeltaTime);
        }

        transform.position = targetPos;
    }
}
