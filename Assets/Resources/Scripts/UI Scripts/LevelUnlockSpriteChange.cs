using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockSpriteChange : MonoBehaviour
{
    [field: SerializeField]
    public bool isUnlocked { get; set; }

    [field: SerializeField]
    public Sprite unlocked { get; set; }
    [field: SerializeField]
    public Sprite locked { get; set; }

    [field: SerializeField]
    public Image img { get; set; }

    [field: SerializeField]
    public Text levelNumber { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (isUnlocked)
        {
            img.sprite = unlocked;
            levelNumber.color = new Color(0.9f,0.035f,0.035f); //E6 09 09
        }
        else
        {
            img.sprite = locked;
            levelNumber.color = new Color(0.6f,0.0235f,0.0235f); //99 06 06
        }
    }
}
