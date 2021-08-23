using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;

public class InfoPanel : NetworkBehaviour
{
    public Sprite[] AvailablePanels = new Sprite[1];
    public string ButtonName;
    public bool isActive = false;

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        AvailablePanels = Resources.LoadAll<Sprite>("Sprites/Panels");
        gameObject.SetActive(false);
    }

    public void OnClick() {
        ButtonName = EventSystem.current.currentSelectedGameObject.name;
        CmdLoadCorrectImage(ButtonName);
    }

    [Command]
    void CmdLoadCorrectImage(string buttonName)
    {
        //RpcLoadCorrectImage(buttonName);
        if (hasAuthority || !hasAuthority)
        {
            Debug.Log("CLCI: Button Name is: " + buttonName);

            if (buttonName == "Rules" || buttonName == "Rules1")
            {
                gameObject.GetComponent<Image>().sprite = null;
                gameObject.GetComponentInChildren<Text>().enabled = true;
                gameObject.SetActive(true);
            }

            else if (buttonName == "CheatSheet" || buttonName == "CheatSheet1")
            {

                NetworkServer.SpawnWithClientAuthority(gameObject);

                gameObject.SetActive(true);
                gameObject.GetComponentInChildren<Text>().enabled = false;
                gameObject.GetComponent<Image>().sprite = AvailablePanels[0];
            }
        }
    }

    [ClientRpc]
    void RpcLoadCorrectImage(string buttonName) {
      
    }
    [Command]
    public void CmdSpawnObject(GameObject gameObject) {
        NetworkServer.SpawnWithClientAuthority(gameObject);
    }
    [Command]
    public void CmdUnSpawnObject(GameObject gameObject) {
        NetworkServer.UnSpawn(gameObject);
    }
}
