using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    public Button btSinglePlayer;
    public Button btMultiplayer;

    private List<AsyncOperation> allScenes;
    private bool doneLoadingScenes;

    void Awake()
    {
        
        btSinglePlayer.onClick.AddListener(EnterSinglePlayer);
        btMultiplayer.onClick.AddListener(EnterMultiPlayer);
    }

    private void EnterSinglePlayer()
    {
        EnableScene(1);
    }

    private void EnterMultiPlayer()
    {
        EnableScene(1);
    }

   
    private void EnableScene(int index)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    private void OnFinishedLoadingAllScene()
    {
        EnableScene(0);
    }
}
