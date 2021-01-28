using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInstantiate : MonoBehaviour
{
    private int[] listCoordX = new int[] { 10, 5, 0, -5, -10 };
    private int[] listCoordZ = new int[] { 10, 5, 0, -5, -10 };
    private string folder = "Prefabs";
    private int positionY = 3;
    private List<Vector3Int> positionsList = new List<Vector3Int>();

    void Start()
    {
        GameObject[] lista = Resources.LoadAll<GameObject>(folder);
        int numberObject = Random.Range(0, lista.Length - 1);
        GameObject intatiateItem = lista[numberObject];
        Debug.Log("Data: " + lista.Length);
        Debug.Log("Name Object: " + intatiateItem.name);

        foreach (GameObject item in lista) {
            getPosition(item);
        }

    }

    private void getPosition(GameObject item) {
        Vector3Int position = getRandomVector();

        do {
            position = getRandomVector();
        } while (existPosition(position));
        positionsList.Add(position);
        Instantiate(item, position, Quaternion.identity);
    }

    private Vector3Int getRandomVector() {
        int positionX = listCoordX[Random.Range(0, listCoordX.Length - 1)];
        int positionZ = listCoordX[Random.Range(0, listCoordZ.Length - 1)];

        Vector3Int position = new Vector3Int(positionX, positionY, positionZ);
        return position;
    }

    private bool existPosition(Vector3Int position) {
        return positionsList.Contains(position);
    }
}
