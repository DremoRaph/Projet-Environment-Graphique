using UnityEngine;
using UnityEngine.UI;

public class StartMenuUi : MonoBehaviour
{
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button optionButton;
    [SerializeField]
    Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartButtonClicked);
        optionButton.onClick.AddListener(OptionButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartButtonClicked()
    {
        SystemManager.getInstance().changeCurrentGameSate();
    }

    void OptionButtonClicked()
    {
        Debug.Log("OptionButtonClicked");
    }

    void ExitButtonClicked()
    {
        Debug.Log("ExitButtonClicked");
    }

}
