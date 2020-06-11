using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fade : MonoBehaviour
{
    [SerializeField]
    private bool visible;
    public float time = 0.2f;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //visible = (Math.Abs(canvasGroup.alpha - 1) < 0.01f);
    }

    public void SwapFade()
    {
        visible = !visible;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    public void SetFade(bool t)
    {
        visible = t;
        if(canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    private void Update()
    {
        if (visible)
        {
            canvasGroup.alpha += 1 / time * Time.deltaTime;
        }
        else
        {
            canvasGroup.alpha -= 1 / time * Time.deltaTime;
        }
    }
}
