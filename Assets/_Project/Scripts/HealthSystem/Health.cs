using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;

    public float CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    
    }


    public void TakeDamege(float damage)
    {
        currentHealth -= damage;   //toglie vita                                
        Debug.Log($"HP: {currentHealth}/{maxHealth}");
        if(currentHealth<=0f)
        {
            Die();  //gameObject.SetActive(false); come se ci fosse scitto questo
        }
    }

    public void Heal (float heal)
    {
        currentHealth += heal;
        if(currentHealth>maxHealth)          //
        {                                   //
            currentHealth = maxHealth;  // oppure Mathf.Clamp(currentHealth,of, maxhealth); <-- è meglio mettere questo
        }                                  //
    }

    public void SetHealth(float _health, float _maxHealth)  // al posto di _ posso  mettre This
    {
        currentHealth = _health;
        maxHealth = _maxHealth;

    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
