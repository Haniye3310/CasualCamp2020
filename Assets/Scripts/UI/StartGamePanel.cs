using System.Collections.Generic;
using UnityEngine;

public enum SoldierType {
    EN, FR, IT
}

public class StartGamePanel : MonoBehaviour
{
    Dictionary<SoldierType, SoldierIcon> _iconsDict = new Dictionary<SoldierType, SoldierIcon>();

    public void RegisterIcon(SoldierType type, SoldierIcon icon) {
        _iconsDict.Add(type, icon);
    }

    public void SetSelectedSoldier(SoldierType soldierType) 
    {
        foreach(SoldierIcon icon in _iconsDict.Values) 
        {
            icon.SetSelected(false);
        }
        _iconsDict[soldierType].SetSelected(true);
    }
}