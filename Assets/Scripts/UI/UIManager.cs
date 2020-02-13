using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text textGoldAvailable;
    Text textSearchAvailable;

    [SerializeField]
    int valueGoldAvailable;
    [SerializeField]
    int valueSearchAvailable;



    // Start is called before the first frame update
    void Start()
    {
        textGoldAvailable = transform.Find("Canvas/Fond_ressource/Gold/Value").GetComponent<Text>();
        textSearchAvailable = transform.Find("Canvas/Fond_ressource/Search/Value").GetComponent<Text>();       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRessoucesDisplay();
    }

    void UpdateRessoucesDisplay()
    {
        textGoldAvailable.text = valueGoldAvailable.ToString();
        textSearchAvailable.text = valueSearchAvailable.ToString();
    }

    void setValueGoldAvailable(int goldValue)
    {
        this.valueGoldAvailable = goldValue;
    }

    void setValueSearchAvailable(int SearchValue)
    {
        this.valueSearchAvailable = SearchValue;
    }
}
