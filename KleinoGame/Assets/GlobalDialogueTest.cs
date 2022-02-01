using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exemple de modification d'un param�etre de dialogue provenant de l'ext�rieur du syst�me de dialogue
/// </summary>
public class GlobalDialogueTest : MonoBehaviour
{
    [SerializeField] NPCConversation _conversation;
    [SerializeField] bool _powerfullEnough = false;
    // Start is called before the first frame update
    void Start()
    {
        //on d�marre la conversation d�fini dans l'inspecteur
        ConversationManager.Instance.StartConversation(_conversation);
        //on d�fini le param�tre voulu avec la valeure correspondante (elle provient de l'inspecteur dans ce cas-ci, mais pourrait venir d'autre part dans le projet)
        ConversationManager.Instance.SetBool("IsPowerfullEnough", _powerfullEnough);

    }
}
