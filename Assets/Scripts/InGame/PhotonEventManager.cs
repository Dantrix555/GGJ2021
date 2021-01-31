using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonEventManager
{
    public delegate void Interaction(PhotonInteractor sender, PhotonInteractable interactable);
    public static event Interaction OnInteraction;

    public static void InteractionEvent(PhotonInteractor sender, PhotonInteractable interactable)
    {
        OnInteraction?.Invoke(sender, interactable);
    }
}
