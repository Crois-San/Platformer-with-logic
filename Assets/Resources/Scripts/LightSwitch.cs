using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    //свечение логического элемента
    private LogicalElement element;
    //private GameObject[] light;
    private GameObject child;
    private List<GameObject> light;
    void Start()
    {
        //находим свечение логического элемента
        light = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            if (child.CompareTag("Light"))
            {
                light.Add(child);
            }
        }
        element = GetComponent<LogicalElement>();
    }

    // Update is called once per frame
    void Update()
    {
        //включаем свечение по сигналу
        foreach (var obj in light)
        {
            obj.SetActive(element.state);
        }
        
    }
}
