using UnityEngine;

public class AbilityDebugListener : MonoBehaviour
{
    [SerializeField]
    private AbilityController controller;

    private void OnEnable()
    {
        controller.AbilityUsed += OnAbilityUsed;
    }

    private void OnDisable()
    {
        controller.AbilityUsed -= OnAbilityUsed;
    }

    private void OnAbilityUsed(
        AbilityDefinition ability)
    {
        Debug.Log(
            "Ability kullanıldı: " +
            ability.Id);
    }
}