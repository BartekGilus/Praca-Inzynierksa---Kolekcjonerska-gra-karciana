using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableField : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool ArtifactField;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null && draggable.placeholderParent == this.transform)
        {
            draggable.placeholderParent = draggable.currentParent;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if(draggable != null && draggable.gameObject.GetComponent<CardController>().canBePlayed)
        {
            if (ArtifactField)
            {
                if (draggable.gameObject.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_ARTIFACT && this.transform.childCount == 1)
                {
                    draggable.currentParent = this.transform;
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerMana -= draggable.GetComponent<Card>().cardCost;
                }
            }
            else
            {
                if (draggable.gameObject.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER && this.transform.childCount < 7)
                {
                    draggable.currentParent = this.transform;
                    draggable.gameObject.GetComponent<CardController>().summoningSickness = true;
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerMana -= draggable.GetComponent<Card>().cardCost;
                }
                else if(draggable.gameObject.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_SPELL)
                {
                    draggable.currentParent = GameObject.Find("PlayedSpellDisplay").transform;
                    draggable.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerMana -= draggable.GetComponent<Card>().cardCost;
                }
            }
                
        }    
    }
}

