using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassMenu : MonoBehaviour
{
    private Dictionary<string, DB.Class> classDict;
    private Dictionary<string, GameObject> btnDict;

    private DB.Class selectedClass;
    private ClassItem selectedClassItem;

    private Dictionary<string, Sprite> portraitImagesDict;
    private Dictionary<string, Sprite> iconImagesDict;
    [SerializeField] private List<Sprite> portraitImagesList;
    [SerializeField] private List<Sprite> iconImagesList;

    [SerializeField] private GameObject scrollPanel;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject reverseBtn;

    [SerializeField] private Image selectedPortrait;
    [SerializeField] private Image selectedIcon;
    [SerializeField] private TMP_Text roleText;
    [SerializeField] private TMP_Text descText;

    public Dictionary<string, Sprite> IconImagesDict { get => iconImagesDict; set => iconImagesDict = value; }

    private void Awake()
    {
        portraitImagesDict = new Dictionary<string, Sprite>();
        foreach (Sprite item in portraitImagesList)
        {
            portraitImagesDict.Add(item.name, item);
        }

        IconImagesDict = new Dictionary<string, Sprite>();
        foreach (Sprite item in iconImagesList)
        {
            IconImagesDict.Add(item.name, item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        classDict = DB.Instance.dictionary_Class;
        btnDict = new Dictionary<string, GameObject>();
        foreach (DB.Class element in classDict.Values)
        {
            GameObject classBtn = Instantiate(itemPrefab, scrollPanel.transform,false);
            classBtn.name = element.index;
            classBtn.GetComponent<ClassItem>().ClassItemConstruct(element, this);
            if (selectedClassItem == null)
            {
                selectedClassItem = classBtn.GetComponent<ClassItem>();
                selectedClassItem.SetSelectedActive();
            }
            btnDict.Add(element.index, classBtn);
        }
    }

    public void SelectClass(string index)
    {
        selectedClass = classDict[index];
        RefreshClass();
    }

    private void RefreshClass()
    {
        selectedPortrait.sprite = portraitImagesDict[selectedClass.name];
        selectedIcon.sprite = IconImagesDict[selectedClass.index];
        roleText.text = selectedClass.name;
        descText.text = selectedClass.desc;
    }

    public void SetSelected(ClassItem classItem)
    {
        if(selectedClassItem != null) selectedClassItem.Diselect();
        selectedClassItem = classItem;
    }

    public void ConfirmSelection()
    {
        foreach (GameObject gameObject in btnDict.Values)
        {
            if (gameObject.name != selectedClass.index) gameObject.SetActive(false);
        }
        reverseBtn.SetActive(true);
    }

    public void ReverseSelection()
    {
        foreach (GameObject gameObject in btnDict.Values)
        {
            gameObject.SetActive(true);
        }
        reverseBtn.SetActive(false);
    }
}
