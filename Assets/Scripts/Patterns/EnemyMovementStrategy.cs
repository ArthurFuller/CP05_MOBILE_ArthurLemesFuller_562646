using UnityEngine;

public abstract class EnemyMovementStrategy : MonoBehaviour
{
    public abstract Vector3 GetDirection(Transform enemy, PlayerMovement player);
}
