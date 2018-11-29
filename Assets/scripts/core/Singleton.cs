namespace keke 
{
    using UnityEngine;

	public class Singleton<T>: MonoBehaviour where T: MonoBehaviour 
    {
		public bool isDoNotDestroyOnLoad = true;
		protected static T instance;
		
		public static T Instance 
        {
			get 
            {
				if(instance == null) 
                {
					instance = (T) FindObjectOfType(typeof(T));
					
					if(instance == null) 
                    {
						// Debug.LogError("An instance of " + typeof(T) + " is needed in scene, but not found");
					}
				}
				return instance;
			}
		}

		private void Awake() 
		{
			if(instance == null) 
			{
				if(isDoNotDestroyOnLoad)
				{
					DontDestroyOnLoad(gameObject);
				}
				instance = this as T;
			} 
			else if(this != Instance) 
			{
				Destroy(gameObject);
			}
			Init ();
		}


		public virtual void Init()
		{
		}
	}
}