using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titles
{

    public TitleData[] list =
    {
        new TitleData("Commoner", 0),
        new TitleData("Citizen", 2500),
        new TitleData("Patrician", 10000),
        new TitleData("Lord", 25000),
    };
};

public class TitleData
{
    public string title;
    public int price;

    public TitleData(string title, int price)
    {
        this.title = title; this.price = price;
    }
}
