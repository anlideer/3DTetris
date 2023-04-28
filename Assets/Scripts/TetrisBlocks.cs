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
        if (isDropping)
        {
            DetectInput();
            if (lastDropTime + DropInterval < Time.timeSinceLevelLoad)
            {
                DoDrop();
                lastDropTime = Time.timeSinceLevelLoad;
            }
        }
    }

    private void DoDrop()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        if (gridCtrl.IsTetrisMovementValid(transform, pos))
        {
            transform.position = pos;
        }
        else
        {
            isDropping = false;
            gridCtrl.UpdateGrid(transform);
        }
    }

    private void DetectInput()
    {
        // only accept one transform operation at a time
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W))
            moveDirection = new Vector3(0, 0, 1);
        else if (Input.GetKeyDown(KeyCode.S))
            moveDirection = new Vector3(0, 0, -1);
        else if (Input.GetKeyDown(KeyCode.A))
            moveDirection = new Vector3(-1, 0, 0);
        else if (Input.GetKeyDown(KeyCode.D))
            moveDirection = new Vector3(1, 0, 0);
        // TODO: rotation


        if (moveDirection != Vector3.zero)
        {
            MoveTetris(moveDirection);
        }
    }

    private void MoveTetris(Vector3 direction)
    {
        Vector3 pos = transform.position;
        pos += direction;
        if (gridCtrl.IsTetrisMovementValid(transform, pos))
        {
            transform.position = pos;
        }
    }
}
