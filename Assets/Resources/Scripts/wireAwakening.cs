using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireAwakening : MonoBehaviour
{
    private GameObject[] wires;
    private LogicalElement leI, leJ;
    void Awake()
    {
        wires=GameObject.FindGameObjectsWithTag("Wire");
        for (var i = 0; i < wires.Length-1; i++)
        {
            for (var j = i + 1; j < wires.Length; j++)
            {
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
