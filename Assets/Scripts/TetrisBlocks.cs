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
            DetectInput();
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
        if (!gridCtrl.IsTetrisMovementValid(transform))
        {
            pos.y += 1f;
            transform.position = pos;
            isDropping = false;
            gridCtrl.UpdateGrid(transform);
        }
    }

    private void DetectInput()
    {
        // only accept one transform operation at a time
        Vector3 moveDirection = Vector3.zero;
        Vector3 rotateAngle = Vector3.zero;
        // position
        if (Input.GetKeyDown(KeyCode.W))
            moveDirection = new Vector3(0, 0, 1);
        else if (Input.GetKeyDown(KeyCode.S))
            moveDirection = new Vector3(0, 0, -1);
        else if (Input.GetKeyDown(KeyCode.A))
            moveDirection = new Vector3(-1, 0, 0);
        else if (Input.GetKeyDown(KeyCode.D))
            moveDirection = new Vector3(1, 0, 0);
        // rotation
        else if (Input.GetKeyDown(KeyCode.Q))   // x
            rotateAngle = new Vector3(90, 0, 0);
        else if (Input.GetKeyDown(KeyCode.E))   // y
            rotateAngle = new Vector3(0, 90, 0);
        else if (Input.GetKeyDown(KeyCode.R))   // z
            rotateAngle = new Vector3(0, 0, 90);
        // speed dropping down
        else if (Input.GetKeyDown(KeyCode.Space))
            DropToBottom();

        if (moveDirection != Vector3.zero)
        {
            MoveTetris(moveDirection);
        }
        if (rotateAngle != Vector3.zero)
        {
            RotateTetris(rotateAngle);
        }
    }

    private void MoveTetris(Vector3 direction)
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

    private void RotateTetris(Vector3 angle)
    {
        transform.Rotate(angle, Space.World);
        if (!gridCtrl.IsTetrisMovementValid(transform))
        {
            transform.Rotate(-angle, Space.World);
        }
    }

    private void DropToBottom()
    {
        while (isDropping)
        {
            DoDrop();
        }
    }
}
