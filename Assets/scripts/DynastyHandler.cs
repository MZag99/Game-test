using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynastyHandler : MonoBehaviour
{
    public List<Dynasty> dynasties = new List<Dynasty> { };
    public int members = 3; // Initial number of members
    private int dynastyCount;

    private List<string> names = new List<string> { "Tusk", "Kowalski", "Wellman" };

    private GameObject[] houseHolds;

    public DynastyHandler(int count)
    {
        dynastyCount = count;
        houseHolds = GameObject.FindGameObjectsWithTag("Household");
    }

    public void populate()
    {
        var dynastyDataList = prepareDynastyData();
        var helperObj = GameObject.FindGameObjectWithTag("HelperObject");

        foreach (var dynasty in dynastyDataList)
        {
            Dynasty instance = helperObj.AddComponent<Dynasty>();

            dynasties.Add(instance);

            instance.Init(dynasty.name, dynasty.houseHold);
        }

    }



    private List<DynastyData> prepareDynastyData()
    {

        var result = new List<DynastyData>();

        for (int i = 0; i < dynastyCount; i++)
        {
            result.Add(new DynastyData { name = names[i], count = members, houseHold = houseHolds[i] });
        }

        return result;
    }



    private class DynastyData
    {
        public string name;
        public int count;
        public GameObject houseHold;
    }
}
