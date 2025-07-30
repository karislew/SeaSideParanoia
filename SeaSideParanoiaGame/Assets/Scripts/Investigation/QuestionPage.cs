using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using UnityEngine.UI;

public class QuestionPage : MonoBehaviour
{
    public int question = 0;
    public bool isSolved = false;
    public List<int> playerAttempt = new List<int>();
    
    //public List<Transform> slots = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Grab all children with slot script on them
        EventDispatcher.Instance.AddListener<SlotUpdate>(HandleSlotUpdate);
    }


    void HandleSlotUpdate(SlotUpdate evt)
    {
        if (isSolved || (evt.clueID == 0) ||
        //MurderBoard.Instance.GetActiveQuestion() != question)
        evt.question != question)
        {
            return;
        }

        if (evt.unGuessing && playerAttempt.Contains(evt.clueID))
        {
            playerAttempt.Remove(evt.clueID);
        } else if (!evt.unGuessing && !playerAttempt.Contains(evt.clueID)) 
        {
            playerAttempt.Add(evt.clueID);
        }

        CheckForCorrectNess();
    }

    void CheckForCorrectNess()
    {
        List<int> trueAnswers = ClueManager.Instance.GetTrueAnswer(question);

        if (playerAttempt.Count != trueAnswers.Count)
        {
            return;
        }

        foreach (int guess in playerAttempt)
        {
            if (!trueAnswers.Contains(guess))
            {
                return;
            }
        }

        // TODO: make MurderBoard handle showing visual confirmation
        // TODO: send signal to investigation system that this question is solved
        Debug.Log("Question " + question + " has been solved!");
        isSolved = true;
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        playerAttempt.Clear();
        Image thisImage = GetComponent<Image>();
        thisImage.color = Color.green;
        //thisImage.enabled = true;
        //thisImage.color = Color.green;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<SlotUpdate>(HandleSlotUpdate);
    }
}
