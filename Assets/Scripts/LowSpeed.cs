using UnityEngine;

public class LowSpeed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int inispeed;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Unit unit = other.GetComponentInParent<Unit>();
            inispeed = unit.unitInfo.speed;
            unit.unitInfo.speed -=1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Unit unit = other.GetComponentInParent<Unit>();
            unit.unitInfo.speed = inispeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
