using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    public Text remainHpText;
    private int currentHp;
    public GameObject destroyEffect;
    public Transform effectTransform;
    public Slider healthbar;
    public Transform camera;
    
    void Start()
    {
        currentHp = maxHealth;

        if(healthbar != null)
        {
            healthbar.wholeNumbers = true;
            healthbar.maxValue = maxHealth;
            healthbar.value = maxHealth;
        }
        if(remainHpText != null)
        {
            remainHpText.text = $"{currentHp}/{maxHealth}";
        }
    }
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHealth);
        if(healthbar != null)
        {
            healthbar.value = currentHp;
        }
        if (remainHpText != null)
        {
            remainHpText.text = $"{currentHp}/{maxHealth}";
        }
        Debug.Log($"TakeDmg => {gameObject.name}({currentHp})");
        if(currentHp == 0)
        {
            if(camera != null)
                camera.SetParent(null);
            Destroy(gameObject);
            if(destroyEffect != null)
                Instantiate(destroyEffect, effectTransform.position, effectTransform.rotation);
        }
    }
}
