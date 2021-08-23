using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;


[System.Serializable]
public class CardTemplate
{
    public int cardID;
    public string cardName;
    public string cardDescription;
    public int cardAttack;
    public int cardHealth;
    public int cardCost;
    public CardType cardType;
    public CardClass cardClass;
    public CardKeywords key_word1;
    public CardKeywords key_word2;
    public CardKeywords key_word3;

    [SerializeField]
    public Sprite cardSprite;

    public CardTemplate(int cardID, string cardName, string cardDescription, int cardAttack, int cardHealth, int cardCost, CardType cardType, CardClass cardClass, Sprite sprite)
    {
        this.cardID = cardID;
        this.cardName = cardName;
        this.cardDescription = cardDescription;
        this.cardAttack = cardAttack;
        this.cardHealth = cardHealth;
        this.cardCost = cardCost;
        this.cardType = cardType;
        this.cardClass = cardClass;
        this.cardSprite = sprite;
    }

    public CardTemplate(int cardID, string cardName, string cardDescription, int cardAttack, int cardHealth, int cardCost, CardType cardType, CardClass cardClass, CardKeywords keyword_1, Sprite sprite)
    {
        this.cardID = cardID;
        this.cardName = cardName;
        this.cardDescription = cardDescription;
        this.cardAttack = cardAttack;
        this.cardHealth = cardHealth;
        this.cardCost = cardCost;
        this.cardType = cardType;
        this.cardClass = cardClass;
        this.cardSprite = sprite;
        this.key_word1 = keyword_1;
    }

    public CardTemplate(int cardID, string cardName, string cardDescription, int cardAttack, int cardHealth, int cardCost, CardType cardType, CardClass cardClass, CardKeywords keyword_1, CardKeywords keyword_2, Sprite sprite)
    {
        this.cardID = cardID;
        this.cardName = cardName;
        this.cardDescription = cardDescription;
        this.cardAttack = cardAttack;
        this.cardHealth = cardHealth;
        this.cardCost = cardCost;
        this.cardType = cardType;
        this.cardClass = cardClass;
        this.cardSprite = sprite;
        this.key_word1 = keyword_1;
        this.key_word2 = keyword_2;
    }

    public CardTemplate(int cardID, string cardName, string cardDescription, int cardAttack, int cardHealth, int cardCost, CardType cardType, CardClass cardClass, CardKeywords keyword_1, CardKeywords keyword_2, CardKeywords keyword_3, Sprite sprite)
    {
        this.cardID = cardID;
        this.cardName = cardName;
        this.cardDescription = cardDescription;
        this.cardAttack = cardAttack;
        this.cardHealth = cardHealth;
        this.cardCost = cardCost;
        this.cardType = cardType;
        this.cardClass = cardClass;
        this.cardSprite = sprite;
        this.key_word1 = keyword_1;
        this.key_word2 = keyword_2;
        this.key_word3 = keyword_3;
    }

    public CardTemplate(int cardID, string cardName, string cardDescription, int cardCost, CardType cardType, CardClass cardClass, Sprite sprite)
    {
        this.cardID = cardID;
        this.cardName = cardName;
        this.cardDescription = cardDescription;
        this.cardCost = cardCost;
        this.cardType = cardType;
        this.cardClass = cardClass;
        this.cardSprite = sprite;
    }
}
