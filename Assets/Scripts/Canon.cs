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
        yield return new WaitForSeconds(5f); // 先等待 5 秒再开始循环

        while (true) // 无限循环，相当于 InvokeRepeating
        {
            CreateProjectile();
            yield return new WaitForSeconds(10f); // 每 20 秒执行一次
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
