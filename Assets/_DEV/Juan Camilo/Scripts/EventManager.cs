using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void Interaction(Interactor sender, Interactable interactable);
    public static event Interaction OnInteraction;

    public static void InteractionEvent(Interactor sender, Interactable interactable)
    {
        OnInteraction?.Invoke(sender, interactable);
    }
}
