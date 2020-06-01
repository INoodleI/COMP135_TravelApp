using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<Fade> screens;
    private int activeScreen;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var f in screens)
            f.SetFade(false);
        
        
        activeScreen = 0;
        screens[activeScreen].SetFade(true);
    }

    public void SetScreen(int index)
    {
        if (index == activeScreen)
            return;

        screens[activeScreen].SetFade(false);
        activeScreen = index;
        screens[activeScreen].SetFade(true);
    }
}
