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

        public void harm(int amount)
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

        public void heal(int amount)
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