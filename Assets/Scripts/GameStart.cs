using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("LoadChoiceScene",1.5f);
    }

    // Update is called once per frame
    private void LoadChoiceScene()
    {
        SceneManager.LoadScene(1);
    }
}
