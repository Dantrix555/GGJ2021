using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSpawnObjects : MonoBehaviourPun
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] loadedObjects;

    private List<int> _randomIntList = new List<int>();

    //Llamar metodo usando Pun para asignar objeto a buscar
    private void Start()
    {
        if (PhotonSingleton.PlayerIsOwner())
        {
            //Here's OK
            for (int j = 0; j < _spawnPoints.Length; j++)
            {
                int randomObjectIndex = Random.Range(0, loadedObjects.Length);
                photonView.RPC("AddItemToList", RpcTarget.All, randomObjectIndex);
            }
            photonView.RPC("SpawnObjects", RpcTarget.All);
        }
    }

    [PunRPC]
    private void AddItemToList(int item)
    {
        _randomIntList.Add(item);
    }

    [PunRPC]
    private void SpawnObjects()
    {
        for(int i = 0; i < _spawnPoints.Length; i++)
        {
            GameObject instance = Instantiate(loadedObjects[_randomIntList[i]], _spawnPoints[i].position, Quaternion.identity, _spawnPoints[i]);
            instance.GetComponent<PhotonCollectable>().PhotonView.ViewID = 100 + i;
        }
    }
}
