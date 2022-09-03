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
        List<Person> freeMembers = dynastyInstance.getFreeMembers();

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
            // Check if some dynasty member is already doing this action
            if (!Utils.CheckIfInProgress(dynastyInstance, "TITLE_PURCHASE")) {
                Debug.Log(dynastyInstance.dynastyName + " PURCHASING TITLE!");

                Person delegateMember = freeMembers[Random.Range(0, freeMembers.Count)];

                Actions.GetTitle(delegateMember);
            }
        }

        /*
         * Action type = PERSON.
         * Action name = WORKING
         * If both previous actions have failed, delegate free members to go
         * and work in their workshops.
         */
        if (freeMembers.Count > 0)
        {

            var availableWorkshopTypes = new List<string>();

            dynastyInstance.workshops.ForEach(el =>
            {
                if (!availableWorkshopTypes.Contains(el.type))
                {
                    availableWorkshopTypes.Add(el.type);
                }
            });

            for(int i = 0; i < freeMembers.Count; i++)
            {
                if (availableWorkshopTypes.Contains(freeMembers[i].type))
                {
                    Debug.Log(freeMembers[i].name + " " + freeMembers[i].dynasty + " is going to work!");

                    var building = dynastyInstance.workshops.Find(el => el.type == freeMembers[i].type);
                    Actions.Work(freeMembers[i], building);
                    return;
                }
            }
        }
    }
}
