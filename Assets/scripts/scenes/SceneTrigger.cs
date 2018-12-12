namespace keke
{
	using System;
	using UnityEngine;

	public class SceneTrigger: CoreObject
	{
		public static string target;
		public static void Execute()
		{
			Game.Instance.ChangeScene(target);
		}
	}
}