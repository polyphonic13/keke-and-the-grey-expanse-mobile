using UnityEngine;
using System.Collections;
using System; 

[CreateAssetMenu()]
public class TerrainData : UpdatableData
{
	[Serializable]	
	public enum TerrainDataType { FLAT, FLAT_WITH_VALLEYS, HILLY, MOUNTAINOUS };

	public TerrainDataType type;
	public string name; 

	public float uniformScale = 2.5f;

	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve; 

	public bool isUsingFalloffMap;
	public bool useFlatShading;

	public float minHeight {
		get {
			return uniformScale * meshHeightMultiplier * meshHeightCurve.Evaluate (0);
		}
	}

	public float maxHeight { 
		get {
			return uniformScale * meshHeightMultiplier * meshHeightCurve.Evaluate(1);
		}
	}
}


