using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TetrisBlocks : MonoBehaviour
{
    public UnityEvent TetrisMoved;
    public UnityEvent TetrisDestroyed;

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

        if (TetrisMoved == null)
            TetrisMoved = new UnityEvent();
        if (TetrisDestroyed == null)
            TetrisDestroyed = new UnityEvent();
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
        TetrisMoved.Invoke();
    }

    public void RotateTetris(Vector3 angle)
    {
        transform.Rotate(angle, Space.World);
        if (!gridCtrl.IsTetrisMovementValid(transform))
        {
            transform.Rotate(-angle, Space.World); 
        }
        TetrisMoved.Invoke();
    }

    public void DropToBottom()
    {
        while (isDropping)
        {
            DoDrop();
        }
    }

    private void OnDestroy()
    {
        TetrisDestroyed.Invoke();
    }
}
