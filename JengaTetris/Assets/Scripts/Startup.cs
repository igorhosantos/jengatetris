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

    public GamenetManager gamenetManager;

    void Awake()
    {
        btSinglePlayer.onClick.AddListener(EnterSinglePlayer);
        btMultiplayer.onClick.AddListener(EnterMultiPlayer);
        GamenetController.ME.StartController(gamenetManager);

    }

    private void EnterSinglePlayer()
    {
        GamenetController.ME.EnableManager(false);
        EnableScene(2);
    }

    private void EnterMultiPlayer()
    {
        GamenetController.ME.EnableManager(true);

        EnableScene(1);
    }
    
    private void EnableScene(int index)=>SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    
    private void OnFinishedLoadingAllScene()
    {
        EnableScene(0);
    }
}
