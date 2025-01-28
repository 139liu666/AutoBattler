using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Building : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float attackCD = 1.4f;
    private float attackTimer;
    public Transform CharacTrans;
    protected override void Start()
    {
        unitInfo = GameControler.Instance.unitinfo[5];
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        Attack();
    }
    private void Attack()
    {
        if (Time.time - attackTimer >= attackCD)
        {
            attackTimer = Time.time;
            if (hasTarget&&targetUnit!=null)
            {
                animator.SetBool("IsAttack", true);
                CharacTrans.LookAt(new Vector3(targetUnit.transform.position.x,CharacTrans.position.y,targetUnit.transform.position.z));
                targetUnit.TakeDamage(unitInfo.attackValue,this);
            }
            else
            {
                animator.SetBool("IsAttack",false);
            }
        }
    }
    protected override void Die(Unit attacker)
    {
        base.Die(attacker);
        if (isDead)
        {
            UIManager.Instance.GameOver(isRed);
        }
        else
        {
            
        }
    }
    public override void AttackAnimation()
    {

    }
}
