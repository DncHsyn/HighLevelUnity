using UnityEngine;

[CreateAssetMenu(
    menuName = "Advanced Arena/Abilities/Heal")]
public class HealAbility : AbilityDefinition
{
    [SerializeField]
    private float healAmount = 30f;

    public override void Execute(
        GameObject owner)
    {
        Health health =
            owner.GetComponent<Health>();

        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}