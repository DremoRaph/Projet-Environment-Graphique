using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    /* -- buttons -- */
    [Header("ButtonS")]
    [SerializeField]
    Button SwapModeButton;

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

    void Start()
    {
      
    }

    void Update()
    {
        UpdateRessoucesDisplay();
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

    void setValueGoldAvailable(int goldValue)
    {
        this.valueGoldAvailable = goldValue;
    }

    void setValueSearchAvailable(int searchValue)
    {
        this.valueSearchAvailable = searchValue;
    }

    void setValueHealthBarFill(int healthValue)
    {
        this.valueHealthBarFill = healthValue;
    }

    void setMaxHealthBarFill(int maxHealthValue)
    {
        this.maxHealthBarFill = maxHealthValue;
    }

    void setValueEnergieBarFill(int energieValue)
    {
        this.valueEnergieBarFill = energieValue;
    }

    void setMaxEnergieBarFill(int maxEnergieValue)
    {
        this.maxEnergieBarFill = maxEnergieValue;
    }

    void setUnitIcon(Sprite unitIcon)
    {
        this.unitIcon.sprite = unitIcon;
    }
}
