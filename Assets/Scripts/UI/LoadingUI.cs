using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{

    [SerializeField]
    Image loadingBarFill;
    [SerializeField]
    int valueLoadingBarFill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadingBarFill.fillAmount = (float) valueLoadingBarFill / 100;
    }
}
