using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InformPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] AvailablePanels = new Sprite[1];
    public string ButtonName;
    public bool isActive = false;

    
    void Start()
    {
        AvailablePanels = Resources.LoadAll<Sprite>("Sprites/Panels");
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        ButtonName = EventSystem.current.currentSelectedGameObject.name;
        LoadCorrectImage(ButtonName);
    }

    void LoadCorrectImage(string buttonName)
    {
        Debug.Log("LCI: Button Name is: " + buttonName);
        if (buttonName == "Rules" || buttonName == "Rules1")
        {
            gameObject.GetComponent<Image>().sprite = null;
            gameObject.GetComponentInChildren<Text>().enabled = true;
            gameObject.SetActive(true);
        }
        else if (buttonName == "CheatSheet" || buttonName == "CheatSheet1")
        {
            gameObject.SetActive(true);
            gameObject.GetComponentInChildren<Text>().enabled = false;
            gameObject.GetComponent<Image>().sprite = AvailablePanels[0];

        }


    }
}
