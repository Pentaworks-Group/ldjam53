using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ContinousButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float interval = 0.2f;
    [SerializeField] private float startTime = 0f;
    [SerializeField] private UnityEvent repeatingMethod;
    [SerializeField] private UnityEvent clickMethod;

    [SerializeField] private float t1t;

    public void OnPointerDown(PointerEventData eventdata)
    {
        if (repeatingMethod != default)
        {
            InvokeRepeating("RepeatingCall", startTime, interval);
        }
        if (clickMethod != default)
        {
            clickMethod.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventdata)
    {
        CancelInvoke("RepeatingCall");
    }

    void RepeatingCall()
    {
        repeatingMethod.Invoke();
    }
}
