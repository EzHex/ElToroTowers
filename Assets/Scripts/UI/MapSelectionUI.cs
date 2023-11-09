using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private Toggle pointMapToggle;
    [SerializeField] private List<Toggle> toggles;

    private SetupManager setupManager;


    private void Awake()
    {
        setupManager = FindObjectOfType<SetupManager>();
    }

    public void ToggleSlider()
    {
        if (pointMapToggle.isOn)
            slider.SetActive(true);
        else slider.SetActive(false);
    }

    public void CheckForMapNumber()
    {
        int i = 1;
        foreach (var t in toggles)
        {
            if (t.isOn)
            {
                setupManager.SetMap(i);
                break;
            }
            i++;
        }
    }

    
}
