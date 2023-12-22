using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDogManager : MonoBehaviour
{
    public UpDog up1;
    public UpDog2 up2;
    public UpDog3 up3;
    public bool dogUp;


    private void Update()
    {
        if (!up1.dogUp2)
        {
            if (!up1.dogUp2)
            {
                if (!up3.dogUp)
                {
                    dogUp = false;
                }
            }
        }
    }
}
