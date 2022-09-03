using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AI;
using UnityEngine;

public class Person : MonoBehaviour
{
    public int age;
    public int level;
    public int hp;

    public string name;
    public string dynasty;
    public string type;
    public string sex;

    public Dynasty dynastyInstance;
    public Person lover;
    public Person spouse;

    public Action currentAction;
    public bool isBusy;

    private List<string> genders = new List<string>() { "male", "female" };
    private List<string> types = new List<string>() { "Scholar", "Craftsman", "Patron", "Rogue" };
    public List<Building> ownedWorkshops = new List<Building>();
    private NamesObject names = new NamesObject();

    public PersonAI AI;

    void Awake()
    {
        age = Random.Range(18, 30);
        sex = genders[Random.Range(0, 2)];
        type = types[Random.Range(0, types.Count)];

        if (sex == "male")
        {
            name = names.maleNames[Random.Range(0, names.maleNames.Count)];
        }
        else
        {
            name = names.femaleNames[Random.Range(0, names.femaleNames.Count)];
        }

        InitAI();
    }



    public void MoveTo(GameObject target)
    {
        Transform goal = target.transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.destination = goal.position;
    }



    public void SetAction(string actionName, int priority)
    {
        currentAction = new Action(actionName, priority);
        isBusy = true;

    }



    public void FinishAction()
    {
        currentAction = null;
        isBusy = false;
    }



    public void InitAI()
    {
        var helperObj = GameObject.FindGameObjectWithTag("HelperObject");

        PersonAI instance = helperObj.AddComponent<PersonAI>();
    }




    public class NamesObject
    {
        public List<string> maleNames = new List<string>() {
        "Adam", "Donald", "Hans", "Thomas", "Moshe", "Alosha", "Jan"
        };
        public List<string> femaleNames = new List<string>() {
        "Maria", "Hildegard", "Hila", "Gertrude", "Patricia", "Shoshanna", "Dorotha"
        };
    }
}
