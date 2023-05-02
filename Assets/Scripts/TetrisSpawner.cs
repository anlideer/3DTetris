using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    public GameObject[] TetrisPrefabs;

    public void SpawnRandomTetris()
    {
        int num = TetrisPrefabs.Length;
        int ind = Random.Range(0, num);
        GameObject prefabToSpawn = TetrisPrefabs[ind];
        GameObject tetris = Instantiate(prefabToSpawn, transform.position, transform.rotation, transform);
        TetrisBlocks tetrisScript = tetris.GetComponent<TetrisBlocks>();
        if (tetrisScript)
            InteractionCenter.Instance.ActiveTetris = tetrisScript;
    }
}
