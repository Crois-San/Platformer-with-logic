using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableMenu : MonoBehaviour
{
    public Button btn;

    void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
        Debug.Log("Disabled!");
        btn.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }
}
