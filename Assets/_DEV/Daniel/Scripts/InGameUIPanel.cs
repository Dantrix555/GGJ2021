using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIPanel : MonoBehaviour
{
    [SerializeField] private Text _timeText = default;

    private int _cachedRound = 0;

    private void Awake()
    {
        StartCoroutine(SetTimer());
    }

    private IEnumerator SetTimer()
    {
        yield return new WaitUntil(() => InGameSingleton.TimeInSeconds != 0);

        _cachedRound = InGameSingleton.ActualRound;

        for (int i = InGameSingleton.TimeInSeconds; i >= 0; i--)
        {
            _timeText.text = "Time: " + i.ToString();
            yield return new WaitForSeconds(1);
        }

        InGameSingleton.SetRoundFinished();

        yield return new WaitUntil(() => InGameSingleton.ActualRound != _cachedRound);

        if(InGameSingleton.ActualRound <= 5 * PhotonSingleton.GetPlayersInRoom())
            StartCoroutine(SetTimer());
    }
}
