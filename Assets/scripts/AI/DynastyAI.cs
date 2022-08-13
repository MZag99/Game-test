using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynastyAI : MonoBehaviour
{

    public Dynasty dynastyInstance;
    public List<string> availableClasses = new List<string>();

    public void Init()
    {
        StartDecisionLoop();
    }


    public void StartDecisionLoop()
    {
        InvokeRepeating("OnGetDecision", Random.Range(0, 2), Random.Range(5, 10));
    }



    public void OnGetDecision()
    {
        /* 
         * Action type = DYNASTY.
         * If there are available building slots on the map, and the dynasty
         * building count is under maximum limit -> build a workshop. This
         * refers only to workshops, not households.
         */
        if (dynastyInstance.buildingCount < dynastyInstance.maxBuildings)
        {
            var freeLocations = Utils.getAvailableLocations();

            if (freeLocations.Length > 0)
            {
                dynastyInstance.BuildWorkshop();
            }

        }

        /* 
         * Action type = PERSON.
         * Action name = TITLE_PURCHASE
         * If dynasty has enough money to afford next title -> delegate a
         * non occupied dynasty member to the city hall to get the next title.
         */
        if (Utils.GetNextDynastyTitle(dynastyInstance).price <= dynastyInstance.dynastyMoney)
        {

        }
    }
}
