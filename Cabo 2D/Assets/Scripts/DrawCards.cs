using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards: NetworkBehaviour
{
    // Start is called before the first frame update
    public PlayerManager PlayerManager;
   

    public void OnClick() 
    { // for deal cards button
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.CmdCreateDeck();
        PlayerManager.CmdDealCards();
    }

  
}
