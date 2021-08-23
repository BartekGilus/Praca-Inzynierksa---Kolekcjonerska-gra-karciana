using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    public GameObject Hand;
    public GameObject drawnCard;

    public PlayerDeck deck;

    void Start()
    {
        deck = this.GetComponent<PlayerDeck>();
    }

    public void Draw()
    {
        if (deck.numberOfCardsInDeck == -1)
        {
            return;
        }

        drawnCard.GetComponent<Card>().thisCardID = deck.playerDeck[deck.numberOfCardsInDeck].cardID - 1;
        drawnCard.GetComponent<Card>().isCovered = false;
        deck.playerDeck.RemoveAt(deck.numberOfCardsInDeck);

        GameObject playerCard = Instantiate(drawnCard, new Vector3(0, 0, 0), Quaternion.identity);
        if(Hand.name == "PlayerHandZone")
            playerCard.gameObject.tag = "PlayerCard";
        else if(Hand.name == "EnemyHandZone")
            playerCard.gameObject.tag = "EnemyCard";
        else
            playerCard.gameObject.tag = "None";
        playerCard.transform.SetParent(Hand.transform, false);
    }

}
