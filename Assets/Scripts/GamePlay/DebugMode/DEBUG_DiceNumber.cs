#if DEBUG_MODE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnNumberSet(int num);
public class DEBUG_DiceNumber : MonoBehaviour
{
    public event OnNumberSet OnNumberSet;
    void Update()
    {
        if(Input.anyKeyDown)
        {
            int num = -1;
            if (Input.GetKeyDown(KeyCode.Alpha1)) num = 1;
            if (Input.GetKeyDown(KeyCode.Alpha2)) num = 2;
            if (Input.GetKeyDown(KeyCode.Alpha3)) num = 3;
            if (Input.GetKeyDown(KeyCode.Alpha4)) num = 4;
            if (Input.GetKeyDown(KeyCode.Alpha5)) num = 5;
            if (Input.GetKeyDown(KeyCode.Alpha6)) num = 6;

            if(num != -1)
            {
                OnNumberSet(num);
                num = -1;
            }
        }
    }
}
#endif