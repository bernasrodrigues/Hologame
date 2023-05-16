using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionUI : MonoBehaviour
{
    public GameObject callerImage;

    public TMP_Text textQuestion;
    public List<TMP_Text> questionAnswers;




    public void setQuestion(string questionText, List<string> questionAnswers , GameObject callerImage = null)
    {
        gameObject.SetActive(true);
        
        if (callerImage != null) this.callerImage = callerImage;
        
        
        this.textQuestion.text = questionText;

        int index = 0;
        foreach (string answerString in questionAnswers)
        {
            this.questionAnswers[index].text = answerString;
            index++;
        }


    }


    private void cleanUp()
    {
        gameObject.SetActive(false);

    }

}
