namespace astral_base.RPBASE;

public record Damage
{
    public float Amount { get; init; }
    public int Type { get; init; }
    public Player Attacker { get; init; }
    public bool Forced { get; init; }
}
