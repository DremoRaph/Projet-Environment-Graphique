using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject startMenuCanvas;
    GameObject loadingCanvas;
    GameObject inGameCanvas;

    public enum GameState { START, LOADING, INGAME };

    [SerializeField]
    GameState currentGameState;

    void Start()
    {
        startMenuCanvas = transform.Find("Start_Menu_Canvas").gameObject;
        loadingCanvas = transform.Find("Loading_Canvas").gameObject;
        inGameCanvas = transform.Find("In_Game_Canvas").gameObject;
        changeActiveUI();
    }

    void changeActiveUI()
    {

        switch (currentGameState)
        {
            case GameState.START:
                startMenuCanvas.SetActive(true);
                loadingCanvas.SetActive(false);
                inGameCanvas.SetActive(false);
                break;
            case GameState.LOADING:
                startMenuCanvas.SetActive(false);
                loadingCanvas.SetActive(true);
                inGameCanvas.SetActive(false);
                break;
            case GameState.INGAME:
                startMenuCanvas.SetActive(false);
                loadingCanvas.SetActive(false);
                inGameCanvas.SetActive(true);
                break;
        }
    }

    void setCurrentGameState(GameState newGameState)
    {
        this.currentGameState = newGameState;
        changeActiveUI();
    }

}
