using UnityEngine;

public class VarTest : MonoBehaviour
{
    public int health = 100;
    public void takeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log(health);
    }   
    void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
