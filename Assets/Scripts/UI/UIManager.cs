using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager getInstance() { return instance; }

    [SerializeField]
    GameObject startMenuCanvas;
    [SerializeField]
    GameObject loadingCanvas;
    [SerializeField]
    GameObject inGameCanvas;


    [SerializeField]
    SystemManager.SystemState currentGameState;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        changeActiveUI();
    }

    void changeActiveUI()
    {

        switch (currentGameState)
        {
            case SystemManager.SystemState.START:
                startMenuCanvas.SetActive(true);
                loadingCanvas.SetActive(false);
                inGameCanvas.SetActive(false);
                break;
            case SystemManager.SystemState.LOADING:
                startMenuCanvas.SetActive(false);
                loadingCanvas.SetActive(true);
                inGameCanvas.SetActive(false);
                break;
            case SystemManager.SystemState.INGAME:
                startMenuCanvas.SetActive(false);
                loadingCanvas.SetActive(false);
                inGameCanvas.SetActive(true);
                break;
        }
    }

    public void setCurrentGameState(SystemManager.SystemState newGameState)
    {
        this.currentGameState = newGameState;
        changeActiveUI();
    }

    public InGameUI getInGameUI()
    {
        return inGameCanvas.GetComponent<InGameUI>();
    }
}
