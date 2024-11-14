using Sandbox.Events;
using astral_base.SCPRP;

public record PlayerLoadedIn(Player player) : IGameEvent;

public record PlayerChangeJob(Player player, Job job) : IGameEvent;

public record PlayerTakeDamage(Player player, Damage damage) : IGameEvent;
public record PlayerTookDamage(Player player, Damage damage) : IGameEvent;