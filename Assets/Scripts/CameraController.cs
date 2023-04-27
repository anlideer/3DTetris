using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotateSensity = 0.2f;
    public float MinAngle = -10f;
    public float MaxAngle = 70f;
    public Transform RotateCenter; // if can automatically set the "rotate center" according to the grid world size, it would be better
    public Transform MainCamera;

    private Vector3 lastPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            RotateCam();
        }
    }

    private void RotateCam()
    {
        Vector3 deltaPos = Input.mousePosition - lastPos;
        float angleX = deltaPos.x * RotateSensity;
        float angleY = -deltaPos.y * RotateSensity;

        // axis x
        Vector3 angle = RotateCenter.eulerAngles;
        angle.x += angleY;
        angle.x = ClampAngle(angle.x, -20f, 70f);
        RotateCenter.eulerAngles = angle;

        // axis y
        transform.RotateAround(RotateCenter.position, Vector3.up, angleX);

        lastPos = Input.mousePosition;
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f)
            angle += 360f;
        if (angle > 180f)
            return Mathf.Max(angle, from + 360f);
        return Mathf.Min(angle, to);
    }
}
