using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class PlayerDeck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<CardTemplate> playerDeck = new List<CardTemplate>();
    public Text cardsInPlayerDeckText;
    public Text cardsInEnemyDeckText;

    public List<CardTemplate> enemyDeck1 = new List<CardTemplate>();
    public List<CardTemplate> enemyDeck2 = new List<CardTemplate>();
    public List<CardTemplate> playerDeck2 = new List<CardTemplate>();

    public int x;
    public int deckSize;
    public int numberOfCardsInDeck;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerEnter.gameObject.tag == "PlayerDeck")
            cardsInPlayerDeckText.text = " " + numberOfCardsInDeck;

        if(eventData.pointerEnter.gameObject.tag == "EnemyDeck")
            cardsInEnemyDeckText.text = " " + numberOfCardsInDeck;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardsInPlayerDeckText.text = " ";
        cardsInEnemyDeckText.text = " ";
    }

    void Start()
    {
        x = 0;
        deckSize = 40;

        if(this.gameObject.tag == "EnemyDeck")
        {
            int y = Random.Range(0, 2);
            Debug.Log(y);
            if (y == 0)
            {
                CreateEnemyDeck1();
                playerDeck = Shuffle(enemyDeck1);
            }
            else
            {
                CreateEnemyDeck2();
                playerDeck = Shuffle(enemyDeck2);
            }
                
        }
        else
        {
            if (System.IO.File.Exists("deck.dat"))
            {
                using (Stream stream = File.Open("deck.dat", FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    List<int> ids = (List<int>)bformatter.Deserialize(stream);

                    foreach (var i in ids)
                    {
                        playerDeck2.Add(CardsDatabase.cardList[i]);
                    }
                }
            }
            else
                CreatePlayerDeck();
            playerDeck = Shuffle(playerDeck2);
        }
        

       
        StartCoroutine(PlayerDeckRoutine());
        
    }

    void Update()
    {
        numberOfCardsInDeck = playerDeck.Count - 1;
    }

    IEnumerator PlayerDeckRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        for (var i = 0; i < 5; i++)
        {
            this.GetComponent<DrawCard>().Draw();
            yield return wait;
        }

        GameObject.Find("TurnSystem").GetComponent<TurnSystem>().turnState = TurnSystem.TurnStates.TURNSTATE_BEGINTURN;
    }

    private List<CardTemplate> Shuffle(List<CardTemplate> list)
    {
        int i1, i2;
        CardTemplate x;

        for(var i = 1; i < 60; i++)
        {
            i1 = Random.Range(0, list.Count);
            i2 = Random.Range(0, list.Count);
            x = list[i1]; list[i1] = list[i2]; list[i2] = x;
        }

        return list;
    }

    private void CreateEnemyDeck1()
    {
        enemyDeck1.Add(CardsDatabase.cardList[11]);
        enemyDeck1.Add(CardsDatabase.cardList[11]);
        enemyDeck1.Add(CardsDatabase.cardList[11]);

        enemyDeck1.Add(CardsDatabase.cardList[13]);
        enemyDeck1.Add(CardsDatabase.cardList[13]);
        enemyDeck1.Add(CardsDatabase.cardList[13]);

        enemyDeck1.Add(CardsDatabase.cardList[5]);
        enemyDeck1.Add(CardsDatabase.cardList[5]);
        enemyDeck1.Add(CardsDatabase.cardList[5]);

        enemyDeck1.Add(CardsDatabase.cardList[0]);
        enemyDeck1.Add(CardsDatabase.cardList[0]);
        enemyDeck1.Add(CardsDatabase.cardList[0]);

        enemyDeck1.Add(CardsDatabase.cardList[16]);
        enemyDeck1.Add(CardsDatabase.cardList[16]);
        enemyDeck1.Add(CardsDatabase.cardList[16]);

        enemyDeck1.Add(CardsDatabase.cardList[8]);
        enemyDeck1.Add(CardsDatabase.cardList[8]);

        enemyDeck1.Add(CardsDatabase.cardList[18]);
        enemyDeck1.Add(CardsDatabase.cardList[18]);
        enemyDeck1.Add(CardsDatabase.cardList[18]);

        enemyDeck1.Add(CardsDatabase.cardList[42]);
        enemyDeck1.Add(CardsDatabase.cardList[42]);

        enemyDeck1.Add(CardsDatabase.cardList[23]);
        enemyDeck1.Add(CardsDatabase.cardList[23]);
        enemyDeck1.Add(CardsDatabase.cardList[23]);

        enemyDeck1.Add(CardsDatabase.cardList[48]);

        enemyDeck1.Add(CardsDatabase.cardList[46]);
        enemyDeck1.Add(CardsDatabase.cardList[46]);

        enemyDeck1.Add(CardsDatabase.cardList[45]);
        enemyDeck1.Add(CardsDatabase.cardList[45]);

        enemyDeck1.Add(CardsDatabase.cardList[49]);
        enemyDeck1.Add(CardsDatabase.cardList[49]);
        enemyDeck1.Add(CardsDatabase.cardList[49]);

        enemyDeck1.Add(CardsDatabase.cardList[50]);
        enemyDeck1.Add(CardsDatabase.cardList[50]);

        enemyDeck1.Add(CardsDatabase.cardList[53]);
        enemyDeck1.Add(CardsDatabase.cardList[53]);

        enemyDeck1.Add(CardsDatabase.cardList[59]);
        enemyDeck1.Add(CardsDatabase.cardList[59]);
        enemyDeck1.Add(CardsDatabase.cardList[59]);
    }

    private void CreateEnemyDeck2()
    {
        enemyDeck2.Add(CardsDatabase.cardList[23]);
        enemyDeck2.Add(CardsDatabase.cardList[23]);
        enemyDeck2.Add(CardsDatabase.cardList[23]);

        enemyDeck2.Add(CardsDatabase.cardList[26]);
        enemyDeck2.Add(CardsDatabase.cardList[26]);
        enemyDeck2.Add(CardsDatabase.cardList[26]);

        enemyDeck2.Add(CardsDatabase.cardList[30]);
        enemyDeck2.Add(CardsDatabase.cardList[30]);

        enemyDeck2.Add(CardsDatabase.cardList[31]);
        enemyDeck2.Add(CardsDatabase.cardList[31]);

        enemyDeck2.Add(CardsDatabase.cardList[37]);
        enemyDeck2.Add(CardsDatabase.cardList[37]);
        enemyDeck2.Add(CardsDatabase.cardList[37]);

        enemyDeck2.Add(CardsDatabase.cardList[38]);
        enemyDeck2.Add(CardsDatabase.cardList[38]);

        enemyDeck2.Add(CardsDatabase.cardList[25]);
        enemyDeck2.Add(CardsDatabase.cardList[25]);
        enemyDeck2.Add(CardsDatabase.cardList[25]);

        enemyDeck2.Add(CardsDatabase.cardList[2]);
        enemyDeck2.Add(CardsDatabase.cardList[2]);

        enemyDeck2.Add(CardsDatabase.cardList[41]);
        enemyDeck2.Add(CardsDatabase.cardList[41]);
        enemyDeck2.Add(CardsDatabase.cardList[41]);

        enemyDeck2.Add(CardsDatabase.cardList[42]);
        enemyDeck2.Add(CardsDatabase.cardList[42]);

        enemyDeck2.Add(CardsDatabase.cardList[19]);
        enemyDeck2.Add(CardsDatabase.cardList[19]);

        enemyDeck2.Add(CardsDatabase.cardList[45]);
        enemyDeck2.Add(CardsDatabase.cardList[45]);
        enemyDeck2.Add(CardsDatabase.cardList[45]);

        enemyDeck2.Add(CardsDatabase.cardList[48]);
        enemyDeck2.Add(CardsDatabase.cardList[48]);
        enemyDeck2.Add(CardsDatabase.cardList[48]);

        enemyDeck2.Add(CardsDatabase.cardList[50]);
        enemyDeck2.Add(CardsDatabase.cardList[50]);

        enemyDeck2.Add(CardsDatabase.cardList[46]);
        enemyDeck2.Add(CardsDatabase.cardList[46]);

        enemyDeck2.Add(CardsDatabase.cardList[58]);
        enemyDeck2.Add(CardsDatabase.cardList[58]);
        enemyDeck2.Add(CardsDatabase.cardList[58]);
    }

    private void CreatePlayerDeck()
    {
        playerDeck2.Add(CardsDatabase.cardList[33]);
        playerDeck2.Add(CardsDatabase.cardList[33]);
        playerDeck2.Add(CardsDatabase.cardList[33]);

        playerDeck2.Add(CardsDatabase.cardList[35]);
        playerDeck2.Add(CardsDatabase.cardList[35]);

        playerDeck2.Add(CardsDatabase.cardList[31]);
        playerDeck2.Add(CardsDatabase.cardList[31]);

        playerDeck2.Add(CardsDatabase.cardList[15]);
        playerDeck2.Add(CardsDatabase.cardList[15]);
        playerDeck2.Add(CardsDatabase.cardList[15]);

        playerDeck2.Add(CardsDatabase.cardList[19]);
        playerDeck2.Add(CardsDatabase.cardList[19]);

        playerDeck2.Add(CardsDatabase.cardList[28]);
        playerDeck2.Add(CardsDatabase.cardList[28]);
        playerDeck2.Add(CardsDatabase.cardList[28]);

        playerDeck2.Add(CardsDatabase.cardList[21]);
        playerDeck2.Add(CardsDatabase.cardList[21]);

        playerDeck2.Add(CardsDatabase.cardList[7]);
        playerDeck2.Add(CardsDatabase.cardList[7]);
        playerDeck2.Add(CardsDatabase.cardList[7]);

        playerDeck2.Add(CardsDatabase.cardList[42]);
        playerDeck2.Add(CardsDatabase.cardList[42]);

        playerDeck2.Add(CardsDatabase.cardList[40]);
        playerDeck2.Add(CardsDatabase.cardList[40]);
        playerDeck2.Add(CardsDatabase.cardList[40]);

        playerDeck2.Add(CardsDatabase.cardList[41]);
        playerDeck2.Add(CardsDatabase.cardList[41]);
        playerDeck2.Add(CardsDatabase.cardList[41]);

        playerDeck2.Add(CardsDatabase.cardList[51]);
        playerDeck2.Add(CardsDatabase.cardList[51]);

        playerDeck2.Add(CardsDatabase.cardList[53]);
        playerDeck2.Add(CardsDatabase.cardList[53]);

        playerDeck2.Add(CardsDatabase.cardList[49]);
        playerDeck2.Add(CardsDatabase.cardList[49]);
        playerDeck2.Add(CardsDatabase.cardList[49]);

        playerDeck2.Add(CardsDatabase.cardList[44]);
        playerDeck2.Add(CardsDatabase.cardList[44]);
        playerDeck2.Add(CardsDatabase.cardList[44]);

        playerDeck2.Add(CardsDatabase.cardList[48]);
        playerDeck2.Add(CardsDatabase.cardList[48]);

        playerDeck2.Add(CardsDatabase.cardList[60]);
        playerDeck2.Add(CardsDatabase.cardList[60]);
    }
}
