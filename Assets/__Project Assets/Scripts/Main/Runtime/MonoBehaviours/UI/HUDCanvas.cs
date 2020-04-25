using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : CanvasBase, IMultipleStatUpgradeable
{
    protected override void Start()
    {
        base.Start();
    }

    public void UpdateStat(int index, float value)
    {
        switch (index)
        {
            case 0:

                break;
        }
    }
}
