using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlocks : MonoBehaviour
{
    public TetrisBlocks ParentTetris;

    private void Start()
    {
        if (ParentTetris != null && ParentTetris.TetrisMoved != null)
        {
            ParentTetris.TetrisMoved.AddListener(UpdateGhostBlocks);
            ParentTetris.TetrisDestroyed.AddListener(() => { Destroy(gameObject); });
        }
        UpdateGhostBlocks();
    }

    private void UpdateGhostBlocks()
    {
        transform.position = ParentTetris.transform.position;
        transform.rotation = ParentTetris.transform.rotation;
        // try to drop down to the end
        while (true)
        {
            transform.position += Vector3.down;
            if (!GridController.Instance.IsTetrisMovementValid(transform, true))
            {
                transform.position += Vector3.up;
                break;
            }
        }
    }
}
