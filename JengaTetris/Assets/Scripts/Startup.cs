using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    [SerializeField]private Button btSinglePlayer;
  
    private List<AsyncOperation> allScenes;
    private bool doneLoadingScenes;

    void Awake()
    {
        btSinglePlayer.onClick.AddListener(EnterSinglePlayer);
    }

    private void EnterSinglePlayer()
    {
        EnableScene(1);
    }

    private void EnableScene(int index)=>SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
  
}
