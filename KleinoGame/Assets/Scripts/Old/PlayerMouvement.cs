using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class PlayerMouvement : MonoBehaviour
{
    //Variables pour le déplacement
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public float _posX;
    public float _posY;
    public static Vector2 savePos=new Vector2(0,0);

    //Variable pour la téléportation
    public Vector2 past = new Vector2(0, 0);
    public Vector2 futur = new Vector2(0, 0);
    public bool temporalite = false; //Si temporalite faux alors on est dans le passé

    /*//Variable pour les dialogues
    public UIManager uiManager;*/

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            transform.position = savePos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        

        if (Input.GetKeyDown(KeyCode.Space) && !ConversationManager.Instance.IsConversationActive && SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (!temporalite)
            {
                temporalite = true;
                past = transform.position;
                transform.position = futur;
            }
            else
            {
                temporalite = false;
                futur = transform.position;
                transform.position = past;
            }
            Debug.Log("Voyage dans le temps !");
        }

        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!ConversationManager.Instance.IsConversationActive)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
       
    }


    public void SavePosition()
    {
        _posX = transform.position.x ;
        _posY = transform.position.y;
        savePos = new Vector2(_posX, _posX);
    }
}

