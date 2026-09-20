using UnityEngine;

[CreateAssetMenu(
    menuName = "Advanced Arena/Abilities/Fireball")]
public class FireballAbility : AbilityDefinition
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float damage = 25f;

    public override void Execute(GameObject owner)
    {
        Vector3 spawnPosition =
            owner.transform.position +
            owner.transform.forward;

        GameObject projectile =
            Object.Instantiate(
                projectilePrefab,
                spawnPosition,
                owner.transform.rotation);

        projectile
            .GetComponent<Projectile>()
            .Initialize(damage);
    }
}