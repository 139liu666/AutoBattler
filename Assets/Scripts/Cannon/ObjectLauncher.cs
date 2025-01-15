using UnityEngine;
using UnityEngine.UI;
public class ObjectLauncher : MonoBehaviour
{
    // Référence de l'objet à lancer (Prefab)
    public GameObject objectPrefab;
    public AudioSource audioSource;
    public GameObject cannon;

    // Point d'apparition de l'objet
    public Transform spawnPoint;

    // Force appliquée pour lancer l'objet
    public float launchForce = 500f;
    public float recoveryTime = 10f;    // Temps de récupération en secondes

    private float lastLaunchTime = -10f; // Le temps du dernier lancement
    public Image recoveryBar;    
    void Update() {
        UpdateRecoveryBar();
    }
    // Fonction pour faire apparaître et lancer l'objet
    public void LaunchObject()
    {   
        Debug.Log(Time.time - lastLaunchTime);
        if (Time.time - lastLaunchTime >= recoveryTime) 
        {
            lastLaunchTime = Time.time;
            audioSource.Play();
            Debug.Log(lastLaunchTime);
            if (objectPrefab == null || spawnPoint == null)
            {
                Debug.LogError("L'objet ou le point d'apparition n'est pas défini.");
                return;
            }

            // Instancier l'objet au point d'apparition avec sa rotation
            GameObject launchedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);

            // Vérifier si l'objet a un Rigidbody
            Rigidbody rb = launchedObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("L'objet lancé n'a pas de composant Rigidbody.");
                return;
            }

            // Appliquer une force pour lancer l'objet en ligne droite
            Debug.Log($"Force appliquée : {spawnPoint.forward * launchForce}");
            rb.AddForce(spawnPoint.forward * launchForce, ForceMode.Impulse);
        }
        else 
        {
            Debug.Log("Cannon en délai de récupération.");
        }
    }
    public void UpdateRecoveryBar()
    {
        // Calcule le pourcentage de récupération
        float recoveryProgress = (Time.time - lastLaunchTime) / recoveryTime;

        // Mettez à jour la barre de récupération en fonction du temps écoulé
        if (recoveryBar != null)
        {
            recoveryBar.fillAmount = Mathf.Clamp01(recoveryProgress);  // "fillAmount" est compris entre 0 et 1
        }
    }
}