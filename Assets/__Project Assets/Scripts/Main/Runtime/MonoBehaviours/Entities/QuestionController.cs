using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    public const string Letters = "ABCDEFG";

    [SerializeField] private string _title;
    [SerializeField] private string _question;
    [SerializeField] private string[] _answers;

    // View part
    [SerializeField] private TextMeshProUGUI _titleGUI;
    [SerializeField] private TextMeshProUGUI _letter;
    [SerializeField] private TextMeshProUGUI _contentGUI;

    void Start()
    {
        _titleGUI.text = _title;

        SetAnswerOrQuestion();
    }

    public void SetAnswerOrQuestion(int val = -1)
    {
        if (val == -1)
        {
            _contentGUI.text = _question;
            _letter.text = string.Empty;
        }
        else
        {
            if (_answers.IsValidIndex(val))
                _contentGUI.text = _answers[val];

            _letter.text = Letters[val].ToString();

            StartCoroutine(WaitAndRestore(3.5f));
        }
    }

    private IEnumerator WaitAndRestore(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SetAnswerOrQuestion();
    }
}
