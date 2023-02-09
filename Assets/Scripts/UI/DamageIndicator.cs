using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    private TextMeshProUGUI damageIndicatorText;

    public void ShowDamage(int damage)
    {
        damageIndicatorText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        damageIndicatorText.text = damage.ToString();
        Destroy(gameObject, 0.5f);
    }
}
