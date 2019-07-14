using Assets.Scripts.view.common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuStartupView : GameComponent {

    [SerializeField] private Button btSinglePlayer;

    void Start () => btSinglePlayer.onClick.AddListener(EnterSinglePlayer);
    private void EnterSinglePlayer()=> SceneManager.LoadScene(Startup.SCENE_GAME, LoadSceneMode.Single);
    
}
