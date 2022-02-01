using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestFollow : MonoBehaviour
{
    public bool firstRencForg = true;
    UnityEvent m_MyEvent = new UnityEvent();

    private void Start()
    {
        m_MyEvent.AddListener(Forgeron);
    }
    public void Forgeron()
    {
        firstRencForg = false;
    }
}
