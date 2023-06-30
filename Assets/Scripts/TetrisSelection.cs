using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TetrisSelection : MonoBehaviour
{
    public GameObject[] PrefabPools;
    public PreviewSpawner[] Previews;
    public Image[] ButtonImages;
    public TetrisPool ChosenPool;
    public Button StartButton;

    private bool[] prefabChosen;

    private void Start()
    {
        GameManager.IsGameEnd = false;
        GameManager.Score = 0;
        Time.timeScale = 1.0f;

        if (PrefabPools.Length != Previews.Length || PrefabPools.Length == 0 || PrefabPools.Length != ButtonImages.Length)
        {
            Debug.LogError("Preview number set wrong");
        }

        for (int i = 0; i < Previews.Length; i++)
        {
            Previews[i].UpdatePreview(PrefabPools[i]);
        }

        prefabChosen = new bool[PrefabPools.Length];
    }

    public void SelectTetris(int i)
    {
        if (prefabChosen[i])
        {
            prefabChosen[i] = false;
            ButtonImages[i].color = Color.white;
            Transform child = ButtonImages[i].transform.Find("Text (TMP)");
            if (child)
            {
                child.GetComponent<TextMeshProUGUI>().text = "Select";
            }
        }
        else
        {
            prefabChosen[i] = true;
            ButtonImages[i].color = new Color(1.0f, 0.647f, 0.0f);
            Transform child = ButtonImages[i].transform.Find("Text (TMP)");
            if (child)
            {
                child.GetComponent<TextMeshProUGUI>().text = "Unselect";
            }
        }

        StartButton.interactable = IfCanStart();
    }

    public void StartGame()
    {
        // should not happen
        if (!IfCanStart())
        {
            Debug.LogWarning("chosen less than 4");
            return;
        }

        // write to file and start game
        List<GameObject> finalChosen = new List<GameObject>();
        for (int i = 0; i < PrefabPools.Length; i++)
        {
            if (prefabChosen[i])
                finalChosen.Add(PrefabPools[i]);
        }
        ChosenPool.tetrisPrefabs = finalChosen.ToArray();

        SceneManager.LoadScene("GameScene");
    }

    private bool IfCanStart()
    {
        int cnt = 0;
        foreach (bool b in prefabChosen)
            if (b) cnt++;
        return cnt >= 4;
    }
}
