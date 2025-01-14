using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class GameControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameControler Instance;

    public float moneyvalue;
    public float Lefttime;
    public List<UnitInfo> unitinfo;
    private void Awake()
    {
        Instance = this;
        moneyvalue = 10;
        Lefttime = 180;
        unitinfo = new List<UnitInfo>()
        {
            new UnitInfo(){id=1,name="������",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=2,name="������ʹ",cost=4,hp=10,attackArea=3,speed=1,attackValue=1},
            new UnitInfo(){id=3,name="����ʹ",cost=5,hp=20,attackArea=5,speed=1,attackValue=5},
            new UnitInfo(){id=4,name="���Ҿ���",cost=4,hp=30,attackArea=4,speed=1,attackValue=6},
            new UnitInfo(){id=5,name="������",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=6,name="������",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=7,name="������",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=8,name="������",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=9,name="������",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=10,name="������",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=11,name="���ù�",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=12,name="�����⻷",cost=3,hp=10,attackArea=4,speed=1,attackValue=2}

        };

    }

    // Update is called once per frame
    void Update()
    {
        if (moneyvalue < 10)
        {
            moneyvalue += Time.deltaTime;
            UIManager.Instance.SetMoney();
        }
        DecreaseTime();
    }
    private void DecreaseTime()
    {
        Lefttime-=Time.deltaTime;
        int min = (int)Lefttime / 60;
        int sec= (int)Lefttime % 60;
        UIManager.Instance.SetTimeValue(min,sec);
    }
    public bool CanUseCard(int id)
    {
        return unitinfo[id - 1].cost <= moneyvalue;
    }
    
}
