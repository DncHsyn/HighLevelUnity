using UnityEngine;

[CreateAssetMenu(
    menuName = "Advanced Arena/Abilities/Dash")]
public class DashAbility : AbilityDefinition
{
    [SerializeField]
    private float distance = 5f;

    public override void Execute(
        GameObject owner)
    {
        owner.transform.position +=
            owner.transform.forward *
            distance;
    }
}