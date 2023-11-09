using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject[] T;
    [SerializeField] private List<int> selectedTowers = new List<int>();

    
    private void Awake()
    {
        PlayerPrefs.SetInt("wave", 100);
        CheckUnlocks();
        ResetList();
    }

    private void CheckUnlocks()
    {
        int maxWave = PlayerPrefs.GetInt("wave");
        for (int i = 10, j = 5; i <= 35; i+=5, j++)
        {
            if (maxWave >= i) T[j].SetActive(true);
            else break;
        }
    }

    public void Select (Toggle b)
    {
        int i = int.Parse(b.name.TrimStart('T'));
        if (selectedTowers.Contains(i))
        {
            selectedTowers.Remove(i);
            CheckStart();
            return;
        }
        selectedTowers.Add(i);
        CheckStart();
    }

    private void CheckStart()
    {
        if (selectedTowers.Count == 4)
        {
            StartButton.SetActive(true);
            return;
        }
        StartButton.SetActive(false);
    }

    public List<int> GetSelectedTowers()
    {
        return selectedTowers;
    }
    public void ResetList()
    {
        while (selectedTowers.Count > 0)
        {
            selectedTowers.RemoveAt(0);
        }
    }

    public void TakeTowersFromTsm()
    {
        var setup = FindObjectOfType<SetupManager>();
        setup.TakeSelectedTowers();
    }

}
