using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject playerCard;
    public Vector3 currentScale;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.gameObject.GetComponent<CardController>().isOnGameField)
        {
            if (playerCard == null)
            {
                if(eventData.pointerPress.gameObject.tag == "PlayerCard" && !eventData.pointerPress.gameObject.GetComponent<CardController>().summoningSickness && !eventData.pointerPress.gameObject.GetComponent<CardController>().isDefender)
                {
                    playerCard = eventData.pointerPress.gameObject;
                    currentScale = playerCard.transform.localScale;
                    playerCard.transform.localScale = new Vector3(1, 1, 1);
                    playerCard.GetComponent<CardController>().isAttacking = true;
                }
            }
            else
            {
                StopAttack();
            }
                
        }
    }

    public void StopAttack()
    {
        playerCard.transform.localScale = currentScale;
        playerCard.GetComponent<CardController>().isAttacking = false;
        playerCard = null;
    }
}
