using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    [SerializeField] private bool usingShop = false;
    [SerializeField] private bool inPauseMenu = false;

    public bool GetShopState()
    {
        return usingShop;
    }

    public void UsingShopTrue()
    {
        usingShop = true;
    }
    public void UsingShopFalse()
    {
        usingShop = false;
    }

    public bool GetPauseState()
    {
        return inPauseMenu;
    }

    public void InPauseMenuTrue()
    {
        inPauseMenu = true;
    }
    public void InPauseMenuFalse()
    {
        inPauseMenu = false;
    }
}
