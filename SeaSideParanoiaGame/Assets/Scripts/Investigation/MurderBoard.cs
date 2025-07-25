using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GHEvtSystem;

public class MurderBoard : Singleton<MurderBoard>
{
    // Murder Board will be parent to clues

    public int questionIndex = 0;
    public List<QuestionPage> questionPages = new List<QuestionPage>();
    public List<Clue> foundClues = new List<Clue>();

    // TODO: how to add clues to left side of board?

    /*
    TODO: need a way to hold the question pages
    - Handle displaying active question
    - Handle switching between questions
    */


    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddListener<FoundClue>(ShowClue);
        EventDispatcher.Instance.AddListener<NextQuestion>(HandleNextQuestion);

        // To make sure only one page is active at a time
        int i = 0;
        foreach (QuestionPage page in questionPages)
        {
            if (i == questionIndex)
            {
                continue;
            }
            page.gameObject.SetActive(false);
            i++;
        }
    }

    void ShowClue(FoundClue evt)
    {
        // FIXME
    }

    void HandleNextQuestion(NextQuestion evt)
    {
        int nextQuestionIndex = questionIndex + 1;
        if (nextQuestionIndex >= questionPages.Count)
        {
            nextQuestionIndex = 0;
        }

        questionPages[questionIndex].gameObject.SetActive(false);
        questionPages[nextQuestionIndex].gameObject.SetActive(true);

        questionIndex = nextQuestionIndex;
    }

    public int GetActiveQuestion()
    {
        return questionPages[questionIndex].question;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<FoundClue>(ShowClue);
        EventDispatcher.Instance.RemoveListener<NextQuestion>(HandleNextQuestion);
    }
}
