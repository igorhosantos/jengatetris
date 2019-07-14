using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    [SerializeField]private Button btSinglePlayer;
    [SerializeField] private Button btMultiplayerHost;
    [SerializeField] private Button btMultiplayerClient;

    private List<AsyncOperation> allScenes;
    private bool doneLoadingScenes;

    public GamenetManager gamenetManager;

    void Awake()
    {
        btSinglePlayer.onClick.AddListener(EnterSinglePlayer);
        btMultiplayerHost.onClick.AddListener(EnterMultiPlayerHost);
        btMultiplayerClient.onClick.AddListener(EnterMultiPlayerClient);
        GamenetController.ME.StartController(gamenetManager);
    }

    private void EnterSinglePlayer()
    {
        GamenetController.ME.EnableManager(false);
        EnableScene(2);
    }

    private void EnterMultiPlayerHost()
    {
        GamenetController.ME.EnableManager(true);
        GamenetController.ME.StartHost();
    }

    private void EnterMultiPlayerClient()
    {
        GamenetController.ME.EnableManager(true);
        GamenetController.ME.StartClient();
    }
    private void EnableScene(int index)=>SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
  
}
