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

    [Header("Grid size")]
    public int GridSizeX;
    public int GridSizeY;
    public int GridSizeZ;

    [Header("Tetris")]
    public float DropInterval = 1f;
    public Material DeadBlockMaterial;
    public Transform TetrisContainer;
    public TetrisSpawner Spawner;

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
    public bool IsTetrisMovementValid(Transform tetris)
    {
        foreach(Transform child in tetris)
        {
            Vector3 currentPos = child.position;
            if (!IsBlockPosAvailable(currentPos))
            {
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// add the tetris to the grid data, when the tetris will not move anymore
    /// </summary>
    /// <param name="tetris"></param>
    public void UpdateGrid(Transform tetris)
    {
        List<Transform> blocksToUpdate = new List<Transform>();
        bool isValid = true;
        foreach(Transform child in tetris)
        {
            Vector3Int ind = new Vector3Int(Mathf.FloorToInt(child.position.x),
                Mathf.FloorToInt(child.position.y),
                Mathf.FloorToInt(child.position.z));
            if (ind.x < 0 || ind.x >= GridSizeX || ind.y < 0 || ind.y >= GridSizeY || ind.z < 0 || ind.z >= GridSizeZ)
            {
                // TODO: game end or error?
                isValid = false;
            }
            
            if (isValid)
            {
                gridData[ind.x, ind.y, ind.z] = child;
                child.GetComponent<MeshRenderer>().material = DeadBlockMaterial;
                blocksToUpdate.Add(child);
            }
        }
        if (isValid)
        {
            foreach (Transform block in blocksToUpdate)
            {
                block.parent = TetrisContainer;
            }

            Destroy(tetris.gameObject);
            GenerateNewTetris();
        }
        // TODO: check and deal with game end?
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

    private void Start()
    {
        GenerateNewTetris();
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

    private bool IsBlockPosAvailable(Vector3 pos)
    {
        float minX = transform.position.x;
        float minY = transform.position.y;
        float minZ = transform.position.z;
        float maxX = minX + GridSizeX;
        float maxZ = minZ + GridSizeZ;

        if (pos.x <= minX || pos.x >= maxX ||
            pos.y <= minY ||    // y direction drop, ignore maxY
            pos.z <= minZ || pos.z >= maxZ)
        {
            return false;
        }

        if (HasBlockAtIndex(new Vector3Int(Mathf.FloorToInt(pos.x - transform.position.x),
            Mathf.FloorToInt(pos.y - transform.position.y),
            Mathf.FloorToInt(pos.z - transform.position.z))))
        {
            return false;
        }

        return true;
    }

    private bool HasBlockAtIndex(Vector3Int ind)
    {
        if (ind.x < 0 || ind.x >= GridSizeX ||
            ind.y < 0 || ind.y >= GridSizeY ||
            ind.z < 0 || ind.z >= GridSizeZ)
            return false;

        if (gridData[ind.x, ind.y, ind.z] == null)
            return false;
        else
            return true;
    }

    private void GenerateNewTetris()
    {
        if (Spawner)
        {
            Spawner.SpawnRandomTetris();
        }
    }
    #endregion
}
