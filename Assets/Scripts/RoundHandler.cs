using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundHandler : MonoBehaviour
{
    public float nextRoundTransitionSpeed = 50f;

    [SerializeField] private List<GameObject> roundsList = new List<GameObject>();

    public UnityEvent OnTransitionInitialized;
    public UnityEvent OnTransitionComplete;
    public UnityEvent OnAllRoundsComplete;

    public RectTransform playPosition;
    private float targetPlayPositionY;
    private int roundCount = 1;

    private void Awake()
    {
        targetPlayPositionY = playPosition.position.y;
    }

    public void GoToNextRound()
    {
        if (roundCount <= roundsList.Count)
        {
            roundsList[roundCount].transform.GetChild(1).gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(MoveToNextRound(roundCount));
        }
        else
        {
            OnAllRoundsComplete?.Invoke();
        }
    }

    IEnumerator MoveToNextRound(int nextRoundIndex)
    {
        OnTransitionInitialized?.Invoke();
        
        while (roundsList[nextRoundIndex].transform.position.y > targetPlayPositionY)
        {
            yield return new WaitForFixedUpdate();

            transform.position += Vector3.down * nextRoundTransitionSpeed * Time.fixedDeltaTime;
        }

        transform.position += new Vector3(0, targetPlayPositionY - roundsList[nextRoundIndex].transform.position.y, 0);

        OnTransitionComplete?.Invoke();
        roundCount++;
    }
}
