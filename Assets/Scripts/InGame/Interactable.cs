using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private void Awake()
    {
        EventManager.OnInteraction += EventManager_OnInteraction;
    }

    private void EventManager_OnInteraction(Interactor sender, Interactable interactable)
    {
        if (interactable == this)
        {
            OnInteraction(sender, interactable);
        }
    }
    protected abstract void OnInteraction(Interactor sender, Interactable interactable);
}
