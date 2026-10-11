using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => maxHealth;

    public event Action Died;

    private bool isDead;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead || amount <= 0f)
            return;

        CurrentHealth = Mathf.Max(
            0f, CurrentHealth - amount);

        Debug.Log(
            gameObject.name +
            " kalan can: " +
            CurrentHealth);

        if (CurrentHealth <= 0f)
        {
            isDead = true;
            Died?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (isDead || amount <= 0f)
            return;

        CurrentHealth = Mathf.Min(
            maxHealth,
            CurrentHealth + amount);

        Debug.Log(
            gameObject.name +
            " iyileşti. Can: " +
            CurrentHealth);
    }
}