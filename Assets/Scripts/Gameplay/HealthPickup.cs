public class HealthPickup : Pickup
{
    private const int HealValue = 1;

    protected override void ApplyEffect(PlayerStats player)
    {
        player.Heal(HealValue);
    }
}
