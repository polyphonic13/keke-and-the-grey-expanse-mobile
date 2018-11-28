using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseUtils {

	public enum NormalizeMode { Local, Global };

	public static float[,] GeneratePerlin(int width, int height, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset, NormalizeMode normalizeMode) {
		float[,] map = new float[width, height];

		System.Random prng = new System.Random (seed);
		Vector2[] octaveOffsets = new Vector2[octaves];

		float maxPossibleHeight = 0;
		float amplitude = 1;
		float frequency = 1;

		for (int i = 0; i < octaves; i++) {
			float offsetX = prng.Next (-100000, 100000) + offset.x;
			float offsetY = prng.Next (-100000, 100000) - offset.y;
			octaveOffsets [i] = new Vector2(offsetX, offsetY);

			maxPossibleHeight += amplitude;
			amplitude *= persistence;
		}

		if (scale < 0) {
			scale = 0.001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = width / 2f;
		float halfHeight = height / 2f; 

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {

				amplitude = 1;
				frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) {

					float sampleX = (x - halfWidth + octaveOffsets[i].x) / scale * frequency;
					float sampleY = (y - halfHeight + octaveOffsets[i].y) / scale * frequency;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1; // x2-1 gives positive & negative values, instead of 0 - 1
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistence;
					frequency *= lacunarity;

					if (noiseHeight > maxNoiseHeight) {
						maxNoiseHeight = noiseHeight;
					} else if (noiseHeight < minNoiseHeight) {
						minNoiseHeight = noiseHeight;
					}
//					Debug.Log ("x/y[" + x + "/" + y + "] height = " + noiseHeight);
					map [x, y] = noiseHeight;
				}
			}
		}
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				// normalize values back to 0 - 1
				if (normalizeMode == NormalizeMode.Local) {
					map [x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, map [x, y]);
				} else {
					float normalized = (map [x, y] + 1) / (maxPossibleHeight);
					map [x, y] = Mathf.Clamp(normalized, 0, int.MaxValue);
				}
			}
		}
		return map;
	}
}
