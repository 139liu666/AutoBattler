using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChoiceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject gamePanel;   // ����ѡ�����
    public GameObject settingsPanel; // �������
    public Dropdown resolutionDropdown; // �ֱ���ѡ��
    public Slider volumeSlider;
    private Resolution[] resolutions;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    void Start()
    {
        gamePanel.SetActive(false);    // Ĭ�����س���ѡ�����
        settingsPanel.SetActive(false); // Ĭ�������������

        // �����ֱ��ʸ���
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.width + "x" + res.height));
        }
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);

        // ������������
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        button1.onClick.AddListener(() => LoadScene("GameScene1"));
        button2.onClick.AddListener(() => LoadScene("GameScene2"));
        button3.onClick.AddListener(() => LoadScene("GameScene3"));
        button4.onClick.AddListener(() => LoadScene("GameScene4"));
        button5.onClick.AddListener(() => LoadScene("GameScene5"));
    }
    public void OpenGamePanel()
    {
        gamePanel.SetActive(true);
    }
    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); // ����ָ���ĳ���
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void CloseChoice()
    {
        gamePanel.SetActive(false);
    }
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void ChangeResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // ���س���
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
