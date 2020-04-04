using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    GameManager gameManager;

    /* -- canvas -- */
    [Header("canvas")]
    [SerializeField]
    GameObject combatCanvas;
    [SerializeField]
    GameObject buildingCanvas;

    /* -- buttons -- */
    [Header("General buttons")]
    [SerializeField]
    Button swapPlayingStateButton;

    [Header("Building buttons")]
    [SerializeField]
    Button towerBuildingButton;
    [SerializeField]
    Button unitSpawnerBuildingButton;
    [SerializeField]
    Button researchBuildingButton;
    [SerializeField]
    Button otherBuildingButton;

    /* -- ressources -- */
    [Header("Ressources fields")]
    [SerializeField]
    Text textGoldAvailable;
    [SerializeField]
    Text textSearchAvailable;

    int valueGoldAvailable;
    int valueSearchAvailable;

    /* -- units -- */
    [Header("Unit fields")]
    [SerializeField]
    Image healthBarFill;
    [SerializeField]
    Image energieBarFill;
    [SerializeField]
    Image unitIcon;

    [Header("Unit values")]
    [SerializeField]
    int valueHealthBarFill;
    [SerializeField]
    int maxHealthBarFill;
    [SerializeField]
    int valueEnergieBarFill;
    [SerializeField]
    int maxEnergieBarFill;

    /* -- Building -- */
    [Header("Building list")]
    [SerializeField]
    GameObject towerList;
    [SerializeField]
    GameObject unitSpawnerList;
    [SerializeField]
    GameObject researchList;
    [SerializeField]
    GameObject otherList;

    private void Awake()
    {

    }
    void Start()
    {

        gameManager = GameManager.getInstance();
        InitialiseButton();
        
    }

    private void InitialiseButton()
    {

        swapPlayingStateButton.onClick.AddListener(SwapPlayingStateButtonClicked);

        towerBuildingButton.onClick.AddListener(TowerBuildingButtonClicked);
        unitSpawnerBuildingButton.onClick.AddListener(UnitSpawnerBuildingButtonClicked);
        researchBuildingButton.onClick.AddListener(ResearchBuildingButtonClicked);
        otherBuildingButton.onClick.AddListener(OtherBuildingButtonClicked);
    }

    private void SwapPlayingStateButtonClicked()
    {
        Debug.Log("SwapPlayingStateButtonClicked");
        gameManager.SwapPlayingState();
    }

    private void TowerBuildingButtonClicked()
    {
        Debug.Log("TowerBuildingButtonClicked");
        towerList.SetActive(!towerList.activeInHierarchy);
    }

    private void UnitSpawnerBuildingButtonClicked()
    {
        Debug.Log("UnitSpawnerBuildingButtonClicked");
        unitSpawnerList.SetActive(!unitSpawnerList.activeInHierarchy);
    }

    private void ResearchBuildingButtonClicked()
    {
        Debug.Log("ResearchBuildingButtonClicked");
        researchList.SetActive(!researchList.activeInHierarchy);
    }

    private void OtherBuildingButtonClicked()
    {
        Debug.Log("OtherBuildingButtonClicked");
        otherList.SetActive(!otherList.activeInHierarchy);
    }

    void Update()
    {
        UpdateUnitDisplay();
    }

    void UpdateRessoucesDisplay()
    {
        textGoldAvailable.text = valueGoldAvailable.ToString();
        textSearchAvailable.text = valueSearchAvailable.ToString();
    }

    void UpdateUnitDisplay()
    {
        healthBarFill.fillAmount = (float) valueHealthBarFill / maxHealthBarFill;
        energieBarFill.fillAmount = (float) valueEnergieBarFill / maxEnergieBarFill;
    }

    public void DisplayCombatUI()
    {
        combatCanvas.SetActive(true);
        buildingCanvas.SetActive(false);
    }

    public void DisplayBuildingUI()
    {
        combatCanvas.SetActive(false);
        buildingCanvas.SetActive(true);
    }

    public void setValueGoldAvailable(int goldValue)
    {
        this.valueGoldAvailable = goldValue;
        UpdateRessoucesDisplay();
    }

    public void setValueSearchAvailable(int searchValue)
    {
        this.valueSearchAvailable = searchValue;
        UpdateRessoucesDisplay();
    }

    public void setValueHealthBarFill(int healthValue)
    {
        this.valueHealthBarFill = healthValue;
    }

    public void setMaxHealthBarFill(int maxHealthValue)
    {
        this.maxHealthBarFill = maxHealthValue;
    }

    public void setValueEnergieBarFill(int energieValue)
    {
        this.valueEnergieBarFill = energieValue;
    }

    public void setMaxEnergieBarFill(int maxEnergieValue)
    {
        this.maxEnergieBarFill = maxEnergieValue;
    }

    public void setUnitIcon(Sprite unitIcon)
    {
        this.unitIcon.sprite = unitIcon;
    }
}
