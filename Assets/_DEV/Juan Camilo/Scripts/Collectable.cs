using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    protected override void EventManager_OnInteraction(Interactor sender, Interactable interactable)
    {
        if (interactable == this)
        {
            Destroy(this.gameObject);
        }
    }

}
