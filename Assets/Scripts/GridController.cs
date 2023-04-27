using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class GridController : MonoBehaviour
{
    [Header("Grid panel instance")]
    public GameObject BottomPanel;
    public GameObject LeftPanel;
    public GameObject RightPanel;
    public GameObject FrontPanel;
    public GameObject BackPanel;

    [Header("Grid size")]
    public int GridSizeX;
    public int GridSizeY;
    public int GridSizeZ;

    [Header("Other")]
    public Material DeadBlockMaterial;
    public Transform TetrisContainer;

    private static GridController instance;
    private Transform[,,] gridData; 

    public static GridController Instance
    {
        get { return instance; }
    }

    #region public
    
    /// <summary>
    /// Check if the tetris movement (next pos) is valid
    /// </summary>
    /// <param name="tetris"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public bool IsTetrisMovementValid(Transform tetris, Vector3 to)
    {
        float minX = transform.position.x;
        float minY = transform.position.y;
        float minZ = transform.position.z;
        float maxX = minX + GridSizeX;
        float maxY = minY + GridSizeY;
        float maxZ = minX + GridSizeZ;

        // check central block
        if (to.x <= minX || to.y >= maxX ||
            to.y <= minY || to.y >= maxY ||
            to.z <= minZ || to.z >= maxZ)
            return false;

        // TODO: check child blocks

        return true;
    }

    /// <summary>
    /// Similar to IsTetrisMovementValid, but only check for drop
    /// </summary>
    /// <param name="tetris"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public bool IsTetrisDropValid(Transform tetris, Vector3 to)
    {
        float minX = transform.position.x;
        float minY = transform.position.y;
        float minZ = transform.position.z;

        if (to.x <= minX || to.y <= minY || to.z <= minZ)
            return false;

        return true;
    }

    /// <summary>
    /// add the tetris to the grid data, when the tetris will not move anymore
    /// </summary>
    /// <param name="tetris"></param>
    public void UpdateGrid(Transform tetris)
    {
        List<Transform> blocksToUpdate = new List<Transform>();
        foreach(Transform child in tetris)
        {
            gridData[Mathf.FloorToInt(child.position.x),
                Mathf.FloorToInt(child.position.y),
                Mathf.FloorToInt(child.position.z)] = child;
            child.GetComponent<MeshRenderer>().material = DeadBlockMaterial;
            blocksToUpdate.Add(child);
        }

        foreach(Transform block in blocksToUpdate)
        {
            block.parent = TetrisContainer;
        }

        Destroy(tetris.gameObject);
    }

    #endregion

    #region private

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.LogWarning("Two instances of GridController exist. This script should be singleton.");

        gridData = new Transform[GridSizeX, GridSizeY, GridSizeZ];
    }

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
    }
    #endregion
}
