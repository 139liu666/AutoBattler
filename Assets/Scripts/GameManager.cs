using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance;
    public AudioSource audiosource;
  
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayMusic(AudioClip audioClip)
    {
        audiosource.clip = audioClip;
        audiosource.Play();
    }
    public void PlaySound(AudioClip audioClip)
    {
        audiosource.PlayOneShot(audioClip);
    }
// Update is called once per frame
void Update()
    {
        
    }
}
