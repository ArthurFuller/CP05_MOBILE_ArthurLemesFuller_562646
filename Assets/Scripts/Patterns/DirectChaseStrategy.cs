using UnityEngine;

public class DirectChaseStrategy : EnemyMovementStrategy
{
    public override Vector3 GetDirection(Transform enemy, PlayerMovement player)
    {
        Vector3 direction = player.transform.position - enemy.position;
        direction.y = 0f;
        return direction.normalized;
    }
}
