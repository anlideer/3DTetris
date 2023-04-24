using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Grid panel instance")]
    public GameObject BottomPanel;
    public GameObject LeftPanel;
    public GameObject RightPanel;
    public GameObject FrontPanel;
    public GameObject BackPanel;
    //public GameObject RotateCenter;

    [Header("Grid size")]
    public int GridSizeX;
    public int GridSizeY;
    public int GridSizeZ;


    void OnDrawGizmos()
    {
        // Rescale the grid and panel accordingly
        if (BottomPanel)
        {
            // obj scale and position
            BottomPanel.transform.localScale = new Vector3((float)GridSizeX / 10, 1, (float)GridSizeZ / 10);
            BottomPanel.transform.localPosition = new Vector3((float)GridSizeX / 2, 0, (float)GridSizeZ / 2);
            // material tiliing rescale
            BottomPanel.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(GridSizeX, GridSizeZ);
        }

        if (LeftPanel && RightPanel)
        {
            // obj scale and position
            LeftPanel.transform.localScale = new Vector3((float)GridSizeY / 10, 1, (float)GridSizeZ / 10);
            LeftPanel.transform.localPosition = new Vector3(0, (float)GridSizeY / 2, (float)GridSizeZ / 2);
            RightPanel.transform.localScale = new Vector3((float)GridSizeY / 10, 1, (float)GridSizeZ / 10);
            RightPanel.transform.localPosition = new Vector3(GridSizeX, (float)GridSizeY / 2, (float)GridSizeZ / 2);
            // material tiliing rescale
            LeftPanel.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(GridSizeY, GridSizeZ);
        }

        if (FrontPanel && BackPanel)
        {
            // obj scale and position
            FrontPanel.transform.localScale = new Vector3((float)GridSizeY / 10, 1, (float)GridSizeX / 10);
            FrontPanel.transform.localPosition = new Vector3((float)GridSizeX / 2, (float)GridSizeY / 2, 0);
            BackPanel.transform.localScale = new Vector3((float)GridSizeY / 10, 1, (float)GridSizeX / 10);
            BackPanel.transform.localPosition = new Vector3((float)GridSizeX / 2, (float)GridSizeY / 2, GridSizeZ);
            // material tiliing rescale
            FrontPanel.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(GridSizeY, GridSizeX);
        }

        //if (RotateCenter)
        //    RotateCenter.transform.localPosition = new Vector3((float)GridSizeX / 2, (float)GridSizeY / 2, (float)GridSizeZ / 2);
    }
}
