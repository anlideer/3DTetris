using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSpawner : MonoBehaviour
{
    private GameObject tetris;

    public void UpdatePreview(GameObject prefab)
    {
        if (tetris != null)
            Destroy(tetris);
        tetris = Instantiate(prefab, transform.position, transform.rotation, transform);
        Destroy(tetris.GetComponent<TetrisBlocks>());
    }

    private void Update()
    {
        if (tetris != null)
        {
            tetris.transform.Rotate(new Vector3(0, 20 * Time.deltaTime, 0));
        }
    }
}
