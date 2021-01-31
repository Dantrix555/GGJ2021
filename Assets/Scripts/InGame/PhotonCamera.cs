using UnityEngine;

public class PhotonCamera : MonoBehaviour
{
    private Transform target;
    private bool isFollowingPlayer = false;

    public Transform camera;
    public float smoothLevel;
    Vector3 finalPosition;
    Vector3 velocity = Vector3.zero;

    public void SetPlayerTransform(Transform newPlayerTransform)
    {
        target = newPlayerTransform;
        IsFollowingPlayer(true);
    }

    public void IsFollowingPlayer(bool follow)
    {
        isFollowingPlayer = follow;
    }

    private void Awake()
    {
        camera = transform.GetChild(0);
    }

    private void Update()
    {
        if(isFollowingPlayer)
            finalPosition = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothLevel);
    }

    private void LateUpdate()
    {
        if(isFollowingPlayer)
        {
            transform.position = finalPosition;
            camera.LookAt(target);
        }
    }
}
