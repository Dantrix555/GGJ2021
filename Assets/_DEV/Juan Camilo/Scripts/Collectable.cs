using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    public enum Description 
    {
        Claro,
        Oscuro,
        Duro,
        Suave
    }

    public string collectableName;
    public List<Description> descriptions;

    protected override void Initialize()
    {
    }
    protected override void OnInteraction(Interactor sender, Interactable interactable)
    {
        Player player = (Player) sender;
        if (player.currentCollectable != null)
        {
            player.DropCollectable(player.currentCollectable);
        }

        player.currentCollectable = this;
        gameObject.SetActive(false);
    }
}
