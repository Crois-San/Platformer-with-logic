using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettingPanel : MonoBehaviour
{
    private GameObject[] panels;
    // Start is called before the first frame update
    void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("Settings panels");
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
        panels[0].gameObject.SetActive(true);
    }

    public void ShowPanel(RectTransform panel)
    {
        foreach (var settPanels in panels)
        {
            if (settPanels.Equals(panel.gameObject))
            {
                panel.gameObject.SetActive(true);
            }
            else
            {
                settPanels.SetActive(false);
            }
            
        }
    }
}
