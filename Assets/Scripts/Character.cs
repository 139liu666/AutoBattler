using UnityEngine;

public class Character : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        UnitMove();
    }
}
