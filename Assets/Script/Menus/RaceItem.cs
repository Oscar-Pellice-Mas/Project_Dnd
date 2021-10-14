using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceItem : MonoBehaviour
{
    private RaceMenu raceMenu;

    [SerializeField] private Image iconNormal;

    [SerializeField] private TMP_Text textNormal;
    [SerializeField] private TMP_Text textSelected;

    [SerializeField] private Button button;

    [SerializeField] private GameObject selectedGO;

    public void ClassItemConstruct(DB.Race selectedRace, RaceMenu raceMenu)
    {
        this.raceMenu = raceMenu;
        iconNormal.sprite = raceMenu.IconImagesDict[selectedRace.index];
        textNormal.text = selectedRace.name;
        textSelected.text = selectedRace.name;
        button.onClick.AddListener(() => { raceMenu.SelectRace(selectedRace.index); OnSelected(); });
    }

    public void SetSelectedActive()
    {
        selectedGO.gameObject.SetActive(true);
    }

    public void OnSelected()
    {
        selectedGO.gameObject.SetActive(true);
        raceMenu.SetSelected(this);
    }

    public void Diselect()
    {
        selectedGO.gameObject.SetActive(false);
    }
}
