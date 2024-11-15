namespace astral_base.SCPRP;

public record ChatMessage
{
    public string Text { get; init; }
    public ulong Author { get; init; }
    public string DisplayName { get; init; }
    public DateTime Time { get; init; }
    public bool IsOOC { get; set; }
    public bool IsActive { get; set; }
}