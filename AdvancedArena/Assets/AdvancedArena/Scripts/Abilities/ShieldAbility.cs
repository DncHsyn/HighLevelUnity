using UnityEngine;

[CreateAssetMenu(
    menuName = "Advanced Arena/Abilities/Shield")]
public class ShieldAbility :
    AbilityDefinition
{
    [SerializeField]
    private float duration = 3f;

    public override void Execute(
        GameObject owner)
    {
        ShieldReceiver receiver =
            owner.GetComponent<ShieldReceiver>();

        if (receiver != null)
        {
            receiver.Activate(duration);
        }
    }
}