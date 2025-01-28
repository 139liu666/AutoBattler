using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public static UIManager Instance;
    public Text MoneyText;
    public Slider Money;
    public Text lefttimeText;
    private  List<int> cardlist= new List<int>();
    public GameObject cardobject;
    public Transform nextcard;
    public Sprite[] cardsprites;
    private int maxCardNum=4;//��������������
    private int currentCardNum;//��ǰ��忨����
    public Transform[] boardCard;//��������ĸ�λ��
    public Transform boardTrans;//��������transform
    public GameObject losePanelGo;
    public GameObject winPanelGo;
    public GameObject endPanelGo;
    public GameObject startPanelGo;
    public AudioClip winClip;
    public AudioClip loseClip;
    public GameObject gamePanel;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    private void Awake()
    {
        Instance = this;
        CreatNewCard();
        startPanelGo.SetActive(true);


    }
    void Start()
    {
        StartCoroutine(HidePanelAfterSeconds(3f));
    }
    IEnumerator HidePanelAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        startPanelGo.SetActive(false); // 5 ������� UI
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMoney()
    {
        MoneyText.text = ((int)GameControler.Instance.moneyvalue).ToString();
        Money.value = GameControler.Instance.moneyvalue / 10;
    }
    public void SetTimeValue(int min,int sec)
    {
        lefttimeText.text=min.ToString()+":"+sec.ToString();
    }
    private void CreatNewCard()
    {
        if (currentCardNum > maxCardNum)
        {
            return;
        }
        GameObject go = Instantiate(cardobject,nextcard);
        go.transform.localPosition=Vector3.zero;
        int randomNum =  Random.Range(1,6);
        cardlist.Add(randomNum);
        Image image =go.transform.GetChild(0).GetComponent<Image>();
        image.sprite = cardsprites[randomNum - 1];
        go.GetComponent<Card>().id = randomNum;
        if (currentCardNum < maxCardNum)
        {
            MoveCardToBoard(currentCardNum);
           
        }
    }
    private void MoveCardToBoard(int posID)
    {
        Transform t = nextcard.GetChild(0);
        //t.SetParent(boardCard[posID]);
        // t.localPosition = Vector3.zero;
        t.GetComponent<Card>().posID = posID ;
        t.SetParent(boardTrans);
        t.DOLocalMove(boardCard[posID].localPosition, 0.4f).OnComplete(() => { CompleteMove(t); });
    }
    //һ���Ʒ����ִ����һ��
    private void CompleteMove(Transform t)
    {
        currentCardNum++;
        CreatNewCard();
        t.GetComponent<Card>().SetInitPos();
    }
    public void Usecard(int posID)
    {
        currentCardNum--;
        MoveCardToBoard (posID);
    }
    public void GameOver(bool win)
    {
        Time.timeScale = 0;
        endPanelGo.SetActive(true);
        if (win)
        {
            GameManager.Instance.PlayMusic(winClip);
            winPanelGo.SetActive(true);
        }
        else
        {
            GameManager.Instance.PlayMusic(loseClip);
            losePanelGo.SetActive(true);
        }
    }
    public void OpenGamePanel()
    {
        gamePanel.SetActive(true);
    }
    public void CloseGamePanel()
    {
        gamePanel.SetActive(false);
    }
    public void LoadChoiceScene1()
    {
        SceneManager.LoadScene("GameScene1");
    }
    public void LoadChoiceScene2()
    {
        SceneManager.LoadScene("GameScene2");
    }
    public void LoadChoiceScene3()
    {
        SceneManager.LoadScene("GameScene3");
    }
    public void LoadChoiceScene4()
    {
        SceneManager.LoadScene("GameScene4");
    }
    public void LoadChoiceScene5()
    {
        SceneManager.LoadScene("GameScene5");
    }
    public void LoadChoicePanel()
    {
        SceneManager.LoadScene(1);
    }
}
