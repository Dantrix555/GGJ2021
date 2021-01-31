using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask detectionLayer;
    public float detectionRadius = 1;
    private Collider[] inRangeInteractables;
    public Interactable nearestInteractable;

    private void Update()
    {
        inRangeInteractables = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        nearestInteractable = GetNearestInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearestInteractable != null)
            {
                EventManager.InteractionEvent(this, nearestInteractable);
            }
        }
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearestInteractable = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider collider in inRangeInteractables)
        {
            float colliderDistance = Vector3.Distance(transform.position, collider.transform.position);
            if (colliderDistance < nearestDistance)
            {
                nearestInteractable = collider.GetComponent<Interactable>();
                nearestDistance = colliderDistance;
            }
        }
        return nearestInteractable;
    }
}
