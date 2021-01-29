using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PanelBase : MonoBehaviour
{
    private MainCanvasController _mainCanvasReference;
    public MainCanvasController MainCanvasReference => _mainCanvasReference != null ? _mainCanvasReference : NullCanvas();

    public bool IsActive => gameObject.activeInHierarchy;

    private MainCanvasController NullCanvas()
    {
        Debug.LogError("There's no reference to the main panel");
        return null;
    }

    public void SetMainCanvasReference(MainCanvasController mainCanvas)
    {
        _mainCanvasReference = mainCanvas;
        OnPanelActive();
    }

    public abstract void OnPanelActive();

    public abstract void OnPanelStart();

    public void SetPanelActiveStatus(bool newStatus)
    {
        this.gameObject.SetActive(newStatus);
    }

    public void ForcePanelClose()
    {
        gameObject.SetActive(false);
    }
}
