namespace keke
{
    using System;
    using UnityEngine;

    public class SimpleMovement2d: CoreObject 
    {
        public Vector2 speed = new Vector2(0, 0);
        public Vector2 direction = new Vector2(0, 0);

        private void FixedUpdate()
        {
            if(this.isActive)
            {
                Vector3 movement = new Vector3(
                    speed.x * direction.x,
                    speed.y * direction.y,
                    0
                );

                movement *= Time.deltaTime;
                transform.Translate(movement);
            }
        }
    }
}