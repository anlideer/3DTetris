using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    public TetrisPool Pool;
    public Material GhostMaterial;
    public PreviewSpawner Preview;

    private GameObject[] TetrisPrefabs;
    private GameObject nextTetris;

    private void Awake()
    {
        TetrisPrefabs = Pool.GetPrefabPool();
    }

    public void SpawnRandomTetris()
    {
        if (nextTetris == null)
            nextTetris = GetRandomTetris();
        GameObject tetris = Instantiate(nextTetris, transform.position, transform.rotation, transform);
        TetrisBlocks tetrisScript = tetris.GetComponent<TetrisBlocks>();
        if (tetrisScript)
        {
            InteractionCenter.Instance.ActiveTetris = tetrisScript;
            InitGhostBlock(nextTetris, tetrisScript);
        }

        // random the next tetris and show preview
        nextTetris = GetRandomTetris();
        if (Preview)
        {
            Preview.UpdatePreview(nextTetris);
        }
    }

    private void InitGhostBlock(GameObject prefabToSpawn, TetrisBlocks parentTetris)
    {
        GameObject tetris = Instantiate(prefabToSpawn, transform.position, transform.rotation, transform);
        Destroy(tetris.GetComponent<TetrisBlocks>());
        foreach(Transform obj in tetris.transform)
        {
            obj.GetComponent<MeshRenderer>().material = GhostMaterial;
        }
        GhostBlocks script = tetris.AddComponent<GhostBlocks>();
        script.ParentTetris = parentTetris;
    }

    private GameObject GetRandomTetris()
    {
        int num = TetrisPrefabs.Length;
        int ind = Random.Range(0, num);
        return TetrisPrefabs[ind];
    }
}
