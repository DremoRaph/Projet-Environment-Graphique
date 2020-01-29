using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HPBar;
    private float hitpoint = 150;
    private float maxHitPoint = 150;

    private void Start()
    {
        UpdateHPBar();
    }
    private void UpdateHPBar()
    {
        float ration = hitpoint / maxHitPoint;
        HPBar.rectTransform.localScale = new Vector3(ration, 1, 1);
    }

    private void TakeDamage( float damage)
    {
        hitpoint -= damage;
        if (hitpoint < 0)
        {
            hitpoint = 0;
            Debug.Log("Dead!");
        }
        UpdateHPBar();

    }

    private void HealDamage(float heal)
    {
        hitpoint += heal;
        if (hitpoint > maxHitPoint)
        {
            hitpoint = maxHitPoint;
        }
        UpdateHPBar();
    }

}
