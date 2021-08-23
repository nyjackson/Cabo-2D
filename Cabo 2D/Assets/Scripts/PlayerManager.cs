using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mirror;
// Need to update the cardfront in client rpc?
// SyncVar create card struct with cardfront, cardback,gameobject,string action?
// 05/26/2021: Retry cmdcreatedeck -> rpccreatdeck
// Deck.Adds in rpc not adding to public variable

/*
public struct Card {
    //public Sprite CardFront;
    public GameObject CardBack;
    public GameObject CardIdentity;
    public int Score;
    public string Action;
}
*/
public class Deck : SyncList<GameObject>{}
public class PlayerManager : NetworkBehaviour
{
    public GameObject CardOne;
    public GameObject CardTwo;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject DropZone;
    public Sprite[] cardFront = new Sprite[54];
    //public Deck deck = new Deck();

    List<GameObject> cardBack = new List<GameObject>();
    List<GameObject> deck = new List<GameObject>();
    List<GameObject> pile = new List<GameObject>();
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");
        DropZone = GameObject.Find("DropZone");
        cardFront = Resources.LoadAll<Sprite>("Sprites/Card Front");


    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        cardBack.Add(CardOne);
        cardBack.Add(CardTwo);
        
    }

    [Command] // when client requests server to do something
    public void CmdDealCards() {
        Debug.Log("CmdDealCards: Check Deck Count: " + deck.Count);
        if (deck.Count == 0) {
            Debug.Log("Deck Count is 0");

        }
        for (int i = 0; i < 4; i++)
        {
            GameObject card = deck[Random.Range(0, deck.Count)];
            //Card card = deck[Random.Range(0, deck.Count)];
            //GameObject cardIdentity = card.CardIdentity;

            Debug.Log("CmdDeal: Card is " + card.GetComponent<CardFlipper>().CardFront);
            
            NetworkServer.SpawnWithClientAuthority(card, connectionToClient); //gives authority to client to use game object 
            RpcShowCard(card, "Dealt");
            RpcRemoveCard(card);
            //deck.Remove(card);
        }
    }

   [Command]
    public void CmdCreateDeck() {
        RpcCreateDeck();
    }

    [ClientRpc]
    public void RpcCreateDeck() {
        for (int i = 0; i < 54; i++)
        {
            //Card card = new Card();
            //card.CardBack = cardBack[0];
            GameObject card = Instantiate(cardBack[0], new Vector2(0, 0), Quaternion.identity);
            //card.CardFront = cardFront[i];
            card.GetComponent<CardFlipper>().CardFront = cardFront[i];
            Debug.Log("CmdDCreate: Card is " + card.GetComponent<CardFlipper>().CardFront);
            deck.Add(card);
            //RpcLoadDeck(card);
            // RpcShowCard(card, "Added To Deck");
            //NetworkServer.Spawn(card, connectionToClient); //gives authority to client to use game object 
        }
    }
    public void PlayCard(GameObject card) {
        CmdPlayCard(card);
    }

    [Command] 
    void CmdPlayCard(GameObject card) {
        RpcShowCard(card,"Played");
    }
    [ClientRpc]
    void RpcLoadDeck(GameObject card) {
        deck.Add(card);
        Debug.Log("RpcLoadDeck: Card Added. New Deck Count: " + deck.Count);
        //Debug.Log("RpcLoadDeck: Card is " + card.GetComponent<CardFlipper>().CardFront);
    }
    [ClientRpc]
    void RpcRemoveCard(GameObject card) {
        deck.Remove(card);
        Debug.Log("RpcRemoveCard: Card Removed. New Deck Count: " + deck.Count);

    }

    [ClientRpc] //Remote Procedure Call, host tells all clients to do things 

    void RpcShowCard(GameObject card, string type) {
        if (type == "Dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(PlayerArea.transform, false);
                card.GetComponent<CardFlipper>().Flip(); // shows card to self
               // card.GetComponent<CardFlipper>().CardFront = deck[]
               //Debug.Log("RpcShow: Card is " + card.GetComponent<CardFlipper>().CardFront);


            }
            else
            {
                card.transform.SetParent(EnemyArea.transform, false);
                //con: a hacker can figure out what cardBack their opponent have, may want to change this logic later
            }
        }

        else if (type == "Played")
        {
            card.transform.SetParent(DropZone.transform, false);

            if (!hasAuthority)
            {
                card.GetComponent<CardFlipper>().Flip();
            }
        }
    }
}
