using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int id;
    public Button button;
    private  Vector3 initPos;
    private bool isDraging;
    private bool showCharacter;
    private Camera cam;
    public int posID;
    public AudioClip UserCard;
    void Start()
    {
        cam=Camera.main;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!button.interactable)
        {
            return;
        }
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!button.interactable)
        {
            return;
        }
        button.transform.localScale = Vector3.one;
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hs = Physics.RaycastAll(r);
        for (int i = 0; i < hs.Length; i++)
        {
            RaycastHit h = hs[i];
            if ((h.collider == null ||h.collider.tag != "FriendUnit"))
            {
                showCharacter = false;
            }
            else
            {
                showCharacter=true;
                break;
            }
        }
        if (showCharacter)
        {
            //射线检测
            GameManager.Instance.PlaySound(UserCard);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits=Physics.RaycastAll(ray);
            UseCurrentCard(hits);
        }
        else
        {
            ReturnToInitpos();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!button.interactable)
        {
            return;
        }
        Vector2 cardpos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out cardpos);
        transform.localPosition = cardpos;
        if (showCharacter)
        {
            //float scale = Mathf.Clamp(((transform.localPosition.y - initPos.y)-60) / 60, 0, 0.1f );
           // charatersShow.transform.position= ScrenPointToWorld(transform.position,2.26f);
            //charatersShow.transform.localScale=Vector3.one*scale;
            //if (charatersShow.transform.localScale.x <= 0)
            //{
                //showCharacter = false;
                //button.gameObject.SetActive(true);
                //charatersShow.SetActive(false);
           // }
        }
        else
        {
            float scale =Mathf.Clamp(( 60-(transform.localPosition.y-initPos.y))/60,0,1);
            button.transform.localScale = Vector3.one * scale;
            if (button.transform.localScale.x <= 0)
            {
                showCharacter = true;
                button.gameObject.SetActive(false);
                //charatersShow.SetActive(true);
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
       button.interactable= GameControler.Instance.CanUseCard(id);
    }
    public void SetInitPos()
    {
        initPos = transform.localPosition;
    }
    private Vector3 ScrenPointToWorld(Vector2 screnpoint,float planeZ)
    {
        return cam.ScreenToWorldPoint(new Vector3(screnpoint.x, screnpoint.y, planeZ));
    }
    private void ReturnToInitpos()
    {
       // charatersShow.SetActive(false);
        button.gameObject.SetActive(true);
        transform.DOLocalMove(initPos, 0.2f).OnComplete(() => { isDraging = false; });
    }
    private void UseCurrentCard(RaycastHit[] hits)
    {
        //花费
        GameControler.Instance.DecreaseMoney(id);
        //检测地面
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider != null&&hit.collider.tag=="FriendUnit")
            {
                Vector3 targetPos = hit.point;
                GameControler.Instance.CreateUnit(id,targetPos,false);
                UIManager.Instance.Usecard(posID);
                Destroy(gameObject);
                
            }
           
        }
       

    }
}
