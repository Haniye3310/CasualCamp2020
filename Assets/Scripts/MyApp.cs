﻿using UnityEngine;
using GameAnalyticsSDK;
public class MyApp : MonoBehaviour
{
    public static MyApp Instance { get; private set; }
    public SoldierType SelectedSoldier = SoldierType.None;
    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            GameAnalytics.Initialize();
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != Instance) 
            {
                Destroy(this.gameObject);
            }
        }
    }
}