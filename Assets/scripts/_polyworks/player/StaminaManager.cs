using UnityEngine;

public class StaminaManager : MonoBehaviour {

	private const float RECHARGE_DELAY = 5f;

	private float _nextActionTime;

	private float _maxStamina;
	private float _remainingStamina; 

	public static bool IsBoosted { get; set; }

	void Awake() {

//		_remainingStamina = _maxStamina = Game.Instance.RemainingStamina;
//		Game.Instance.UpdateStamina(_remainingStamina);
	}

	void Update() {
	}
	
}
