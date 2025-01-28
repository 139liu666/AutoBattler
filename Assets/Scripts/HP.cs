using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Camera mainCamera;
    public Slider slider;

    void Awake()
    {
        // ��ȡ�������������
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ��ȡ�������������Ӧ�õ� UI ������
        transform.LookAt(transform.position +
            mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
    public void SetHPColorSlider(bool isRed)
    {
        if (isRed)
        {
            slider = transform.Find("Slider_red").GetComponent<Slider>();
        } 
        else
        {
            slider = transform.Find("Slider_blue").GetComponent<Slider>();
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void SetHPValue(float value)
    {
        if (!slider.gameObject.activeSelf)
        {
            slider.gameObject.SetActive(true);
        }
        slider.value = value;
    }
}
