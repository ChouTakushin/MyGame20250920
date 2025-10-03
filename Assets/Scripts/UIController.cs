using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ObjectManager _om;

    public void SwitchUmi(EnumUmi flg)
    {
        switch (flg)
        {
            case EnumUmi.Danger:
                _om.UmiImage.sprite = _om.Umi_Danger;
                break;
            case EnumUmi.Happy:
                _om.UmiImage.sprite = _om.Umi_Happy;
                break;
            case EnumUmi.Finished:
                _om.UmiImage.sprite = _om.Umi_Finished;
                break;
            case EnumUmi.Start:
            default:
                _om.UmiImage.sprite = _om.Umi_Start;
                break;
        }
        
    }

    public void SetUmiActivate(bool sw)
    {
        _om.UmiImage.gameObject.SetActive(sw);
        if (sw)
        {
            _om.UmiImage.sprite = _om.Umi_Start;
        }
    }
}