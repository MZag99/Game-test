using System.Collections;
using System.Collections.Generic;
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

    public Person lover;
    public Person spouse;

    public Action currentAction;
    public bool isBusy;

    private List<string> genders = new List<string>() { "male", "female" };
    private List<string> types = new List<string>() { "Scholar", "Craftsman", "Patron", "Rogue" };
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
        } else
        {
            name = names.femaleNames[Random.Range(0, names.femaleNames.Count)];
        }

        initAI();
    }



    void Update()
    {
        
    }



    public void setAction()
    {

    }



    public void initAI()
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
