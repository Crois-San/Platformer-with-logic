using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuTransition : MonoBehaviour
{
    public float SpeedMultiplier { get; set; } = 2f;
    public Vector3 MenuPosition { get; set; }
    public bool canTransition { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (canTransition)
        {
            transform.position =
                        new Vector3(Mathf.Lerp(transform.position.x, MenuPosition.x, Time.deltaTime * SpeedMultiplier),
                            Mathf.Lerp(transform.position.y, MenuPosition.y, Time.deltaTime * SpeedMultiplier),
                            transform.position.z);
            
        }
    }
    
    public void MenuTransition(RectTransform targetMenu)
    {
        Debug.Log("HEY!!");
        if(!targetMenu) return;
        MenuPosition = targetMenu.position;
        //initialMenu.gameObject.SetActive(false);
        targetMenu.gameObject.SetActive(true);
        canTransition = true;


    }
}
