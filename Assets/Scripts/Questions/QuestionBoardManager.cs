using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionBoardManager : MonoBehaviour
{
    public QuestionUI questionUI;

    [SerializeField] private QuestionData[] questions;
    [SerializeField] private float questionTimeLimit = 10f;
    [SerializeField] private int maxFailedAnswers = 3;


    [SerializeField] private float minTimeBeforeFirstQuestion = 1f;
    [SerializeField] public float maxTimeBeforeFirstQuestion = 10f;

    [SerializeField] private float timeToAnswerQuestion = 60f;
    [SerializeField] private float timeLeftToAnswer = 0;

    [SerializeField] private int rightAnswers = 0;
    [SerializeField] private int failedAnswers = 0;

    [SerializeField] private bool isQuestionActive = false;
    [SerializeField] private QuestionData activeQuestion;

    [SerializeField] private bool loggerOn = false;
    private Logger logger;

    private void Start()
    {
        logger = new Logger(loggerOn);


        float timeUntilQuestion = Random.Range(1f, 5f);
        logger.Log("Time until Question: " + timeUntilQuestion);
        Invoke("ChooseRandomQuestion", Random.Range(1f, 10f)); // Invoke the DisplayRandomQuestion function after a random time
    }

    private void Update()
    {
        if (isQuestionActive)
        {
            timeLeftToAnswer -= Time.deltaTime;
            if (timeLeftToAnswer <= 0)
            {
                AnswerGuessFail();  // time's up
            }
        }
    }



    private void ChooseRandomQuestion()
    {
        int questionIndex = Random.Range(0, questions.Length);
        activeQuestion = questions[questionIndex];
        isQuestionActive = true;

        DisplayQuestion();
        SetTimer();

    }



    private void SetTimer()
    {
        timeLeftToAnswer = timeToAnswerQuestion;
    }



    private void DisplayQuestion()
    {
        logger.Log("Displaying Question");
        questionUI.setQuestion(questionText: activeQuestion.question , questionAnswers: activeQuestion.answers , callerImage: null);
    }


    public void CheckAnswer(int index)
    {
        if (index == activeQuestion.correctAnswerIndex)
        {
            AnswerGuessCorrect();
        }
        else
        {
            AnswerGuessFail();
        }

    }



    private void AnswerGuessFail()
    {
        logger.Log("Incorrect answer.");
        failedAnswers += 1;

        if (failedAnswers >= maxFailedAnswers)
        {
            TriggerFailureFunction();
        }
        else
        {
            ChooseRandomQuestion();
        }

        isQuestionActive = false;
    }


    private void AnswerGuessCorrect()
    {
        logger.Log("Correct answer.");
        rightAnswers += 1;

        if (failedAnswers >= maxFailedAnswers)
        {
            TriggerRightFunction();
        }
        else
        {
            ChooseRandomQuestion();
        }

        isQuestionActive = false;

    }





    private void TriggerFailureFunction()
    {
        logger.Log("Failed " + failedAnswers + " questions. Triggering failure function.");
        // call your failure function here
    }

    private void TriggerRightFunction()
    {
        logger.Log("Right " + rightAnswers + " questions. Triggering Correct function.");
    }
}