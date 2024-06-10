using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModeManager : MonoBehaviour
{
    public static ModeManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Error more than one " + name + " component found");
            return;
        }
        instance = this;
    }

    //Layouts
    public GameObject[] layouts;
    public int currentLayout;

    public virtual void PreStartGame()
    {

    }

    public virtual void StartGame()
    {

    }

    public virtual void EndGame(Transform theWinner)
    {

    }

    public void ChangeLayout(bool increase)
    {
        layouts[currentLayout].SetActive(false);
        if (increase)
        {
            currentLayout++;
            if (currentLayout >= layouts.Length)
                currentLayout = 0;
        }
        else
        {
            currentLayout--;
            if (currentLayout < 0)
                currentLayout = layouts.Length - 1;
        }
        layouts[currentLayout].SetActive(true);
    }
}
