using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int id;
    public Button button;
    public Vector3 initPos;
    private bool isDraging;
    private bool showCharacter;
    public GameObject charatersShow;
    private Camera cam;

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
        transform.DOLocalMove(initPos, 0.2f).OnComplete(() => { isDraging = false; });
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
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
            float scale = Mathf.Clamp(((transform.localPosition.y - initPos.y)-60) / 60, 0, 1);
            charatersShow.transform.position= ScrenPointToWorld(transform.position,2.26f);
            charatersShow.transform.localScale=Vector3.one*scale;
            if (charatersShow.transform.localScale.x <= 0)
            {
                showCharacter = false;
            }
        }
        else
        {
            float scale =Mathf.Clamp(( 60-(transform.localPosition.y-initPos.y))/60,0,1);
            button.transform.localScale = Vector3.one * scale;
            if (button.transform.localScale.x <= 0)
            {
                showCharacter = true;
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
}
