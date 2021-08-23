using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CardTwo;
    public string[] Actions = {"None", "LookCard","LookOtherCard", "BlindSwap","LookBlindSwap"};
    public int[] Points = {2,3,4,5,6,7,8,9,10,-1,1,0};
    public bool CaboCalled;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject DropPile;
    public GameObject DropDeck;
    public Sprite[] CardFronts = new Sprite[54];

    List<GameObject> deck = new List<GameObject>();
    List<GameObject> pile = new List<GameObject>();
    //this will be the start button

     void Start()
    {
        PlayerArea = GameObject.Find("Player1Area");
        EnemyArea = GameObject.Find("AIArea");
        DropPile = GameObject.Find("Pile");
        DropDeck = GameObject.Find("Deck");
        CardFronts = Resources.LoadAll<Sprite>("Sprites/Card Front");
        Debug.Log("All Resources Loaded");
    }

    public void CalculateScore() {

    }

    public void TakeAction(GameObject card) {

    }
    public void OnClick() {
        CreateDeck();
        DealCards();
    }
    public void DealCards()
    {
        Debug.Log("CmdDealCards: Check Deck Count: " + deck.Count);
        for (int i = 0; i < 4; i++)
        {
            GameObject card = deck[Random.Range(0, deck.Count)];
            Debug.Log("CmdDeal: Card is " + card.GetComponent<CardFlipper>().CardFront);
            ShowCard(card, "Dealt");
            deck.Remove(card);
        }
    }

    public void CreateDeck()
    {
        Debug.Log("Creating Deck");
        for (int i = 0; i < 54; i++)
        {
            GameObject card = Instantiate(CardTwo, new Vector2(0, 0), Quaternion.identity);
            card.GetComponent<CardFlipper>().CardFront = CardFronts[i];
            Debug.Log("CreateDeck: Card is " + card.GetComponent<CardFlipper>().CardFront);
            deck.Add(card);
        }
    }

    void ShowCard(GameObject card, string type)
    {
        if (type == "Dealt")
        {
            Debug.Log("Cards should be dealt and showing on screen.");
            card.transform.SetParent(PlayerArea.transform, false);
        }

        else if (type == "Played")
        {
            //card.transform.SetParent(DropZone.transform, false);
            card.GetComponent<CardFlipper>().Flip();
        }
    }
}
