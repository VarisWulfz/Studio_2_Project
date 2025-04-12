using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthFill;
    public float healthAmount;
    public float maxHealth = 100f;
    public float lerpTimer = 0.1f;

    private void Start()
    {
        healthAmount = maxHealth;
    }

    private void Update()
    { 
        if (healthAmount > maxHealth) healthAmount = maxHealth;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, healthAmount / 100, lerpTimer);
    }

}
