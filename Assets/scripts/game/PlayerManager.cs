namespace keke
{
    public class PlayerManager: Singleton<PlayerManager>
    {
        public string playerPrefabPath;
        private Player2d player;

        public override void Init()
        {

        }

        public void InitPlayerScene()
        {
            // create player prefab and set to member
            // GameObject.Load()
        }

        public void DestroyPlayerScene()
        {
            // destroy player game object and null member
        }
    }
}