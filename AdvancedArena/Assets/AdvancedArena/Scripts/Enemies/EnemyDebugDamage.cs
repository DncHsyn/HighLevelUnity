using UnityEngine;

public class EnemyDebugDamage : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.TakeDamage(25f);
        }
    }
}