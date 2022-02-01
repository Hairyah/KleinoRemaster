using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Actions : MonoBehaviour
{
    [SerializeField] bool _firstEncEzalTuto = true;
    [SerializeField] bool _finalEzalTuto = false;

    [SerializeField] /*static*/ bool _firstEncForg = true;
    [SerializeField] /*static*/ bool _firstEncMarc = true;
    [SerializeField] /*static*/ bool _firstEncPret = true;
    [SerializeField] /*static*/ bool _possClef = false;
    [SerializeField] /*static*/ int _questClefEtat = 0;

    public NPCConversation _EzaldirTutoDial;
    public NPCConversation _ForgeronDial;
    public NPCConversation _MarchandDial;
    public NPCConversation _PretreDial;
    public PlayerMouvement _PlayerMouv;

    GameObject pnj;
    GameObject porte;
    bool collisionPnj = false;
    bool collisionPorte = false;
    string nomNPC;


    /*<!-- Lors d'une collision -->*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*< !--Si c'est un pnj -->*/
        if (collision.gameObject.tag == "Pnj")
        {
            /*< !--On recupere le pnj avec qui on est en collision-- >*/
            pnj = collision.collider.gameObject;
            collisionPnj = true;
        }

        if (collision.gameObject.tag == "PorteFerme")
        {
            porte = collision.collider.gameObject;
            collisionPorte = true;
        }
    }

    private void Update()
    {
        /*< !--Verifier si on est assez proche d'un pnj et qu'on tente de lui parler-- >*/
        if (collisionPnj && Input.GetKeyDown(KeyCode.E) && !ConversationManager.Instance.IsConversationActive)
        {
            nomNPC = pnj.name;
            startDialogue(pnj.name);
        }

        if (collisionPorte && Input.GetKeyDown(KeyCode.E))
        {
            enterNewScene(porte.name);
        }
    }

    void startDialogue(string a_pnj)
    {
        /*< !--Permet de lancer le dialogue associé au bon pnj-- >*/
        switch (a_pnj)
        {
            case "EzaldirTuto":
                ConversationManager.Instance.StartConversation(_EzaldirTutoDial);
                break;
            case "Forgeron":
                ConversationManager.Instance.StartConversation(_ForgeronDial);
                break;
            case "Marchand":
                ConversationManager.Instance.StartConversation(_MarchandDial);
                break;
            case "Pretre":
                ConversationManager.Instance.StartConversation(_PretreDial);
                break;
        }
    }

    public void enterNewScene(string a_porte)
    {
        if (a_porte != "Exterieur")
        {
            _PlayerMouv.SavePosition();
        }
        
        switch (a_porte)
        {
            case "Exterieur":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Eglise":
                SceneManager.LoadScene("EgliseScene");
                break;
            case "Taverne":
                SceneManager.LoadScene("TaverneScene");
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        /*< !--Reseter la variable qui recupere le pnj une fois qu'on s'en va -->*/
        if (collision.gameObject.tag == "Pnj")
        {
            collisionPnj = false;
        }

        if (collision.gameObject.tag == "PorteFerme")
        {
            collisionPorte = false;
        }
    }

    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += ConversationStart;
        ConversationManager.OnConversationEnded += ConversationEnd;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= ConversationStart;
        ConversationManager.OnConversationEnded -= ConversationEnd;
    }

    private void ConversationStart()
    {
        //on défini le paramètre voulu avec la valeure correspondante (elle provient de l'inspecteur dans ce cas-ci, mais pourrait venir d'autre part dans le projet)
        switch (nomNPC)
        {
            case "EzaldirTuto":
                ConversationManager.Instance.SetBool("FirstTime", _firstEncEzalTuto);
                ConversationManager.Instance.SetBool("Final", _finalEzalTuto);
                break;
            case "Forgeron":
                ConversationManager.Instance.SetBool("FirstEncForg", _firstEncForg);
                ConversationManager.Instance.SetBool("FirstEncMarc", _firstEncMarc);
                ConversationManager.Instance.SetBool("FirstEncPret", _firstEncPret);
                break;
            case "Marchand":
                ConversationManager.Instance.SetBool("FirstEncForg", _firstEncForg);
                ConversationManager.Instance.SetBool("FirstEncMarc", _firstEncMarc);
                ConversationManager.Instance.SetBool("FirstEncPret", _firstEncPret);
                ConversationManager.Instance.SetBool("PossClef", _possClef);
                ConversationManager.Instance.SetInt("QuestClefEtat", _questClefEtat);
                break;
            case "Pretre":
                ConversationManager.Instance.SetBool("FirstEncForg", _firstEncForg);
                ConversationManager.Instance.SetBool("FirstEncPret", _firstEncPret);
                break;
        }
    }

    private void ConversationEnd()
    {
        switch (nomNPC)
        {
            case "EzaldirTuto":
                _firstEncEzalTuto = ConversationManager.Instance.GetBool("FirstTime");
                _finalEzalTuto = ConversationManager.Instance.GetBool("Final");
                break;
            case "Forgeron":
                _firstEncForg = ConversationManager.Instance.GetBool("FirstEncForg");
                _firstEncMarc = ConversationManager.Instance.GetBool("FirstEncMarc");
                _firstEncPret = ConversationManager.Instance.GetBool("FirstEncPret");
                break;
            case "Marchand":
                _firstEncForg = ConversationManager.Instance.GetBool("FirstEncForg");
                _firstEncMarc = ConversationManager.Instance.GetBool("FirstEncMarc");
                _possClef = ConversationManager.Instance.GetBool("PossClef");
                _questClefEtat = ConversationManager.Instance.GetInt("QuestClefEtat");
                break;
            case "Pretre":
                _firstEncForg = ConversationManager.Instance.GetBool("FirstEncForg");
                _firstEncPret = ConversationManager.Instance.GetBool("FirstEncPret");
                break;
        }
    }
}
