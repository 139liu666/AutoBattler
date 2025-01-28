using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int iniHP;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Unit unit = other.GetComponentInParent<Unit>();
            iniHP = unit.currentHP;
            unit.currentHP -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
