using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SoldierType {
    EN, FR, IT, None
}

public class StartGamePanel : MonoBehaviour
{
    Dictionary<SoldierType, SoldierIcon> _iconsDict = new Dictionary<SoldierType, SoldierIcon>();
    public Button StartButton;
    public void RegisterIcon(SoldierType type, SoldierIcon icon) {
        _iconsDict.Add(type, icon);
    }

    public void SetSelectedSoldier(SoldierType soldierType) 
    {
        MyApp.Instance.SelectedSoldier = soldierType;
        foreach(SoldierIcon icon in _iconsDict.Values) 
        {
            icon.SetSelected(false);
        }
        _iconsDict[soldierType].SetSelected(true);
        ActableStartButton();
    }
    void ActableStartButton()
    {
        if (MyApp.Instance.SelectedSoldier != SoldierType.None) StartButton.interactable = true;
    }
}