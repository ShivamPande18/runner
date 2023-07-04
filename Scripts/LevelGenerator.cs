using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levelPieces;

    List<GameObject> spawnedPieces = new List<GameObject>();
    int spawnPos = 50;

    private void Start()
    {
        spawnedPieces.Clear();
        SpawnLevelPiece();
        SpawnLevelPiece();
        SpawnLevelPiece();
        SpawnLevelPiece();
        SpawnLevelPiece();
    }

    public void SpawnLevelPiece()
    {
        int choice = Random.Range(0, levelPieces.Length);
        GameObject curLevel =  Instantiate(levelPieces[choice],new Vector3(0,0,spawnPos),Quaternion.identity) as GameObject;
        spawnedPieces.Add(curLevel);

        if (spawnedPieces.Count > 6)
        {
            GameObject delLevel = spawnedPieces[0];
            spawnedPieces.RemoveAt(0);
            Destroy(delLevel);
        }


        spawnPos += 200;
    }
}