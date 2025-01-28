using UnityEngine;
using System.Collections;
public class Canon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject projectile;
    public float launchVelocity = 700f;
    void Start()
    {
        StartCoroutine(Project());
    }
    IEnumerator Project()
    {
        yield return new WaitForSeconds(5f); // �ȵȴ� 5 ���ٿ�ʼѭ��

        while (true) // ����ѭ�����൱�� InvokeRepeating
        {
            CreateProjectile();
            yield return new WaitForSeconds(10f); // ÿ 20 ��ִ��һ��
        }
    }
    public void CreateProjectile()
    {
        GameObject ball = Instantiate(projectile,
            transform.position, transform.rotation);

        ball.GetComponent<Rigidbody>().AddRelativeForce(new
        Vector3(0, launchVelocity, 0 ));
        Destroy(ball, 5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
