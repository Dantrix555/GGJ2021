using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Text _timeText = default;

    private void Awake()
    {
        StartCoroutine(SetTimer());
    }

    private IEnumerator SetTimer()
    {
        yield return new WaitUntil(() => InGameSingleton.TimeInSeconds != 0);
        for(int i = InGameSingleton.TimeInSeconds; i >= 0; i--)
        {
            _timeText.text = "Time: " + i.ToString();
            yield return new WaitForSeconds(1);
        }

        InGameSingleton.SetGameFinished();
    }
}
