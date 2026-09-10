using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHp;
    public GameObject destroyEffect;
    public Transform effectTransform;
    public Slider healthbar;
    public Transform camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHealth;

        if(healthbar != null)
        {
            healthbar.wholeNumbers = true;
            healthbar.maxValue = maxHealth;
            healthbar.value = maxHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHealth);
        if(healthbar != null)
        {
            healthbar.value = currentHp;
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
