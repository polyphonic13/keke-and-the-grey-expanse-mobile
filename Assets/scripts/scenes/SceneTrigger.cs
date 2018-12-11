namespace keke
{
	using System;
	using UnityEngine;

	public class SceneTrigger: CoreObject
	{
		public string target;
		public void Execute()
		{
			Game.Instance.ChangeScene(target);
		}
	}
}