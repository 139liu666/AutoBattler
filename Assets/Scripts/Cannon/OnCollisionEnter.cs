using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public VarTest vie;
    private void OnCollisionEnter(Collision collision)
    {
        // Affiche le nom de l'objet avec lequel une collision s'est produite
        Debug.Log($"Collision détectée avec : {collision.gameObject.name}");
        if (collision.gameObject.name == "Canonball(Clone)") {
            vie.takeDamage(50);
            Destroy(collision.gameObject);

        }
    }

}
