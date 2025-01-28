using DG.Tweening;
using NUnit.Framework;  
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnitInfo unitInfo;
    public bool isRed;
    public int currentHP;
    public bool isDead;
    public bool hasTarget;
    
    //组件
    public Unit targetUnit;
    public Animator animator;
    public NavMeshAgent agent;
    public Unit defaultTarget;//国王
    public List<Unit> targetsList = new List<Unit>();
    public List<Unit> attackList = new List<Unit>();

    protected Collider[] colliders;
    protected HP hpslider;

    public AudioClip attackClip;
    public AudioClip dieClip;
    protected virtual void Start()
    {
       
        animator  = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GetComponentInChildren<SphereCollider>().radius = unitInfo.attackArea;
        colliders=GetComponentsInChildren<Collider>();
        currentHP = unitInfo.hp;
        if (unitInfo.hp > 0)
        {
            hpslider = transform.Find("Canva_HP").GetComponent<HP>();
            hpslider.SetHPColorSlider(isRed);
        }
    }
    protected virtual void UnitMove()
    {
        if (hasTarget)
        {
            //是否目标销毁
            if (targetUnit != null&&!targetUnit.isDead)
            {
                //目标移动
                agent.SetDestination(targetUnit.transform.position);
                ReachTargetPos(transform.position, targetUnit.transform.position);
            }
            else
            {
                //重置目标
                RestTarget(targetUnit);
            }
        }
        //无目标
        else
        {
            //获取当前目标
            GameControler.Instance.UnitGetTargetPos(this, isRed);
            if (defaultTarget != null)
            {
                //攻击国王
                agent.SetDestination(defaultTarget.transform.position);
                transform.LookAt(defaultTarget.transform);

                ReachTargetPos(transform.position, defaultTarget.transform.position);
            }
        }
        
    }
    //到达攻击范围并移动
    public void ReachTargetPos(Vector3 currentPos,Vector3 target)
    {
        if (Vector3.Distance(currentPos, target) >= unitInfo.attackArea)
        {
            UnitBehaviour();
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("IsAttack",true);
            animator.SetBool("IsMoving", false);
        }
    }
    private void RestTarget(Unit unit)
    {
        targetsList.Remove(unit);
        ClearDeadUnit();
        if (targetsList.Count > 0)
        {
            SetTarget();
        }
        else
        {
            hasTarget = false;
            targetUnit = null;
            GameControler.Instance.UnitGetTargetPos(this, isRed);
        }
    }
    protected virtual void UnitBehaviour()
    {
        if (agent.enabled)
        {
            agent.isStopped = false;
        }
        //动画
        animator.SetBool("IsMoving",true);
        animator.SetBool("IsAttack",false);
    }
    //受到伤害
    public void TakeDamage(int damageValue,Unit attacker)
    {
        currentHP-= damageValue;
        Mathf.Clamp(currentHP,0,unitInfo.hp);
        hpslider.SetHPValue((float)currentHP / unitInfo.hp);
        if (currentHP <= 0)
        {
            Die(attacker);
        }
    }
    protected virtual void Die(Unit attacker)
    {
        //GameManager.Instance.PlaySound(dieClip);
        isDead = true;
        animator.SetTrigger("Die");
        SetCollider(false);
        attacker.RestTarget(this);
        RemoveAttacker();
        if (agent.enabled)
        {
            agent.isStopped = true;
        }
        Invoke("DestroyObjet",2);
    }
    public void DestroyObjet()
    {
        Destroy(gameObject);
    }
    public virtual void AttackAnimation()
    {
        if (hasTarget)
        {
            transform.LookAt(targetUnit.transform);
        }
        else
        {
            transform.LookAt(defaultTarget.transform);
        }
        if (targetUnit != null)
        {
            //扣血
            targetUnit.TakeDamage(unitInfo.attackValue, this);
        }
        //GameManager.Instance.PlaySound(attackClip);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Unit unit =  other.GetComponentInParent<Unit>();
            if (isRed!=unit.isRed)
            {
                targetsList.Add(unit);
                unit.AddAttackerToList(this);
                //清除死亡单位
                ClearDeadUnit();
                SetTarget();         
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Unit unit = other.GetComponentInParent<Unit>();
            if (isRed != unit.isRed&&unit!=null&&targetUnit==unit)
            {
                RestTarget(unit);
            }
        }
    }
    public void AddAttackerToList(Unit unit)
    {
        attackList.Add(unit);
    }
    private void ClearDeadUnit()
    {
       //临时列表
        List<int> clearList = new List<int>();
        for(int i = 0; i < targetsList.Count; i++)
        {
            if(targetsList[i] == null)
            {
                clearList.Add(i);
            }
        }
        //遍历清除列表
        for(int i = 0;i < clearList.Count; i++)
        {
            targetsList.RemoveAt(clearList[i]);
        }
    }
    private void SetTarget()
    {
        float closestDistance = Mathf.Infinity;
        Unit u = null;
        for(int i = 0; i < targetsList.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, targetsList[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                u = targetsList[i];
            }
        }
        targetUnit = u;
        hasTarget = true;
    }
    protected void SetCollider(bool state)
    {
        for(int i = 0;i<colliders.Length;i++)
        {
            colliders[i].enabled = state;
        }
    }
    private void RemoveAttacker()
    {
        for (int i = 0; i < attackList.Count; i++)
        {
            attackList[i].targetsList.Remove(this);
        }
    }
    // Update is called once per frame
   
}
[Serializable]
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