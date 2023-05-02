using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlocks : MonoBehaviour
{
    private float lastDropTime;
    private GridController gridCtrl;
    private bool isDropping;
    private float dropInterval;

    private void Start()
    {
        lastDropTime = 0f;
        isDropping = true;
        gridCtrl = GridController.Instance;
        dropInterval = gridCtrl.DropInterval;
    }

    private void Update()
    {
        if (isDropping)
        {
            if (lastDropTime + dropInterval < Time.timeSinceLevelLoad)
            {
                DoDrop();
                lastDropTime = Time.timeSinceLevelLoad;
            }
        }
    }

    private void DoDrop()
    {
        Vector3 pos = transform.position;
        pos.y -= 1f;
        transform.position = pos;
        if (!gridCtrl.IsTetrisMovementValid(transform, true))
        {
            pos.y += 1f;
            transform.position = pos;
            isDropping = false;
            gridCtrl.UpdateGrid(transform);
        }
    }

    public void MoveTetris(Vector3 direction)
    {
        Vector3 pos = transform.position;
        pos += direction;
        transform.position = pos;
        if (!gridCtrl.IsTetrisMovementValid(transform))
        {
            pos -= direction;
            transform.position = pos;
        }
    }

    public void RotateTetris(Vector3 angle)
    {
        transform.Rotate(angle, Space.World);
        if (!gridCtrl.IsTetrisMovementValid(transform))
        {
            transform.Rotate(-angle, Space.World);
        }
    }

    public void DropToBottom()
    {
        while (isDropping)
        {
            DoDrop();
        }
    }
}
