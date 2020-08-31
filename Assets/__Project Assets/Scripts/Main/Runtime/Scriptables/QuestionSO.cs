using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Game/QuestionSO")]
public class QuestionSO : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private string _question;
    [SerializeField] private string[] _answers;

    public string Title { get; set; }
    public string Question { get; set; }
    public string[] Answers { get; set; }
}
