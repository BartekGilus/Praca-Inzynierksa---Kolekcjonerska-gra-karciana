using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddCardToDeck : MonoBehaviour, IPointerClickHandler
{
    public GameObject deckListContent;
    public GameObject cardDisplay;
    public GameObject card;

    public int numberInDeck;

    void Start()
    {
        deckListContent = GameObject.Find("DeckListContent");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(numberInDeck < 4 && deckListContent.GetComponent<DeckBuilderDeckList>().cardsInDeck.Count < 40)
        {
            if (deckListContent.GetComponent<DeckBuilderDeckList>().cardsInDeck.Contains(eventData.pointerPress.GetComponent<Card>().thisCardID))
            {
                numberInDeck++;
                card.transform.GetChild(1).GetComponent<Text>().text = "x" + numberInDeck;

            }
            else
            {
                cardDisplay.transform.GetChild(0).GetComponent<Text>().text = eventData.pointerPress.GetComponent<Card>().cardName;
                numberInDeck++;
                cardDisplay.transform.GetChild(1).GetComponent<Text>().text = "x" + numberInDeck;
                card = Instantiate(cardDisplay, new Vector3(0, 0, 0), Quaternion.identity);
                card.transform.SetParent(deckListContent.transform);
                card.transform.localScale = new Vector3(1, 1, 1);

                if(eventData.pointerPress.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_HUMAN)
                    card.GetComponent<Image>().color = new Color32(255, 255, 180, 255);
                if (eventData.pointerPress.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_ELF)
                    card.GetComponent<Image>().color = new Color32(75, 150, 100, 255);
                if (eventData.pointerPress.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_NEUTRAL)
                    card.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
                if(eventData.pointerPress.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_SPELL)
                    card.GetComponent<Image>().color = new Color32(50, 100, 150, 255);
            }

            if (numberInDeck == 1)
                eventData.pointerPress.transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "x3";
            else if (numberInDeck == 2)
                eventData.pointerPress.transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "x2";
            else if (numberInDeck == 3)
                eventData.pointerPress.transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "x1";
            else
                eventData.pointerPress.transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "x0";

            deckListContent.GetComponent<DeckBuilderDeckList>().cardsInDeck.Add(eventData.pointerPress.GetComponent<Card>().thisCardID);
        }
    }
}
