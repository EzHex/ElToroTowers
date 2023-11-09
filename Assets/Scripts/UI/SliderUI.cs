using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{

    [SerializeField] private TMP_Text count;
    [SerializeField] private Slider slider;

    [SerializeField] private SetupManager setupManager;

    private void Awake()
    {
        setupManager = FindObjectOfType<SetupManager>();
    }

    public void SetNum()
    {
        int num = (int)slider.value;
        count.text = num.ToString();
        setupManager.SetPointMapTurns(num);
    }

}
