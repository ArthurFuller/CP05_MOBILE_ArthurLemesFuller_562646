using UnityEngine;

public class PredictiveChaseStrategy : EnemyMovementStrategy
{
    [SerializeField] private float leadTime = 0.45f;

    public override Vector3 GetDirection(Transform enemy, PlayerMovement player)
    {
        Vector3 target = player.transform.position + player.Velocity * leadTime;
        Vector3 direction = target - enemy.position;
        direction.y = 0f;
        return direction.normalized;
    }
}
