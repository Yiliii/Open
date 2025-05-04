using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    void Start()
    {
        string spawnName = GameStateManager.Instance.currentSpawnPoint;
        if (!string.IsNullOrEmpty(spawnName))
        {
            GameObject spawnPoint = GameObject.Find(spawnName);
            if (spawnPoint)
                transform.position = spawnPoint.transform.position;
        }
    }
}

