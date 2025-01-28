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
        yield return new WaitForSeconds(5f); // �ȵȴ� 5 ���ٿ�ʼѭ��

        while (true) // ����ѭ�����൱�� InvokeRepeating
        {
            CreateUnits();
            yield return new WaitForSeconds(20f); // ÿ 20 ��ִ��һ��
        }
    }
    private void CreateUnits()
    {
        GameControler.Instance.CreateUnit(Random.Range(1,6),transform.position,true);
    }
}
