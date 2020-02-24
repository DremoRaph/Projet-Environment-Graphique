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

    Scene UIScene;

    [SerializeField]
    UIManager uiManager;

    private void Awake()
    {
        instance = this;
        UIScene = SceneManager.LoadScene("UI", new LoadSceneParameters(LoadSceneMode.Additive));
    }

    // Start is called before the first frame update
    void Start()
    {
        this.currentGameState = SystemState.START;

        uiManager = FindObjectOfType<UIManager>();
        uiManager.setCurrentGameState(this.currentGameState);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCurrentGameSate()
    {
        switch (this.currentGameState)
        {
            case SystemState.START:
                this.currentGameState = SystemState.LOADING;
                break;

            case SystemState.LOADING:
                this.currentGameState = SystemState.INGAME;
                break;

            case SystemState.INGAME:
                this.currentGameState = SystemState.START;
                break;
        }

        uiManager.setCurrentGameState(this.currentGameState);

    }

}
