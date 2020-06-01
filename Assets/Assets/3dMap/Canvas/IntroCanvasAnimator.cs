using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class IntroCanvasAnimator : MonoBehaviour
{
    public Fade background;
    public WindowManager windowManager;
    public Animator toBeDisabled;

    private void Start()
    {
        background.SetFade(false);
        background.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Begin()
    {
        background.SetFade(true);
        windowManager.NextPage();
        toBeDisabled.enabled = false;
    }
}
