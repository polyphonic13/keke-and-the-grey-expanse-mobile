namespace keke
{
    using UnityEngine;
    public class PlayerManager: Singleton<PlayerManager>
    {
        public string playerPrefabPath;

        public bool isPlayerActive { get; set; }
        private Player2d player;

        public override void Init()
        {
            Debug.Log("PlayerManager/Init");
            isPlayerActive = false;
        }

        public void InitPlayerScene()
        {
            Debug.Log("PlayerManager/IniPlayerScene");
            // create player prefab and set to member
            // GameObject.Load()
            isPlayerActive = true;
        }

        public void DestroyPlayerScene()
        {
            // destroy player game object and null member
            isPlayerActive = false;
        }
    }
}