using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    private int maxCardNum=4;
    private int currentCardNum;
    public Transform[] boardCard;
    public Transform boardTrans;
    private void Awake()
    {
        Instance = this;
      
        CreatNewCard();
    }
    void Start()
    {
        
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
        int randomNum =  Random.Range(1,11);
        cardlist.Add(randomNum);
        Image image =go.transform.GetChild(0).GetComponent<Image>();
        image.sprite = cardsprites[randomNum - 1];
        go.GetComponent<Card>().id = randomNum;
        if (currentCardNum < maxCardNum)
        {
            MoveCardToBoard(currentCardNum);
            currentCardNum++;
        }
    }
    private void MoveCardToBoard(int posID)
    {
        Transform t = nextcard.GetChild(0);
        //t.SetParent(boardCard[posID]);
        // t.localPosition = Vector3.zero;
        t.SetParent(boardTrans);
        t.DOLocalMove(boardCard[posID].localPosition, 0.4f).OnComplete(() => { CompleteMove(t); });
    }
    
    private void CompleteMove(Transform t)
    {
        CreatNewCard();
        t.GetComponent<Card>().SetInitPos();
    }
}
