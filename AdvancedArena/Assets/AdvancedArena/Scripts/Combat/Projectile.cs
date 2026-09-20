using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;

    private float damage;

    public void Initialize(float amount)
    {
        damage = amount;
    }

    private void Update()
    {
        transform.position +=
            transform.forward *
            speed *
            Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}