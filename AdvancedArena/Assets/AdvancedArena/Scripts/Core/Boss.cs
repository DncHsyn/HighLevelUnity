using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public void TakeDamage(float amount)
    {
        Debug.Log("Boss damage aldı.");
    }
}