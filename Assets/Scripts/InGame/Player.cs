using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Interactor
{
    public string name;
    public int score;
    public Collectable currentCollectable;
    public Transform hand;
    public float damageRange;
    public LayerMask hitLayerMask;

    private void Awake()
    {
        EventManager.OnHitPlayer += EventManager_OnHitPlayer;
    }

    private void EventManager_OnHitPlayer(Player player)
    {
        if (player == this)
        {
            //Stun Player
            DropCollectable();
        }
    }

    public void DropCollectable()
    {
        if (currentCollectable != null)
        {
            //Todo code here
        }
    }

    public void Attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(hand.position, transform.forward, out hit, damageRange, hitLayerMask))
        {
            Player player = hit.collider.gameObject.GetComponent<Player>();
            EventManager.HitPlayerEvent(player);
        }
    }
}
