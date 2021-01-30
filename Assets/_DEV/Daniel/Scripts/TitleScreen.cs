using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : PanelBase
{

    [Header("Login Elements")]
    [SerializeField] private InputField _nicknameImputField = default;
    [SerializeField] private Button _setNicknameButton = default;

    public override void OnPanelActive()
    {
        _nicknameImputField.text = "";
        _setNicknameButton.interactable = false;

        _nicknameImputField.onValueChanged.AddListener(SetNicknameButtonActive);
        _setNicknameButton.onClick.AddListener(SetPlayerNickname);
    }

    public override void OnPanelStart()
    {
        //
    }

    private void SetNicknameButtonActive(string nickname)
    {
        _setNicknameButton.interactable = nickname.Length >= 3;
    }

    private void SetPlayerNickname()
    {
        string playerNickname = _nicknameImputField.text;

        if (playerNickname.Length < 3) { Debug.LogError("Were trying to set names with length less than 3 characters"); return; }

        PhotonSingleton.SetPlayerNickname(playerNickname);

        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuScreen);
    }
}
