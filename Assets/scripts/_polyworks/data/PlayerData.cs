namespace Polyworks {
	using System;

	[System.Serializable]
	public class PlayerData: Data
	{
		public float health = 100; 
		public float stamina = 100;
		public float breath = 100;

		public PlayerData() {}

		public override Data Clone() {
			PlayerData clone = base.Clone() as PlayerData;
			clone.health = this.health;
			clone.stamina = this.stamina;
			clone.breath = this.breath;

			return clone as PlayerData;
		}
	}

	[Serializable]
	public enum AttributeType 
	{
		HEALTH,
		SPEED,
		STRENGTH,
		DEFENSE,
		STAMINA
	}

}

