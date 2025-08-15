using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using UnityEngine.UI;
using Yarn.Unity;
public class QuestionPage : MonoBehaviour
{
    public int question = 0;
    public bool isSolved = false;
    public List<int> playerAttempt = new List<int>();
    public GameObject murderBoard;
    private DialogueRunner dialogueRunner;
    public string conversationStartNode;
    private bool isCurrentCoversation;



    //public List<Transform> slots = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Grab all children with slot script on them
        EventDispatcher.Instance.AddListener<SlotUpdate>(HandleSlotUpdate);
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }
    private void StartConversation()
    {
        isCurrentCoversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
    }


    private void EndConversation()
    {
        if (isCurrentCoversation)
        {
            EventDispatcher.Instance.RaiseEvent<ChangeMode>(new ChangeMode
            {
                newMode = Mode.Game
            });
            isCurrentCoversation = false;

        }

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
        }
        else if (!evt.unGuessing && !playerAttempt.Contains(evt.clueID))
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

        isSolved = true;

        foreach (Transform child in gameObject.transform)
        {
            Debug.Log(child.gameObject.name);
            DragItem dragScript = child.gameObject.GetComponentInChildren<DragItem>();
            if (dragScript == null)
            {
                Debug.Log("Is this working");
                //Destroy(child.gameObject.GetComponent<DragItem>());
            }

        }
        //playerAttempt.Clear();
        Image thisImage = GetComponent<Image>();
        thisImage.color = Color.green;
        StartCoroutine(HideMurderBoard());

        //thisImage.enabled = true;
        //thisImage.color = Color.green;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<SlotUpdate>(HandleSlotUpdate);
    }
    IEnumerator HideMurderBoard()
    {
        yield return new WaitForSeconds(1);
        murderBoard.SetActive(false);
        RunDialogue();

    }
    void RunDialogue()
    {
        if(!dialogueRunner.IsDialogueRunning)
        {
            EventDispatcher.Instance.RaiseEvent<ChangeMode>(new ChangeMode
            {
                newMode = Mode.Dialogue
            });
            StartConversation();
        }
    }
}
