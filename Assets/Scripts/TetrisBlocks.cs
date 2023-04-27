using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlocks : MonoBehaviour
{
    public float DropInterval = 0.5f;

    private float lastDropTime;
    private GridController gridCtrl;
    private bool isDropping;

    private void Start()
    {
        lastDropTime = 0f;
        isDropping = true;
        gridCtrl = GridController.Instance;
    }

    private void Update()
    {
        if (isDropping && lastDropTime + DropInterval < Time.timeSinceLevelLoad)
        {
            DoDrop();
            lastDropTime = Time.timeSinceLevelLoad;
        }
    }

    private void DoDrop()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        if (gridCtrl.IsTetrisDropValid(transform, pos))
        {
            transform.position = pos;
        }
        else
        {
            isDropping = false;
            gridCtrl.UpdateGrid(transform);
        }
    }
}
