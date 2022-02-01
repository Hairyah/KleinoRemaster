using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exemple de modification d'un paramèetre de dialogue provenant de l'extérieur du système de dialogue
/// </summary>
public class GlobalDialogueTest : MonoBehaviour
{
    [SerializeField] NPCConversation _conversation;
    [SerializeField] bool _powerfullEnough = false;
    // Start is called before the first frame update
    void Start()
    {
        //on démarre la conversation défini dans l'inspecteur
        ConversationManager.Instance.StartConversation(_conversation);
        //on défini le paramètre voulu avec la valeure correspondante (elle provient de l'inspecteur dans ce cas-ci, mais pourrait venir d'autre part dans le projet)
        ConversationManager.Instance.SetBool("IsPowerfullEnough", _powerfullEnough);

    }
}
