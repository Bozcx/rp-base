namespace astral_base.SCPRP
{
    public class Damage
    {
        public float Amount { get; set; }
        public int Type { get; set; }
        public Player Attacker  { get; set; }

        public bool Forced  { get; set; }

        public Damage(float amount, int type, Player attacker, bool forced)
        {
            Amount = amount;
            Type = type;
            Attacker = attacker;
            Forced = forced;
        }
    }
}
