using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private void Awake()
    {
        EventManager.OnInteraction += EventManager_OnInteraction;
    }

    protected abstract void EventManager_OnInteraction(Interactor sender, Interactable interactable);
}
