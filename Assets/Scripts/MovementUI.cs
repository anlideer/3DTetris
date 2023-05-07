using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUI : MonoBehaviour
{
    public Transform CameraHolder;

    public Transform[] NoRotateObjects;

    private void Update()
    {
        // not perfect but better than nothing?
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = CameraHolder.rotation.eulerAngles.y;
        rot.x = CameraHolder.rotation.eulerAngles.x;
        transform.rotation = Quaternion.Euler(rot);

        // don't rotate texts
        for (int i = 0; i < NoRotateObjects.Length; i++)
        {
            NoRotateObjects[i].rotation = Quaternion.identity;
        }
    }

    public void MoveTetrisW()
    {
        InteractionCenter.Instance.MoveTetris(new Vector3(0, 0, 1));
    }
    
    public void MoveTetrisS()
    {
        InteractionCenter.Instance.MoveTetris(new Vector3(0, 0, -1));
    }

    public void MoveTetrisA()
    {
        InteractionCenter.Instance.MoveTetris(new Vector3(-1, 0, 0));
    }

    public void MoveTetrisD()
    {
        InteractionCenter.Instance.MoveTetris(new Vector3(1, 0, 0));
    }
}
