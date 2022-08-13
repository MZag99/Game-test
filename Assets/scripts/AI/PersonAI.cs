using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAI : MonoBehaviour
{
    public Person personInstance;



    public void Init(Person parent)
    {
        personInstance = parent;

        // StartDecisionLoop();
    }



    private void StartDecisionLoop()
    {
        InvokeRepeating("OnGetDecision", Random.Range(0, 2), Random.Range(5, 10));
    }



    public void OnGetDecision()
    {

    }

    void Update()
    {
        
    }
}
