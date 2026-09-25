using UnityEngine;

// FACTORY PATTERN
// A Factory escolhe e entrega um objeto do tipo solicitado, mas NAO instancia nada.
// Todos os objetos do pool ja existem na Hierarchy e sao apenas reutilizados.
public class PickupFactory : MonoBehaviour
{
    [SerializeField] private Pickup[] crystalPool;
    [SerializeField] private Pickup[] healthPool;
    [SerializeField] private Transform[] spawnPoints;

    private int nextSpawnPoint;

    public int ActiveCount => CountActive(crystalPool) + CountActive(healthPool);

    private void Awake()
    {
        PreparePool(crystalPool);
        PreparePool(healthPool);
    }

    public Pickup ActivatePickup(PickupType type)
    {
        Pickup pickup = GetAvailable(type == PickupType.Crystal ? crystalPool : healthPool);
        if (pickup == null || spawnPoints == null || spawnPoints.Length == 0)
            return null;

        Transform spawnPoint = GetNextSpawnPoint();
        pickup.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        pickup.gameObject.SetActive(true);
        return pickup;
    }

    public void Recycle(Pickup pickup)
    {
        if (pickup != null)
            pickup.gameObject.SetActive(false);
    }

    private void PreparePool(Pickup[] pool)
    {
        if (pool == null)
            return;

        foreach (Pickup pickup in pool)
        {
            if (pickup == null)
                continue;

            pickup.Configure(this);
            pickup.gameObject.SetActive(false);
        }
    }

    private static Pickup GetAvailable(Pickup[] pool)
    {
        if (pool == null)
            return null;

        foreach (Pickup pickup in pool)
        {
            if (pickup != null && !pickup.gameObject.activeSelf)
                return pickup;
        }

        return null;
    }

    private Transform GetNextSpawnPoint()
    {
        Transform point = spawnPoints[nextSpawnPoint % spawnPoints.Length];
        nextSpawnPoint = (nextSpawnPoint + 1) % spawnPoints.Length;
        return point;
    }

    private static int CountActive(Pickup[] pool)
    {
        if (pool == null)
            return 0;

        int count = 0;
        foreach (Pickup pickup in pool)
        {
            if (pickup != null && pickup.gameObject.activeSelf)
                count++;
        }

        return count;
    }
}
