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
    public SceneLoader sl { get; set; }
    public GameObject[] icons { get; set; }


    public int numberOfLevels { get; set; } = 50;
    // Start is called before the first frame update
    void Start()
    { 
        sl = GetComponent<SceneLoader>();
        sl.GetLevelsList();
        numberOfLevels = sl.GetSceneList.Count;
        icons = new GameObject[numberOfLevels];
//        var panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
//        var iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
//        var maxInARow = Mathf.FloorToInt(panelDimensions.width / iconDimensions.width);
//        var maxInACol = Mathf.FloorToInt(panelDimensions.height / iconDimensions.height);
        LoadIcons(numberOfLevels, levelHolder);
    }
    void LoadIcons(int numberOfIcons, GameObject parentObject)
    {
        for (int i = 0; i < numberOfIcons; i++)
        {
            icons[i] = Instantiate(levelIcon, thisCanvas.transform, false);
            icons[i].transform.SetParent(parentObject.transform, false);
            icons[i].name = "level " + i;
            icons[i].GetComponent<LevelUnlockSpriteChange>().isUnlocked = sl.LevelsState[i];
            icons[i].GetComponentInChildren<Text>().text= (i+1).ToString();
            int localIndex = i;
            icons[i].GetComponent<Button>().onClick.AddListener(delegate { LoadLevelOnclick(localIndex); });
        }
    }

    void LoadLevelOnclick(int levelIndex)
    {
        if (icons[levelIndex].GetComponent<LevelUnlockSpriteChange>().isUnlocked)
        {
            sl.LoadLevel(levelIndex+1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
