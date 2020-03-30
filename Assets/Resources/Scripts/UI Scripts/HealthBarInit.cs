using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarInit : MonoBehaviour
{
    private int HP;
    private int currentHP;
    [field: SerializeField]
    public GameObject HPBarStart { get; set; }
    [field: SerializeField]
    public GameObject HPBarEnd { get; set; }
    [field: SerializeField]
    public GameObject HPBarMiddle { get; set; }
    public HPBarHPDisplay[] HPBars { get; set; }

    private Character ch;
    // Start is called before the first frame update
    void Start()
    {
        ch = GameObject.Find("Character").GetComponent<Character>();
        HP = ch.getHealthPoints;
        currentHP = HP;
        HPBars = new HPBarHPDisplay[HP];
        for (int i = 0; i < HP; i++)
        {
            switch (i)
            {
                case 0:
                    HPBars[i] = Instantiate(HPBarStart, transform, false).GetComponent<HPBarHPDisplay>();
                    break;
                case int n when (n == (HP - 1)):
                    HPBars[i] = Instantiate(HPBarEnd, transform, false).GetComponent<HPBarHPDisplay>();
                    break;
                default:
                    HPBars[i] = Instantiate(HPBarMiddle, transform, false).GetComponent<HPBarHPDisplay>();
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = ch.getHealthPoints;
        for (int i = 0; i < HP; i++)
        {
            if (i < currentHP)
            {
                HPBars[i].hasHP = true;
            }
            else
            {
                HPBars[i].hasHP = false;
            }
        }

    }
}
