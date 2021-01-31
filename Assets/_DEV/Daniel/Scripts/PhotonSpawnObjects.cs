using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSpawnObjects : MonoBehaviourPun
{
    [SerializeField] private Transform[] spawnPoints;
    
    private string _folder = "Prefabs";
    private List<GameObject> _objectsList;

    //Llamar metodo usando Pun para asignar objeto a buscar

    private void Start()
    {
        if (PhotonSingleton.PlayerIsOwner())
            photonView.RPC("SetObjectToCollect", RpcTarget.All);
    }

    [PunRPC]
    private void SetObjectToCollect()
    {
        _objectsList = new List<GameObject>();
        GameObject[] loadedObjects = Resources.LoadAll<GameObject>(_folder);

        for(int i = 0; i < loadedObjects.Length; i++)
        {
            _objectsList.Add(loadedObjects[i]);
        }

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            int randomObjectIndex = Random.Range(0, _objectsList.Count);
            GameObject instance = Instantiate(_objectsList[randomObjectIndex], spawnPoints[i].position, Quaternion.identity, transform);
            //Save here the objects in game singleton gameobject list
            _objectsList.RemoveAt(randomObjectIndex);
        }
    }
}
