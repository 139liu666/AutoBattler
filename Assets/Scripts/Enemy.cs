using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnUnitsCoroutine());
    }

    IEnumerator SpawnUnitsCoroutine()
    {
        yield return new WaitForSeconds(5f); // 先等待 5 秒再开始循环

        while (true) // 无限循环，相当于 InvokeRepeating
        {
            CreateUnits();
            yield return new WaitForSeconds(20f); // 每 20 秒执行一次
        }
    }
    private void CreateUnits()
    {
        GameControler.Instance.CreateUnit(Random.Range(1,6),transform.position,true);
    }
}
