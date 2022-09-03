using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynasty : MonoBehaviour
{
    public int dynastyMoney = 5000;
    public int buildingCount = 0; // Does not refer to households, only workshops
    public int houseHoldsCount = 0;
    public int memberCount; // Initial number of members to create - passed from the DynastyHandler.
    public string dynastyName;
    public string dynastyTitle;
    public int titleLevel = 0;
    public int maxBuildings = 1; // Does not refer to households, only workshops

    public List<Dynasty> friends = new List<Dynasty>();
    public List<Dynasty> neutral = new List<Dynasty>();
    public List<Dynasty> enemies = new List<Dynasty>();

    public Vector3 initPos;
    public Color dynastyColor;
    private Titles titles = new Titles();

    public List<string> availableClasses = new List<string>();
    public List<Person> members = new List<Person>();

    public List<Building> households = new List<Building>();
    public List<Building> workshops = new List<Building>();

    public GameObject baseHouseHold;


    public DynastyAI AI;

    public void Init(string arg, GameObject houseHold)
    {
        dynastyName = arg;
        initPos = houseHold.transform.position;
        initPos.x -= 15;
        baseHouseHold = houseHold;
        dynastyColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        dynastyTitle = titles.list[titleLevel].title;
        baseHouseHold.GetComponent<Renderer>().material.color = dynastyColor;

        households.Add(baseHouseHold.GetComponent<Building>());

        createMembers();
        assignHouseHoldOwner();
        initAI();
        getAvailableClasses();
    }



    public void assignHouseHoldOwner()
    {
        var houseHoldBuilding = baseHouseHold.GetComponent<Building>();

        houseHoldBuilding.belongsTo = members[Random.Range(0, members.Count)];
        houseHoldBuilding.dynasty = dynastyName;
    }



    public void initAI()
    {
        var helperObj = GameObject.FindGameObjectWithTag("HelperObject");

        DynastyAI instance = helperObj.AddComponent<DynastyAI>();

        instance.dynastyInstance = this;
        instance.availableClasses = availableClasses;
        instance.Init();
    }



    public void IncreaseMaxBuildings()
    {
        int increment = 1;
        maxBuildings += increment;
    }



    public void BuildWorkshop()
    {
        var workshopPrefabs = Resources.LoadAll<GameObject>("prefabs/buildings/workshops");
        var locations = Utils.getAvailableLocations();

        var chosenLocation = locations[Random.Range(0, locations.Length)];
        chosenLocation.GetComponent<locationMarker>().isOccupied = true;

        Utils.ShuffleArray(workshopPrefabs);

        foreach (var workshop in workshopPrefabs)
        {
            Building instance = workshop.GetComponent<Building>();

            if (availableClasses.Contains(instance.type) && dynastyMoney >= instance.price)
            {
                dynastyMoney -= instance.price;
                buildingCount++;

                members.ForEach(member =>
                {
                    if (member.type == instance.type && instance.belongsTo == null)
                    {
                        instance.belongsTo = member;
                        member.ownedWorkshops.Add(instance);
                    }
                });

                instance.dynasty = dynastyName;
                Instantiate(workshop, chosenLocation.transform.position, Quaternion.identity);
                workshops.Add(workshop.GetComponent<Building>());

                return;

            }

        }

    }



    public void BuildHouseHold()
    {

    }



    private void createMembers()
    {
        for (int i = 0; i < 3; i++)
        {
            initPos.x += i + 1;

            var personPrefab = (GameObject)Resources.Load("prefabs/people/person", typeof(GameObject));
            GameObject member = Instantiate(personPrefab, initPos, Quaternion.identity);

            Person instance = member.GetComponent<Person>();
            members.Add(instance);

            member.GetComponent<Person>().dynasty = dynastyName;
            member.GetComponent<Person>().dynastyInstance = this;
            member.GetComponent<Renderer>().material.color = dynastyColor;
        }
    }



    public void getAvailableClasses()
    {

        List<string> classes = new List<string>();

        members.ForEach(member =>
        {
            if (!classes.Contains(member.type))
            {
                classes.Add(member.type);
            }
        });

        availableClasses = classes;
    }



    public List<Person> getFreeMembers()
    {
        var freeMembers = new List<Person>();

        members.ForEach((member) =>
        {
            if (!member.isBusy)
            {
                freeMembers.Add(member);
            }
        });

        return freeMembers;
    }
}
