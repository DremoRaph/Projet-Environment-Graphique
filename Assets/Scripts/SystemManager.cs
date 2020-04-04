using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    private static SystemManager instance;
    public static SystemManager getInstance() { return instance; }

    public enum SystemState {NONE, START, LOADING, INGAME };

    [SerializeField]
    SystemManager.SystemState currentGameState;

    Scene uiScene;
    Scene inGameScene;

    UIManager uiManager;
    GameManager GameManager;

    private void Awake()
    {
        instance = this;
        uiScene = SceneManager.LoadScene("UIScene", new LoadSceneParameters(LoadSceneMode.Additive));
    }

    // Start is called before the first frame update
    void Start()
    {
        this.currentGameState = SystemState.START;

        uiManager = UIManager.getInstance();
        uiManager.setCurrentGameState(this.currentGameState);

    }

    // Update is called once per frame
    void Update()
    {
        if(this.currentGameState == SystemState.LOADING && inGameScene.isLoaded)
        {
            OnLoadingOver();
        }
    }

    public void changeCurrentGameSate()
    {
        switch (this.currentGameState)
        {
            case SystemState.START:
                CreateInGameScene();
                this.currentGameState = SystemState.LOADING;
                break;

            case SystemState.LOADING:
                this.currentGameState = SystemState.INGAME;
                break;

            case SystemState.INGAME:
                DestroyInGameScene();
                this.currentGameState = SystemState.START;
                break;
        }

        uiManager.setCurrentGameState(this.currentGameState);

    }

    void CreateInGameScene()
    {
        inGameScene = SceneManager.LoadScene("InGameScene", new LoadSceneParameters(LoadSceneMode.Additive));

    }

    void DestroyInGameScene()
    {
        SceneManager.UnloadSceneAsync(inGameScene.buildIndex);
    }

    void OnLoadingOver()
    {
        changeCurrentGameSate();
    }

}
