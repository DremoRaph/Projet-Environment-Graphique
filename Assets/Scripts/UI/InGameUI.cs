using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    /* -- ressources -- */
    [Header("Ressources fields")]
    [SerializeField]
    Text textGoldAvailable;
    [SerializeField]
    Text textSearchAvailable;

    [Header("Ressources values")]
    [SerializeField]
    int valueGoldAvailable;
    [SerializeField]
    int valueSearchAvailable;

    /* -- unit -- */
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
        textGoldAvailable = transform.Find("Fond_ressource/Gold/Value").GetComponent<Text>();
        textSearchAvailable = transform.Find("Fond_ressource/Search/Value").GetComponent<Text>();
        healthBarFill = transform.Find("Fond_unit_selectionee/Health/Bar_fill").GetComponent<Image>();
        energieBarFill = transform.Find("Fond_unit_selectionee/Energie/Bar_fill").GetComponent<Image>();
        unitIcon = transform.Find("Fond_unit_selectionee/Unit_icon/Icon").GetComponent<Image>();
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

    void setValueSearchAvailable(int SearchValue)
    {
        this.valueSearchAvailable = SearchValue;
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
