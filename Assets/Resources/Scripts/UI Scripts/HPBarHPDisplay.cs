using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarHPDisplay : MonoBehaviour
{
    [field: SerializeField]
    public Image img { get; set; }
    [field: SerializeField]
    public bool hasHP { get; set; }
    
    [field: SerializeField]
    public Sprite HPBarFull { get; set; }
    [field: SerializeField]
    public Sprite HPBarEmpty { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (hasHP)
        {
            img.sprite = HPBarFull;
        }
        else
        {
            img.sprite = HPBarEmpty;
        }
    }
}
