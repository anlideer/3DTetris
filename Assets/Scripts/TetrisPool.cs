using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tetris/Tetris Pool")]
public class TetrisPool : ScriptableObject
{
    public GameObject[] tetrisPrefabs;

    public GameObject[] GetPrefabPool()
    {
        if (tetrisPrefabs.Length != 0)
            return tetrisPrefabs;
        else
            return new GameObject[] { Resources.Load("TetrisOshape") as GameObject };
    }
}
