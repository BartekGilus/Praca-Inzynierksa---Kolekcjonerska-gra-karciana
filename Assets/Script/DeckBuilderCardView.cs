using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderCardView : MonoBehaviour
{
    public List<CardTemplate> allCardsList = new List<CardTemplate>();
    public GameObject cardPrefab;
    public Text cardQuantity;

    void Start()
    {
        CreateCards();

    }


    void Update()
    {
        
    }

    private void CreateCards()
    {
        for (var i = 0; i < CardsDatabase.cardList.Count; i++)
        {
            allCardsList.Add(CardsDatabase.cardList[i]);
        }

        for (var i = 0; i < allCardsList.Count; i++)
        {
            cardPrefab.transform.GetChild(0).GetComponent<Card>().thisCardID = allCardsList[i].cardID - 1;
            GameObject card = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            card.transform.SetParent(this.transform);
            card.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        }
    }
}
