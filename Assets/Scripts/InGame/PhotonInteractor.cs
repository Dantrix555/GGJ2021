using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonInteractor : MonoBehaviour
{
    public LayerMask detectionLayer;
    public float detectionRadius = 1;
    private Collider[] inRangeInteractables;
    public PhotonInteractable nearestInteractable;

    private void Update()
    {
        inRangeInteractables = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        nearestInteractable = GetNearestInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearestInteractable != null)
            {
                PhotonEventManager.InteractionEvent(this, nearestInteractable);
            }
        }
    }

    private PhotonInteractable GetNearestInteractable()
    {
        PhotonInteractable nearestInteractable = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider collider in inRangeInteractables)
        {
            float colliderDistance = Vector3.Distance(transform.position, collider.transform.position);
            if (colliderDistance < nearestDistance)
            {
                nearestInteractable = collider.GetComponent<PhotonInteractable>();
                nearestDistance = colliderDistance;
            }
        }
        return nearestInteractable;
    }
}
