using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceMenu : MonoBehaviour
{
    private Dictionary<string, DB.Race> racesDict;
    private Dictionary<string, GameObject> btnDict;

    private DB.Race selectedRace;
    private RaceItem selectedRaceItem;

    private Dictionary<string,Sprite> portraitImagesDict;
    private Dictionary<string,Sprite> iconImagesDict;
    [SerializeField] private List<Sprite> portraitImagesList;
    [SerializeField] private List<Sprite> iconImagesList;

    [SerializeField] private GameObject scrollItemsPanel;
    [SerializeField] private GameObject scrollInfoPanel;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject infoPrefab;
    [SerializeField] private GameObject reverseBtn;

    [SerializeField] private Image selectedPortrait;
    [SerializeField] private Image selectedIcon;
    [SerializeField] private TMP_Text roleText;

    public Dictionary<string, Sprite> IconImagesDict { get => iconImagesDict; set => iconImagesDict = value; }

    private void Awake()
    {
        portraitImagesDict = new Dictionary<string, Sprite>();
        foreach (Sprite item in portraitImagesList)
        {
            portraitImagesDict.Add(item.name, item);
        }

        iconImagesDict = new Dictionary<string, Sprite>();
        foreach (Sprite item in iconImagesList)
        {
            iconImagesDict.Add(item.name, item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bool first = true;
        racesDict = DB.Instance.dictionary_Race;
        btnDict = new Dictionary<string, GameObject>();
        foreach (DB.Race element in racesDict.Values)
        {
            GameObject raceBtn = Instantiate(itemPrefab, scrollItemsPanel.transform, false);
            raceBtn.name = element.index;
            raceBtn.GetComponent<RaceItem>().ClassItemConstruct(element, this);
            if (selectedRaceItem == null)
            {
                selectedRaceItem = raceBtn.GetComponent<RaceItem>();
                selectedRaceItem.SetSelectedActive();
            }
            btnDict.Add(element.index, raceBtn);
            if (first)
            {
                SelectRace(element.index);
                first = false;
            } 
        }

    }

    public void SelectRace(string index)
    {
        selectedRace = racesDict[index];
        PlayerCharacter.Instance.SetRace(selectedRace.index);
        RefreshRace();
    }

    private void RefreshRace()
    {
        selectedPortrait.sprite = portraitImagesDict[selectedRace.index];
        selectedIcon.sprite = IconImagesDict[selectedRace.index];
        roleText.text = selectedRace.name;

        foreach (Transform child in scrollInfoPanel.transform)
        {
            Destroy(child.gameObject);
        }


        // Info
        InfoContainer infoItem = Instantiate(infoPrefab, scrollInfoPanel.transform, false).GetComponent<InfoContainer>();
        infoItem.SetTitle("Alignment");
        infoItem.SetDescription(selectedRace.alignment);

        infoItem = Instantiate(infoPrefab, scrollInfoPanel.transform, false).GetComponent<InfoContainer>();
        infoItem.SetTitle("Age");
        infoItem.SetDescription(selectedRace.age);

        infoItem = Instantiate(infoPrefab, scrollInfoPanel.transform, false).GetComponent<InfoContainer>();
        infoItem.SetTitle("Size");
        infoItem.SetDescription(selectedRace.size_description);

        infoItem = Instantiate(infoPrefab, scrollInfoPanel.transform, false).GetComponent<InfoContainer>();
        infoItem.SetTitle("Proficiencies");
        infoItem.SetProficiencies(selectedRace.starting_proficiencies);

        infoItem = Instantiate(infoPrefab, scrollInfoPanel.transform, false).GetComponent<InfoContainer>();
        infoItem.SetTitle("Lenguages");
        infoItem.SetDescription(selectedRace.language_desc);

    }

    public void SetSelected(RaceItem raceItem)
    {
        if (selectedRaceItem != null) selectedRaceItem.Diselect();
        selectedRaceItem = raceItem;
    }

    public void ConfirmSelection()
    {
        foreach (GameObject gameObject in btnDict.Values)
        {
            if (gameObject.name != selectedRace.index) gameObject.SetActive(false);
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

    private void GenerateOptions()
    {
        //selectedRace.
    }
}
