namespace keke
{
    using System;
    using UnityEngine;
    using UnityStandardAssets.CrossPlatformInput;

    public class MovementController: CoreObject {
        private bool isJumped; 
        private bool isFired; 

        void FixedUpdate()
        {
            isJumped = CrossPlatformInputManager.GetButtonDown("Jump");
            isFired = CrossPlatformInputManager.GetButtonDown("Fire");

            
        }
    }
}