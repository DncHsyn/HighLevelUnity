using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    private EnemyDefinition enemyDefinition;

    public EnemyDefinition EnemyDefinition => enemyDefinition;
}
