using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private List<Button> T;

    private void Awake()
    {
        HideShop();
    }

    public void ShowShop()
    {
        shopMenu.SetActive(true);
    }

    public void HideShop()
    {
        shopMenu.SetActive(false);
    }

    public void SetupUI (SpriteRenderer t, int i)
    {
        T[i].interactable = true;
        T[i].image.sprite = t.sprite;
    }

}
