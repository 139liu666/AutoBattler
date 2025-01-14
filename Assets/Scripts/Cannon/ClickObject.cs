using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;
using UnityEngine.WSA;



public class ClickObject : MonoBehaviour

{

    public GameObject cube;

    public ObjectLauncher lanceur;

    // Start is called before the first frame update

    void Start()

    {
        

    }



    // Update is called once per frame

    void Update()

    {

        if (Input.GetMouseButtonDown(0))

        {

            if (cube == GetClickedObject(out RaycastHit hit))

            {

                print("clicked/touched!");
                lanceur.LaunchObject();
            }

        }

        if (Input.GetMouseButtonUp(0))

        {


        }

    }



    GameObject GetClickedObject(out RaycastHit hit)

    {

        GameObject target = null;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))

        {

            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }

        }

        return target;

    }

    private bool isPointerOverUIObject()

    {

        PointerEventData ped = new PointerEventData(EventSystem.current);

        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(ped, results);

        return results.Count > 0;

    }

}
