using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class EzaldirTexteTest : MonoBehaviour
{
    // Update is called once per frame
    public NPCConversation Conversation;
    public GameObject Player;
    bool inRange = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !ConversationManager.Instance.IsConversationActive && inRange)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
}
