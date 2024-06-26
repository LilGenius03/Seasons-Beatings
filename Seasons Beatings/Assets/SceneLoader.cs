using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneIndex;
    public float delay;
    void Start()
    {
        Invoke(nameof(loadScene), delay);
    }

    void loadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
