using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Graveyard : MonoBehaviour, IPointerClickHandler
{
    public List<CardTemplate> playerGraveyard = new List<CardTemplate>();
    public List<CardTemplate> enemyGraveyard = new List<CardTemplate>();
    public GameObject graveyardDisplay;
    public GameObject card;

    public int counter;

    public bool playerGraveyardIsShowed;
    public bool enemyGraveyardIsShowed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerPress.gameObject.tag == "PlayerGraveyard" && !enemyGraveyardIsShowed)
        {
            if (!playerGraveyardIsShowed)
            {
                graveyardDisplay.GetComponent<DisplayGraveyard>().graveyard = playerGraveyard;
                graveyardDisplay.SetActive(true);
                playerGraveyardIsShowed = true;
            }
            else
            {
                graveyardDisplay.GetComponent<DisplayGraveyard>().graveyard = null;
                graveyardDisplay.SetActive(false);
                playerGraveyardIsShowed = false;
            }
        }

        if(eventData.pointerPress.gameObject.tag == "EnemyGraveyard" && !playerGraveyardIsShowed)
        {
            if (!enemyGraveyardIsShowed)
            {
                graveyardDisplay.GetComponent<DisplayGraveyard>().graveyard = enemyGraveyard;
                graveyardDisplay.SetActive(true);
                enemyGraveyardIsShowed = true;
            }
            else
            {
                graveyardDisplay.GetComponent<DisplayGraveyard>().graveyard = null;
                graveyardDisplay.SetActive(false);
                enemyGraveyardIsShowed = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "PlayerGraveyard")
        {
            if (playerGraveyard.Count == 0)
            {
                counter = 0;
                if (this.transform.childCount > 0)
                    Destroy(this.transform.GetChild(0).gameObject);
            }
            else
            {
                if (playerGraveyard.Count > counter)
                {
                    int index = playerGraveyard.Count;
                    card.GetComponent<Card>().thisCardID = playerGraveyard[index - 1].cardID - 1;
                    GameObject displayOnTopCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                    displayOnTopCard.transform.SetParent(this.transform);
                    displayOnTopCard.transform.localScale = new Vector3(1, 1, 1);
                    displayOnTopCard.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    counter++;
                }
            }

            if (this.transform.childCount > 1)
            {
                Destroy(this.transform.GetChild(0).gameObject);
            }
        }

        if(this.gameObject.tag == "EnemyGraveyard")
        {
            if (enemyGraveyard.Count == 0)
            {
                counter = 0;
                if (this.transform.childCount > 0)
                    Destroy(this.transform.GetChild(0).gameObject);
            }
            else
            {
                if (enemyGraveyard.Count > counter)
                {
                    int index = enemyGraveyard.Count;
                    card.GetComponent<Card>().thisCardID = enemyGraveyard[index - 1].cardID - 1;
                    GameObject displayOnTopCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                    displayOnTopCard.transform.SetParent(this.transform);
                    displayOnTopCard.transform.localScale = new Vector3(1, 1, 1);
                    displayOnTopCard.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    counter++;
                }
            }

            if (this.transform.childCount > 1)
            {
                Destroy(this.transform.GetChild(0).gameObject);
            }
        }
       
    }
}
