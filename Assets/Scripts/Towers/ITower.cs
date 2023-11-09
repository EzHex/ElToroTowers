using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    public float GetPrice();

    public float GetSellPrice();

    public void DestroyTower();

    public void ToggleRange();

}
