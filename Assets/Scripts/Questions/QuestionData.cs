using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "ScriptableObjects/Question")]
public class QuestionData : ScriptableObject
{
    public string question;
    public List<string> answers;
    public int correctAnswerIndex;
}