using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [field: SerializeField]
    public GameObject levelHolder { get; set; }

    [field: SerializeField]
    public GameObject levelIcon { get; set; }
    [field: SerializeField]
    public GameObject thisCanvas { get; set; }

    
    public int numberOfLevels { get; set; } = 50;
    // Start is called before the first frame update
    void Start()
    {
        var panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        var iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        var maxInARow = Mathf.FloorToInt(panelDimensions.width / iconDimensions.width);
        var maxInACol = Mathf.FloorToInt(panelDimensions.height / iconDimensions.height);
        LoadIcons(numberOfLevels, levelHolder);
    }

    void LoadIcons(int numberOfIcons, GameObject parentObject)
    {
        for (int i = 0; i < numberOfIcons; i++)
        {
            var icon = Instantiate(levelIcon, thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform, false);
            icon.name = "level" + i;
            icon.GetComponentInChildren<Text>().text= (i+1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
