using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    public void SetTitle(string text)
    {
        title.text = text;
        gameObject.name = text;
    }

    public void SetDescription(string desc)
    {
        description.text = desc;
    }

    public void SetProficiencies(List<string> starting_proficiencies)
    {
        description.text = "Your starting proficiencies are the following:";
        foreach (string prof in starting_proficiencies)
        {
            description.text += "\n\t- " + prof;
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
