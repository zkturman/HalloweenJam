using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    private GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.Start;    
    }

    public bool InStartState()
    {
        return currentGameState == GameState.Start;
    }

    public void SetRetrievedSkullState()
    {
        currentGameState = GameState.HasRetrievedSkull;
    }

    public bool InRetrievedSkullState()
    {
        return currentGameState == GameState.HasRetrievedSkull;
    }

    public void SetReceivedHintsState()
    {
        currentGameState = GameState.HasReceivedHints;
    }

    public bool InOrAboveHintsState()
    {
        return currentGameState >= GameState.HasReceivedHints;
    }

    public bool InReceivedHintsState()
    {
        return currentGameState == GameState.HasReceivedHints;
    }

    public void SetHasAllShardsState()
    {
        currentGameState = GameState.HasAllShards;
    }

    public bool InHasAllShardsState()
    {
        return currentGameState == GameState.HasAllShards;
    }

    public void SetGameFinishedState()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("ConclusionStory");
    }

    public void SetGameOverState()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");
    }
}
