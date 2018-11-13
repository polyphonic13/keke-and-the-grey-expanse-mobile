namespace keke
{
    public class Damage
    {
        public int strength;

        public void Trigger(Health target)
        {
            target.Harm(strength);
        }
    }
}