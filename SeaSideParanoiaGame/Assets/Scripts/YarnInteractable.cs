using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Yarn.Unity;
using GHEvtSystem;

public class YarnInteractable : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    public string conversationStartNode;
    private bool interactable = true;
    private bool isCurrentCoversation;

    public Clue clue;

    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && clue != null)
        {
            if (spriteRenderer.sprite == null)
            {
                spriteRenderer.sprite = clue.worldSprite;
            }
            
        }
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


    [YarnCommand("disable")]
    public void DisableConveration()
    {
        interactable = false;
    }

    public void OnMouseDown()
    {
        
        if (ModeManager.Instance.GetCurrentMode() != Mode.Game)
        {
            Debug.Log("mode is not Game, FoundClue and StartConversation will not be triggered");
            return;
        }
        
        Debug.Log("mouse down");

        // if this object also has an associated clue, add to journal
        if (clue != null)
        {
            EventDispatcher.Instance.RaiseEvent<FoundClue>(new FoundClue
            {
                clueID = clue.id
            });
            
        }

        if (interactable && !dialogueRunner.IsDialogueRunning && (!string.IsNullOrEmpty(conversationStartNode)))
        {
            Debug.Log("Pleak");
            EventDispatcher.Instance.RaiseEvent<ChangeMode>(new ChangeMode
            {
                newMode = Mode.Dialogue
            });
            StartConversation();
        }
    }
}
