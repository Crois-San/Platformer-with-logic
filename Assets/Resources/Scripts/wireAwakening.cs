using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireAwakening : MonoBehaviour
{
    /*
     * (Скрипт связан со скриптом работы провода)
     * Весь этот скрипт нужен из-за того, что каждый элемент имеет две точкм входа,
     * которые не получается выбрать внутри скрипта самих проводов,
     * поэтому эти точки выбираются и применяются здесь.
     */
    
    //Все провода в сцене
    private GameObject[] wires;
    //компоненты этих проводов
    private LogicalElement leI, leJ;
    void Awake()
    {
        /*
         * У каждого провода есть определенный тэг,
         * по которому они и находятся в этой строчке,
         * и добавляются в массив с проводами.
         */
        wires=GameObject.FindGameObjectsWithTag("Wire");
        /*
         * В этом цикле мы проходим по всем проводам, и смотрим, если они приклеплены к одному элементу,
         * то скрипт назначает им точки входа.
         */
        for (var i = 0; i < wires.Length-1; i++)
        {
            for (var j = i + 1; j < wires.Length; j++)
            {
                //Берем сам элемент, к которому приклеплен провод
                leI = wires[i].GetComponent<LogicalWire>().GetLe2;
                leJ = wires[j].GetComponent<LogicalWire>().GetLe2;
                if (leI == leJ)
                {
                    wires[j].GetComponent<LogicalWire>().SetOutp = leJ.gameObject.transform.Find("inPoint 2");
                    wires[i].GetComponent<LogicalWire>().SetOutp = leI.gameObject.transform.Find("inPoint 1");
                    
                }
                
                if(leJ is LogicalNot || leJ is LogicalPiston)
                {
                    wires[j].GetComponent<LogicalWire>().SetOutp = leJ.gameObject.transform.Find("inPoint 1");
                }
            }
            if (leI is LogicalNot || leI is LogicalPiston)
            {
                wires[i].GetComponent<LogicalWire>().SetOutp = leI.gameObject.transform.Find("inPoint 1");
            }
        }
    }
    
}
