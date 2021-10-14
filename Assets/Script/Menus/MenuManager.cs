using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        foreach (Menu menu in menus)
        {
            CloseMenu(menu);
        }
        OpenMenu("MainMenu");
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].MenuName == menuName)
            {
                OpenMenu(menus[i]);
            }
            else if (menus[i].IsOpen())
            {
                CloseMenu(menus[i]);
            }
        }
    }

    private void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    public void OpenMenu(Menu menu)
    {
        menu.Open();
    }
}
