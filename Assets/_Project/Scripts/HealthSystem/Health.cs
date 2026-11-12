using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;
    public float CurrentHealth => currentHealth;

    public bool IsDead { get; private set; }
    public Action<float, float> OnHealthChanged;
    public Action Died;

    private void Awake()
    {
        currentHealth = maxHealth;
    
    }


    public void TakeDamege(float damage)
    {

        if (IsDead) return;

        currentHealth -= damage;   //toglie vita                                
        Debug.Log($"HP: {currentHealth}/{maxHealth}");
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
        if(currentHealth<=0f)
        {
            Die();  //gameObject.SetActive(false); come se ci fosse scitto questo
        }
    }

    public void Heal (float heal)
    {
        currentHealth += heal;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        if (currentHealth>maxHealth)          //
        {                                   //
            currentHealth = maxHealth;  // oppure Mathf.Clamp(currentHealth,of, maxhealth); <-- è meglio mettere questo
        }                                  //
    }

    public void SetHealth(float _health, float _maxHealth)  // al posto di _ posso  mettre This
    {
        currentHealth = _health;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        maxHealth = _maxHealth;

    }

    void Die()
    {
        IsDead = true;
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
