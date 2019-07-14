using Assets.Scripts.view.common;

public class MenuView : GameComponent
{
   
    void Awake()
    {
        EnableMenu(false);
    }
    public void EnableMenu(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }
}
