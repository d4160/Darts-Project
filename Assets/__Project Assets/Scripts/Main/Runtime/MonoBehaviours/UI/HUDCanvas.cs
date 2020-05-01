using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : CanvasBase, IMultipleStatUpgradeable
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _lastCalculatedPoints;

    protected override void Start()
    {
        base.Start();

        _scoreText.text = "0";
        _lastCalculatedPoints.text = string.Empty;
    }

    public void UpdateStat(int index, float value)
    {
        switch (index)
        {
            case 0:
                _scoreText.text = ((int)value).ToString();
                break;
            case 1:
                _lastCalculatedPoints.text = ((int)value).ToString();
                break;
        }
    }
}
