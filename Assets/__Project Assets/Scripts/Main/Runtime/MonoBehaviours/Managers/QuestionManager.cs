using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityExtensions;

public class QuestionManager : MonoBehaviour
{
    public const string Letters = "ABCDEFG";

    [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
    [SerializeField] private QuestionSO _question;

    // View part
    [SerializeField] private TextMeshProUGUI _titleGUI;
    [SerializeField] private TextMeshProUGUI _letter;
    [SerializeField] private TextMeshProUGUI _contentGUI;

    void Start()
    {
        _titleGUI.text = _question.Title;

        SetAnswerOrQuestion();
    }

    public void SetAnswerOrQuestion(int val = -1)
    {
        if (val == -1)
        {
            _contentGUI.text = _question.Question;
            _letter.text = string.Empty;
        }
        else
        {
            if (_question.Answers.IsValidIndex(val))
                _contentGUI.text = _question.Answers[val];

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
