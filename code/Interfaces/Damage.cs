namespace astral_base.SCPRP;

public record Damage
{
    public float Amount { get; init; }
    public int Type { get; init; }
    public Player Attacker { get; init; }
    public bool Forced { get; init; }
}
