using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    //свечение логического элемента
    private LogicalElement element;
    private GameObject[] light;
    private GameObject child;
    void Start()
    {
        //находим свечение логического элемента
        //light = GameObject.FindGameObjectsWithTag("Light");
        int n = 0, j=0;
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            if (child.CompareTag("Light"))
                n++;
        }
        light = new GameObject[n];
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            if (child.CompareTag("Light"))
            {
                light[j] = child;
                j++;
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
