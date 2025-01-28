using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
public class GameControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameControler Instance;

    public float moneyvalue;
    public float Lefttime;
    public List<UnitInfo> unitinfo;
    public GameObject[] units;//角色预制体
    public Building[] blueBuildings;
    public Building[] redBuildings;
    public AudioClip[] gameBGMusic;
    private void Awake()
    {
        Instance = this;
        moneyvalue = 10;
        Lefttime = 180;
        unitinfo = new List<UnitInfo>()
        {
            new UnitInfo(){id=1,name="弓箭手",cost=3,hp=10,attackArea=4,speed=1,attackValue=2},
            new UnitInfo(){id=2,name="治愈天使",cost=4,hp=10,attackArea=3,speed=1,attackValue=1},
            new UnitInfo(){id=3,name="三头狼",cost=5,hp=20,attackArea=2,speed=1,attackValue=4},
            new UnitInfo(){id=4,name="堕天使",cost=4,hp=30,attackArea=3,speed=1,attackValue=3},
            new UnitInfo(){id=5,name="熔岩巨兽",cost=3,hp=10,attackArea=2,speed=1,attackValue=4},
            new UnitInfo(){id=6,name="king",cost=0,hp=200,attackArea=5,speed=0,attackValue=6},
        };
        GameManager.Instance.PlayMusic(gameBGMusic[Random.Range(0, 3)]);
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
    public void DecreaseMoney(int id)
    {
        int value=unitinfo[id-1].cost;
        moneyvalue -= value;
    }
    public void CreateUnit(int id,Vector3 pos,bool isRed=false)
    {
        GameObject go =  Instantiate(units[id - 1]);
        go.transform.position = pos;
        switch (id)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                Unit unit = go.GetComponent<Unit>();
                unit.isRed = isRed;
                unit.unitInfo = unitinfo[id - 1];
                break;
            default:
                break;
        }
    }
    public void UnitGetTargetPos(Unit unit,bool isRed)
    {
       Building[] buildings =  isRed?blueBuildings:redBuildings; 
       if (!buildings[0])
        {
            return;
        }
        else
        {
            unit.defaultTarget = buildings[0];
        }
    }
    
}
