using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Unit unit;
    void Start()
    {
        unit = GetComponentInParent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AttackAnimationEvent()
    {
        unit.AttackAnimation();
    }
}
