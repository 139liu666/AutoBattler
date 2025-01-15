using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;
using UnityEngine.WSA;
using System;



public class ClickObject : MonoBehaviour

{
    public GameObject cube;
    public ObjectLauncher lanceur;

    void Update()

    {

        if (Input.GetMouseButtonDown(0))

        {

            if (cube == GetClickedObject(out RaycastHit hit))

            {
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
