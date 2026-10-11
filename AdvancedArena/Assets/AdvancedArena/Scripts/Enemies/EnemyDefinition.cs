using UnityEngine;

[CreateAssetMenu(
    menuName = "Advanced Arena/Enemies/Enemy Definition")]
public class EnemyDefinition : ScriptableObject
{
    [SerializeField]
    private string enemyName;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float damage = 10f;

    public string EnemyName => enemyName;

    public float MaxHealth => maxHealth;

    public float MoveSpeed => moveSpeed;

    public float Damage => damage;
}