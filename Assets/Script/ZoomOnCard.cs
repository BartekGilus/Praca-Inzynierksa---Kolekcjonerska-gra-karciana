using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomOnCard : MonoBehaviour
{
    private GameObject zoom;
    public GameObject cardZoom;

    public void Zoom()
    {
        cardZoom = GameObject.Find("CardZoom");
        zoom = Instantiate(gameObject, new Vector3(cardZoom.GetComponent<RectTransform>().position.x - 5, cardZoom.GetComponent<RectTransform>().position.y + 5, 1), Quaternion.identity);
        zoom.GetComponent<Card>().targetFrame.GetComponent<Image>().enabled = false;
        if(zoom.GetComponent<CardController>().isOnGameField)
            zoom.transform.GetChild(1).GetComponent<Image>().enabled = false;
        zoom.transform.SetParent(this.transform.parent.parent);
        zoom.GetComponent<CanvasGroup>().blocksRaycasts = false;
        zoom.transform.localScale = new Vector3(1.5f, 1.5f, 1);
    }

    public void EndZooming()
    {
        Destroy(zoom);
    }
}
