public class CrystalPickup : Pickup
{
    private const int ScoreValue = 10;

    protected override void ApplyEffect(PlayerStats player)
    {
        player.AddScore(ScoreValue);
    }
}
