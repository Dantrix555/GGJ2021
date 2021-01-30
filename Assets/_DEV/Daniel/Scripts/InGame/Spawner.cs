using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Player Prefab")]
    [SerializeField] private GameObject _playerPrefab = default;

    void Awake()
    {
        string prefabNameString = _playerPrefab.name;

        //Instantiate and rotates the object
        GameObject photonObejct = PhotonNetwork.Instantiate(prefabNameString, new Vector3(Random.Range(50f, 100), Random.Range(50f, 100), Random.Range(50f, 100)), _playerPrefab.transform.rotation);

        //Set position and player's name 
        photonObejct.GetComponent<PhotonMovement>().SetStartPosition();
    }
}
