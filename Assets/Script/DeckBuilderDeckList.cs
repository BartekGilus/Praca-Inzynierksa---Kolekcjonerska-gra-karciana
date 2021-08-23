using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeckBuilderDeckList : MonoBehaviour
{
    public int numberOfCardsInDeck;
    public List<int> cardsInDeck = new List<int>();

    void Start()
    {
        cardsInDeck.Clear();
    }

    void Update()
    {
        numberOfCardsInDeck = cardsInDeck.Count;
        GameObject.Find("NumberOfCards").GetComponent<Text>().text = "" + numberOfCardsInDeck;
    }
}
