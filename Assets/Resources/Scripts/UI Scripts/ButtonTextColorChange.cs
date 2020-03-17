using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextColorChange  : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Color red,brightRed;
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        brightRed =  new Color(0.9f, 0.035f, 0.035f);
        gameObject.GetComponentInChildren<Text>().color = brightRed;

    }
     
    public void OnDeselect(BaseEventData eventData)
    {
        red = new Color(0.6f, 0.0235f, 0.0235f);
        gameObject.GetComponentInChildren<Text>().color = red;
     
    }
}
