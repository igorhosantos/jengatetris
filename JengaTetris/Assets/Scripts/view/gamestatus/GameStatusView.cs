using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameStatusView : MonoBehaviour
{
    [SerializeField] private Text statusLabel;

    private int countStart;

    private const string TEXT_GO = "GO!";
    private const string TEXT_LOSE = "YOU LOSE";
    private const string TEXT_WIN = "YOU WIN";

    public UnityAction OnStartGame;

    public enum GAMESTATE
    {
        ON_START,
        ON_WIN,
        ON_LOSE
    }

    public void UpdateState(GAMESTATE state)
    {
        gameObject.SetActive(true);

        switch (state)
        {
            case GAMESTATE.ON_START:
                SetStart();
                break;
            case GAMESTATE.ON_LOSE:
                statusLabel.text = TEXT_LOSE;
                break;
            case GAMESTATE.ON_WIN:
                statusLabel.text = TEXT_WIN;
                break;
            default:
                throw new Exception("Wrong State");
        }
    }

    private void SetStart()
    {
        countStart = 3;
        UpdateStartCount();
        InvokeRepeating(nameof(UpdateStartCount),1f,1f);
    }

    private void UpdateStartCount()
    {
        statusLabel.text = countStart.ToString();
        if (countStart == 0) statusLabel.text = TEXT_GO;
        countStart--;
        if (countStart == -1)
        {
            CancelInvoke(nameof(UpdateStartCount));
            Invoke(nameof(DispatchGameStarted), 1f);
        }
    }

    private void DispatchGameStarted()
    {
        
        OnStartGame.Invoke();
        gameObject.SetActive(false);
    }
}
