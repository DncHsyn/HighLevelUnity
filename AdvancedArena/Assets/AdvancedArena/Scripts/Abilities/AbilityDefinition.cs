using UnityEngine;

public abstract class AbilityDefinition : ScriptableObject
{
    [field: SerializeField]
    public string Id { get; private set; }

    [field: SerializeField]
    public float Cooldown { get; private set; }

    public abstract void Execute(GameObject owner);
}