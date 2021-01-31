using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhotonInteractable : MonoBehaviour
{
    private void Awake()
    {
        PhotonEventManager.OnInteraction += EventManager_OnInteraction;
    }

    protected abstract void EventManager_OnInteraction(PhotonInteractor sender, PhotonInteractable interactable);
}
