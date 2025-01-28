using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChoiceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject gamePanel;   // 场景选择面板
    public GameObject settingsPanel; // 设置面板
    public Dropdown resolutionDropdown; // 分辨率选项
    public Slider volumeSlider;
    private Resolution[] resolutions;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    void Start()
    {
        gamePanel.SetActive(false);    // 默认隐藏场景选择面板
        settingsPanel.SetActive(false); // 默认隐藏设置面板

        // 监听分辨率更改
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.width + "x" + res.height));
        }
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);

        // 监听音量更改
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
        SceneManager.LoadScene(sceneIndex); // 加载指定的场景
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
        SceneManager.LoadScene(sceneName); // 加载场景
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
