using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCenter : MonoBehaviour
{
    public TetrisBlocks ActiveTetris;

    private static InteractionCenter instance;

    public static InteractionCenter Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Two instances of InteractionCenter exist. This script should be singleton.");
    }

    private void Update()
    {
        DetectInput();
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
        else if (Input.GetKeyDown(KeyCode.Space) && ActiveTetris)
            ActiveTetris.DropToBottom();

        if (moveDirection != Vector3.zero && ActiveTetris)
        {
            ActiveTetris.MoveTetris(moveDirection);
        }
        if (rotateAngle != Vector3.zero && ActiveTetris)
        {
            ActiveTetris.RotateTetris(rotateAngle);
        }
    }
}
