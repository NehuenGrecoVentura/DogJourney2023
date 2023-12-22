using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsMenu : MonoBehaviour
{
    [SerializeField] Image _backgroundSelect;

    public void MoveBackgroundButton()
    {
        _backgroundSelect.transform.position = transform.position;
    }
}
