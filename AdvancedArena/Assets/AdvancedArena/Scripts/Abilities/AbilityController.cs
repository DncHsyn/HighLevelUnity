using System;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField]
    private AbilityDefinition[] abilities;

    private float[] cooldowns;

    public event Action<AbilityDefinition> AbilityUsed;

    private void Awake()
    {
        cooldowns = new float[abilities.Length];
    }

    public void Use(int index)
    {
        if (index < 0 || index >= abilities.Length)
        {
            return;
        }

        if (abilities[index] == null)
        {
            return;
        }

        if (cooldowns[index] > 0f)
        {
            return;
        }

        abilities[index].Execute(gameObject);

        cooldowns[index] = abilities[index].Cooldown;

        AbilityUsed?.Invoke(abilities[index]);
    }

    private void Update()
    {
        for (int i = 0; i < cooldowns.Length; i++)
        {
            cooldowns[i] = Mathf.Max(
                0f,
                cooldowns[i] - Time.deltaTime
            );
        }
    }
}