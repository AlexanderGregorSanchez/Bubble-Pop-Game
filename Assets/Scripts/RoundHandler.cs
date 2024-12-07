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

    private Vector3 playPosition;
    private int roundCount = 1;

    private void Awake()
    {
        playPosition = roundsList[0].GetComponent<RectTransform>().position;
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

        while (roundsList[nextRoundIndex].transform.position.y >= playPosition.y)
        {
            //print($"{roundsList[nextRoundIndex].name} Current Y: {roundsList[nextRoundIndex].transform.position.y} || Target: {playPosition.y}");
            yield return new WaitForFixedUpdate();

            transform.position += Vector3.down * nextRoundTransitionSpeed * Time.fixedDeltaTime;
        }

        OnTransitionComplete?.Invoke();
        roundCount++;
    }
}
