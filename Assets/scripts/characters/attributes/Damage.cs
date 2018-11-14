namespace keke
{
    public class Damage: CoreObject
    {
        public int strength;

        public void Trigger(Health target)
        {
            target.Harm(strength);
        }
    }
}