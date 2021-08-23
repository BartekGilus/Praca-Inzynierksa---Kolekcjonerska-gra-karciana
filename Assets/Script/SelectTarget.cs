using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTarget : MonoBehaviour
{
    public GameObject card;
    public GameObject attackTarget;

    private bool isDragged = false;
    public Vector2 startPosition;

    private bool beenDragged;

    void Update()
    {
        if (isDragged)
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameObject.Find("TurnSystem").GetComponent<TurnSystem>().turnState == TurnSystem.TurnStates.TURNSTATE_BLOCK_DECLARATION)
        {
            if (collision.gameObject.tag == "EnemyCard")
            {
                if (card.GetComponent<CardController>().target == null)
                {
                    if (!collision.gameObject.GetComponent<CardController>().isBlocked)
                    {
                        if (collision.gameObject.GetComponent<CardController>().isFlying)
                        {
                            if (card.GetComponent<CardController>().isArcher || card.GetComponent<CardController>().isFlying)
                            {
                                card.GetComponent<CardController>().target = collision.gameObject;
                                collision.gameObject.GetComponent<CardController>().isBlocked = true;
                                attackTarget = card.gameObject;
                                this.transform.position = collision.gameObject.transform.position;
                            }
                        }
                        else
                        {
                            card.GetComponent<CardController>().target = collision.gameObject;
                            attackTarget = card.gameObject;
                            this.transform.position = collision.gameObject.transform.position;
                        }
                    }
                }
            }
        }    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyCard")
        {
            card.GetComponent<CardController>().target = null;
            collision.gameObject.GetComponent<CardController>().isBlocked = false;
            attackTarget = null;
            this.transform.position = startPosition;
        }
    }

    public void StartDragging()
    {
        if(!beenDragged)
            startPosition = this.transform.position;
        isDragged = true;
    }

    public void EndDragging()
    {
        isDragged = false;
        if (attackTarget == null)
            this.transform.position = startPosition;
        beenDragged = true;
    }
}
