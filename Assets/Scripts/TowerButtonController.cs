using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonController : MonoBehaviour
{
    [SerializeField] private GameObject highLight;

    public void UpdateHighLight()
    {
        highLight.SetActive(!highLight.activeSelf);
    }
}
