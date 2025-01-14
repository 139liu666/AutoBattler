using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public struct UnitInfo
{
    public int id;
    public string name;
    public int cost;
    public int hp;
    public float attackArea;
    public int speed;
    public int attackValue;
    public bool canCreatAnywhere;
}