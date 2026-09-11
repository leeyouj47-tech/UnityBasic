using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SphereController : MonoBehaviour
{
    //체력 관련
    public int maxHealth = 1;
    public Text remainHpText;
    private int currentHp;
    public GameObject destroyEffect;
    public Transform effectTransform;
    public Slider healthbar;
    public bool isPressed;

    void Start()
    {
        currentHp = maxHealth;

        if (healthbar != null)
        {
            healthbar.wholeNumbers = true;
            healthbar.maxValue = maxHealth;
            healthbar.value = maxHealth;
        }
        if (remainHpText != null)
        {
            remainHpText.text = $"{currentHp}/{maxHealth}";
        }
    }

    public void OnAttack(InputValue value)
    {
        isPressed = value.isPressed;
    }

    void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            SphereController sphere = hit.collider.GetComponent<SphereController>();

            if (sphere != null)
            {
                sphere.TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHealth);
        if (healthbar != null)
        {
            healthbar.value = currentHp;
        }
        if (remainHpText != null)
        {
            remainHpText.text = $"{currentHp}/{maxHealth}";
        }
        Debug.Log($"TakeDmg => {gameObject.name}({currentHp})");
        if (currentHp == 0)
        {
            Destroy(gameObject);
            if (destroyEffect != null)
                Instantiate(destroyEffect, effectTransform.position, effectTransform.rotation);
        }
    }
}
