using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupManager : MonoBehaviour
{
    [SerializeField] private TowerSelectionManager tsm;

    [SerializeField] private int turnCountForPointMap;

    [SerializeField] private List<int> selectedTowers = new List<int>();
    [SerializeField] private int Map = 1;

    private void Awake()
    {
        SceneManager.sceneLoaded += (scene, mode) => OnLoadCallback(scene);
        
        var sM = FindObjectsOfType<SetupManager>().Length;
        turnCountForPointMap = 5;
        if (sM != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
    
    private void OnLoadCallback(Scene scene)
    {
        if (scene.buildIndex == 1)
        {
            tsm = FindObjectOfType<TowerSelectionManager>();
        }
    }

    public void SetMap(int i)
    {
        Map = i;
    }

    public int GetMap()
    {
        return Map;
    }

    public List<int> GetSelectedTowers()
    {
        return selectedTowers;
    }

    public void TakeSelectedTowers()
    {
        selectedTowers = tsm.GetSelectedTowers();
    }

    public void SetPointMapTurns(int i)
    {
        turnCountForPointMap = i;
    }

    public int GetPointMapTurns()
    {
        return turnCountForPointMap;
    }
    
}
