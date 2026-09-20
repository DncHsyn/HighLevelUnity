using UnityEngine;
public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] 
    private float maxHealth = 100f;
    public float CurrentHealth { get; private set; }
    
    private void Awake()
    {
        CurrentHealth = maxHealth;
    }
    
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        
        Debug.Log(gameObject.name + " kalan can: " + CurrentHealth);
        
        if (CurrentHealth <= 0f) 
        { 
            Destroy(gameObject); 
        } 
    }
}