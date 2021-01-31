using System.Collections;
using UnityEngine;

public class LoadingScreen : PanelBase
{
    public override void OnPanelActive()
    {

    }

    public override void OnPanelStart()
    {
        if (!PhotonSingleton.PhotonIsActive())
            PhotonSingleton.ConnectToPhoton();

        StartCoroutine(LoadTitleScene());
    }

    private IEnumerator LoadTitleScene()
    {
        yield return new WaitUntil(() => PhotonSingleton.PhotonIsActive());

        bool hasCachedNickName = PlayerPrefs.GetString(PhotonSingleton.PlayerNamePrefsKey) != "";

        MainCanvasReference.TriggerStartPanel(hasCachedNickName);
    }

}
