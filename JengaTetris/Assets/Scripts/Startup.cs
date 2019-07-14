using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    private List<AsyncOperation> allScenes;
    public const string SCENE_MENU = "Menu";
    public const string SCENE_GAME = "Game";
  
    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(SCENE_MENU,LoadSceneMode.Single);
    }

}
