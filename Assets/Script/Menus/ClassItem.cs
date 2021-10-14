using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassItem : MonoBehaviour
{
    private ClassMenu classMenu;

    [SerializeField] private Image iconNormal;
    [SerializeField] private Image iconSelected;

    [SerializeField] private TMP_Text textNormal;
    [SerializeField] private TMP_Text textSelected;

    [SerializeField] private Button button;

    [SerializeField] private GameObject selectedGO;

    public void ClassItemConstruct(DB.Class selectedClass, ClassMenu classMenu)
    {
        this.classMenu = classMenu;
        iconNormal.sprite = classMenu.IconImagesDict[selectedClass.index];
        iconSelected.sprite = classMenu.IconImagesDict[selectedClass.index];
        textNormal.text = selectedClass.name;
        textSelected.text = selectedClass.name;
        button.onClick.AddListener(() => { classMenu.SelectClass(selectedClass.index); OnSelected(); });
    }

    public void SetSelectedActive()
    {
        selectedGO.gameObject.SetActive(true);
    }

    public void OnSelected()
    {
        selectedGO.gameObject.SetActive(true);
        classMenu.SetSelected(this);
    }

    public void Diselect()
    {
        selectedGO.gameObject.SetActive(false);
    }
}
