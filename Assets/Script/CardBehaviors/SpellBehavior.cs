using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehavior : MonoBehaviour
{
    private bool enumeratorDone;
    public GameObject card;

    void OnEnable()
    {

        switch (this.gameObject.GetComponent<Card>().cardName)
        {
            case "Kula ognia":
                Kula_Ognia();
                break;
            case "Deszcz Meteorytów":
                Deszcz_Meteorytow(4);
                break;
            case "Lodowe pociski":
                Deszcz_Meteorytow(2);
                break;
            case "Starożytna wiedza":
                Starozytna_Wiedza();
                break;
            case "Kataklizm":
                Kataklizm();
                break;
            case "Latające noże":
                Latajace_Noze(3);
                break;
            case "Ognisty podmuch":
                Latajace_Noze(5);
                break;
            case "Wysysanie życia":
                Wysysanie_Zycia();
                break;
            case "Uzdrowienie":
                Uzdrowienie();
                break;
            case "Śmiertelna kołysanka":
                Smiertelna_Kolysnaka();
                break;
            case "Zniszczenie artefaktu":
                Zniszczenie_Artefaktu();
                break;
            case "Odrodzenie stworów":
                Odrodzenie();
                break;
        }
    }

    void OnDisable()
    {
        if (this.GetComponent<CardController>().spellUsed)
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.transform.GetChild(4).gameObject.activeSelf)
                        o.transform.GetChild(4).gameObject.SetActive(false);
                }
            }

            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.transform.GetChild(4).gameObject.activeSelf)
                        o.transform.GetChild(4).gameObject.SetActive(false);
                }
            }

            if (this.GetComponent<CardController>().target != null)
                this.GetComponent<CardController>().target = null;

            this.GetComponent<CardController>().target = null;
            this.GetComponent<ZoomOnCard>().EndZooming();
            this.GetComponent<Card>().RemoveFromTheField();
            Destroy(this.gameObject);
        }
    }

    public void Kula_Ognia()
    {
        if(this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            if (!this.GetComponent<CardController>().spellUsed)
            {
                foreach (var o in enemyCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
                if (this.GetComponent<CardController>().target != null)
                {
                    this.GetComponent<CardController>().target.gameObject.GetComponent<Card>().cardHealth -= 3;
                    if (this.GetComponent<CardController>().target.GetComponent<Card>().cardHealth <= 0)
                    {
                        this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                        Destroy(this.GetComponent<CardController>().target.gameObject);
                    }
                    this.GetComponent<CardController>().spellUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            if (!this.GetComponent<CardController>().spellUsed)
            {
                foreach (var o in playerCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
                if (this.GetComponent<CardController>().target != null)
                {
                    this.GetComponent<CardController>().target.gameObject.GetComponent<Card>().cardHealth -= 3;
                    if (this.GetComponent<CardController>().target.GetComponent<Card>().cardHealth <= 0)
                    {
                        this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                        Destroy(this.GetComponent<CardController>().target.gameObject);
                    }
                    this.GetComponent<CardController>().spellUsed = true;
                }
            }
        }
    }

    public void Deszcz_Meteorytow(int damage)
    {
        StartCoroutine(WaitAfterPlayed());
        if (enumeratorDone)
        {
            if(this.gameObject.tag == "PlayerCard")
            {
                GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
                foreach (var o in enemyCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.GetComponent<Card>().cardHealth -= damage;
                        if (o.GetComponent<Card>().cardHealth <= 0)
                        {
                            o.GetComponent<Card>().RemoveFromTheField();
                            Destroy(o.gameObject);
                        }
                    }
                }

                this.GetComponent<CardController>().spellUsed = true;
            }

            if (this.gameObject.tag == "EnemyCard")
            {
                GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
                foreach (var o in playerCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.GetComponent<Card>().cardHealth -= damage;
                        if (o.GetComponent<Card>().cardHealth <= 0)
                        {
                            o.GetComponent<Card>().RemoveFromTheField();
                            Destroy(o.gameObject);
                        }
                    }
                }

                this.GetComponent<CardController>().spellUsed = true;
            }

        }
            
    }

    private void Starozytna_Wiedza()
    {
        StartCoroutine(WaitAfterPlayed());
        if (enumeratorDone)
        {
            if(this.gameObject.tag == "PlayerCard")
            { 
                if (this.GetComponent<CardController>().isOnGameField)
                {
                    GameObject.Find("PlayerDeck").GetComponent<DrawCard>().Draw();
                }
                this.GetComponent<CardController>().spellUsed = true;
            }

            if (this.gameObject.tag == "EnemyCard")
            {
                if (this.GetComponent<CardController>().isOnGameField)
                {
                    GameObject.Find("EnemyDeck").GetComponent<DrawCard>().Draw();
                }
                this.GetComponent<CardController>().spellUsed = true;
            }
        }
    }

    private void Kataklizm()
    {
        StartCoroutine(WaitAfterPlayed());
        if (enumeratorDone)
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    o.GetComponent<Card>().RemoveFromTheField();
                    Destroy(o.gameObject);
                }
            }

            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    o.GetComponent<Card>().RemoveFromTheField();
                    Destroy(o.gameObject);
                }
            }

            this.GetComponent<CardController>().spellUsed = true;
        }
    }

    private void Latajace_Noze(int damage)
    {
        StartCoroutine(WaitAfterPlayed());
        if (enumeratorDone)
        {
            if(this.gameObject.tag == "PlayerCard")
            {
                if (this.GetComponent<CardController>().isOnGameField)
                {
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyHealth -= damage;
                }
                this.GetComponent<CardController>().spellUsed = true;
            }

            if (this.gameObject.tag == "EnemyCard")
            {
                if (this.GetComponent<CardController>().isOnGameField)
                {
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerHealth -= damage;
                }
                this.GetComponent<CardController>().spellUsed = true;
            }
        }
    }

    private void Wysysanie_Zycia()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            if (!this.GetComponent<CardController>().spellUsed)
            {
                foreach (var o in enemyCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
                if (this.GetComponent<CardController>().target != null)
                {
                    this.GetComponent<CardController>().target.gameObject.GetComponent<Card>().cardHealth -= 4;
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerHealth += 4;
                    if (this.GetComponent<CardController>().target.GetComponent<Card>().cardHealth <= 0)
                    {
                        this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                        Destroy(this.GetComponent<CardController>().target.gameObject);
                    }
                    this.GetComponent<CardController>().spellUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            if (!this.GetComponent<CardController>().spellUsed)
            {
                foreach (var o in playerCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
                if (this.GetComponent<CardController>().target != null)
                {
                    this.GetComponent<CardController>().target.gameObject.GetComponent<Card>().cardHealth -= 4;
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyHealth += 4;
                    if (this.GetComponent<CardController>().target.GetComponent<Card>().cardHealth <= 0)
                    {
                        this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                        Destroy(this.GetComponent<CardController>().target.gameObject);
                    }
                    this.GetComponent<CardController>().spellUsed = true;
                }
            }
        }
    }

    private void Uzdrowienie()
    {
        StartCoroutine(WaitAfterPlayed());
        if (enumeratorDone)
        {
            if (this.gameObject.tag == "PlayerCard")
            {
                if (this.GetComponent<CardController>().isOnGameField)
                {
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerHealth += 4;
                }
                this.GetComponent<CardController>().spellUsed = true;
            }

            if (this.gameObject.tag == "EnemyCard")
            {
                if (this.GetComponent<CardController>().isOnGameField)
                {
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyHealth += 4;
                }
                this.GetComponent<CardController>().spellUsed = true;
            }
        }
    }

    private void Smiertelna_Kolysnaka()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            if (!this.GetComponent<CardController>().spellUsed)
            {
                foreach (var o in enemyCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
                if (this.GetComponent<CardController>().target != null)
                {
                    this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                    Destroy(this.GetComponent<CardController>().target.gameObject);
                    this.GetComponent<CardController>().spellUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            if (!this.GetComponent<CardController>().spellUsed)
            {
                foreach (var o in playerCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        o.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
                if (this.GetComponent<CardController>().target != null)
                {
                    this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                    Destroy(this.GetComponent<CardController>().target.gameObject);
                    this.GetComponent<CardController>().spellUsed = true;
                }
            }
        }
    }

    private void Zniszczenie_Artefaktu()
    {
        GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
        GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
        if (!this.GetComponent<CardController>().spellUsed)
        {
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<CardController>().isArtifact)
                        o.transform.GetChild(4).gameObject.SetActive(true);
                }
            }

            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<CardController>().isArtifact)
                        o.transform.GetChild(4).gameObject.SetActive(true);
                }
            }


            if (this.GetComponent<CardController>().target != null)
            {
                this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                Destroy(this.GetComponent<CardController>().target.gameObject);
                this.GetComponent<CardController>().spellUsed = true;
            }
        }
    }

    private void Odrodzenie()
    {
        StartCoroutine(WaitAfterPlayed());
        if (enumeratorDone)
        {
            if (this.gameObject.tag == "PlayerCard")
            {
                GameObject graveyard = GameObject.Find("PlayerGraveyard");
                List<CardTemplate> cards = new List<CardTemplate>();
                List<int> indexes = new List<int>();

                foreach (var o in graveyard.GetComponent<Graveyard>().playerGraveyard)
                {
                    if (o.cardType == Cards.CardType.CARDTYPE_MONSTER)
                    {
                        cards.Add(o);
                        indexes.Add(graveyard.GetComponent<Graveyard>().playerGraveyard.IndexOf(o));
                    }
                }

                int index = cards.Count;
                int place = 0;

                if (index > 0)
                {
                    int x = Random.Range(0, index - 1);
                    place = indexes[x];

                    card.GetComponent<Card>().thisCardID = cards[x].cardID - 1;
                    GameObject card2 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                    card2.transform.SetParent(GameObject.Find("PlayerHandZone").transform);
                    card2.GetComponent<CardController>().isOnGameField = false;
                    card2.transform.localScale = new Vector3(0.9f, 0.9f, 1);

                    graveyard.GetComponent<Graveyard>().playerGraveyard.RemoveAt(place);
                }
                this.GetComponent<CardController>().spellUsed = true;
            }
            if (this.gameObject.tag == "EnemyCard")
            {
                GameObject graveyard = GameObject.Find("EnemyGraveyard");
                List<CardTemplate> cards = new List<CardTemplate>();
                List<int> indexes = new List<int>();

                foreach (var o in graveyard.GetComponent<Graveyard>().enemyGraveyard)
                {
                    if (o.cardType == Cards.CardType.CARDTYPE_MONSTER)
                    {
                        cards.Add(o);
                        indexes.Add(graveyard.GetComponent<Graveyard>().enemyGraveyard.IndexOf(o));
                    }
                }

                int index = cards.Count;
                int place = 0;

                if (index > 0)
                {
                    int x = Random.Range(0, index - 1);
                    place = indexes[x];

                    card.GetComponent<Card>().thisCardID = cards[x].cardID - 1;
                    GameObject card2 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                    card2.transform.SetParent(GameObject.Find("PlayerHandZone").transform);
                    card2.GetComponent<CardController>().isOnGameField = false;
                    card2.transform.localScale = new Vector3(0.9f, 0.9f, 1);

                    graveyard.GetComponent<Graveyard>().enemyGraveyard.RemoveAt(place);
                }
                this.GetComponent<CardController>().spellUsed = true;
            }
        }
    }

    IEnumerator WaitAfterPlayed()
    {
        yield return new WaitForSecondsRealtime(2);
        enumeratorDone = true;
    }
}


