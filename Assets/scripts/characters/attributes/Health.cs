namespace keke
{
    public class Health
    {
        public int current;

        public int max;

        public bool isAlive {
            get {
                return current > 0;
            }
        }

        public void Harm(int amount)
        {
            if(current > 0)
            {
                current -= amount;

                if(current < 0)
                {
                    current = 0;
                }
            }

        }

        public void Heal(int amount)
        {
            if(current < max)
            {
                current += amount;

                if(current < max)
                {
                    current = max;
                }
            }
        }
    }
}