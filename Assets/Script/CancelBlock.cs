using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelBlock : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Card>().targetFrame.transform.position = eventData.pointerPress.gameObject.GetComponent<Card>().targetFrame.GetComponent<SelectTarget>().startPosition;
    }
}
