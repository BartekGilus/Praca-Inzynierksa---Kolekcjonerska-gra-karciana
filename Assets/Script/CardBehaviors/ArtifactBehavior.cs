using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBehavior : MonoBehaviour
{
    void OnEnable()
    {
        if(this.transform.childCount == 1) 
        {
            switch (this.transform.GetChild(0).gameObject.GetComponent<Card>().cardName)
            {
                case "Naszyjnik życia":
                    Naszyjnik_Zycia();
                    break;
                case "Legendarny krasnoludzki róg":
                    Krasnoludzki_Rog();
                    break;
                case "Kryształ many":
                    Krysztal_Many();
                    break;
                case "Serce elfów":
                    Serce_Elfow();
                        break;
                case "Księga śmierci":
                    Ksiega_Smierci();
                    break;
            }
        }
        
    }

    void OnDisable()
    {
        
    }

    private void Naszyjnik_Zycia()
    {
        if (this.transform.GetChild(0).gameObject.tag == "PlayerCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] playerField = GameObject.FindGameObjectsWithTag("PlayerCard");
                foreach (var o in playerField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        {
                            o.GetComponent<Card>().cardHealth += 1;
                        }
                    }
                }
            }
        }

        if (this.transform.GetChild(0).gameObject.tag == "EnemyCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] enemyField = GameObject.FindGameObjectsWithTag("EnemyCard");
                foreach (var o in enemyField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        {
                            o.GetComponent<Card>().cardHealth += 1;
                        }
                    }
                }
            }
        }
    }

    private void Krasnoludzki_Rog()
    {
        if (this.transform.GetChild(0).gameObject.tag == "PlayerCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] playerField = GameObject.FindGameObjectsWithTag("PlayerCard");
                foreach (var o in playerField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        {
                            o.GetComponent<Card>().cardAttack += 2;
                        }
                    }
                }
            }
        }

        if (this.transform.GetChild(0).gameObject.tag == "EnemyCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] enemyField = GameObject.FindGameObjectsWithTag("EnemyCard");
                foreach (var o in enemyField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        {
                            o.GetComponent<Card>().cardAttack += 2;
                        }
                    }
                }
            }
        }
    }

    private void Krysztal_Many()
    {
        if (this.transform.GetChild(0).gameObject.tag == "PlayerCard")
            GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerMana += 1;

        if (this.transform.GetChild(0).gameObject.tag == "EnemyCard")
            GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyMana += 1;
    }

    private void Serce_Elfow()
    {
        if (this.transform.GetChild(0).gameObject.tag == "PlayerCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] playerField = GameObject.FindGameObjectsWithTag("PlayerCard");
                foreach (var o in playerField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER && o.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_ELF)
                        {
                            o.GetComponent<Card>().cardAttack += 1;
                            o.GetComponent<Card>().cardHealth += 2;
                        }
                    }
                }
            }
        }

        if (this.transform.GetChild(0).gameObject.tag == "EnemyCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] enemyField = GameObject.FindGameObjectsWithTag("EnemyCard");
                foreach (var o in enemyField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER && o.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_ELF)
                        {
                            o.GetComponent<Card>().cardAttack += 1;
                            o.GetComponent<Card>().cardHealth += 2;
                        }
                    }
                }
            }
        }
    }

    private void Ksiega_Smierci()
    {
        if (this.transform.GetChild(0).gameObject.tag == "PlayerCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] enemyField = GameObject.FindGameObjectsWithTag("EnemyCard");
                foreach (var o in enemyField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        {
                            o.GetComponent<Card>().cardHealth -= 2;
                            if(o.GetComponent<Card>().cardHealth <= 0)
                            {
                                o.GetComponent<Card>().RemoveFromTheField();
                                Destroy(o.gameObject);
                            }
                        }
                    }
                }
            }
        }

        if (this.transform.GetChild(0).gameObject.tag == "EnemyCard")
        {
            if (this.transform.GetChild(0).gameObject.GetComponent<CardController>().isOnGameField)
            {
                GameObject[] playerField = GameObject.FindGameObjectsWithTag("PlayerCard");
                foreach (var o in playerField)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        {
                            o.GetComponent<Card>().cardHealth -= 2;
                            if (o.GetComponent<Card>().cardHealth <= 0)
                            {
                                o.GetComponent<Card>().RemoveFromTheField();
                                Destroy(o.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }
}
