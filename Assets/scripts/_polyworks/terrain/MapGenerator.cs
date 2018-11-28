using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public enum DrawMode
	{
		Noise,
		Mesh,
		Falloff}

	;

	public DrawMode drawMode;

	public TerrainData terrainData;
	public NoiseData noiseData;
	public TextureData textureData;

	public Material terrainMaterial;

	[Range (0, 6)]
	public int previewLOD;

	public bool isAutoUpdate;

	private float[,] falloffMap;


	Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>> ();
	Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>> ();

	public int mapCellSize {
		get {
			if (terrainData.useFlatShading) {
				return 95;
			} else {
				return 239;
			}
		}
	}

	public void OnValuesUpdated ()
	{
		if (!Application.isPlaying) {
			DrawMapInEditor ();
		}
	}

	public void OnTextureValuesUpdated ()
	{
		textureData.ApplyToMaterial (terrainMaterial);
	}

	public void DrawMapInEditor ()
	{
		MapData mapData = CreateMapData (Vector2.zero);

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.Noise) {
			display.DrawTexture (TextureGenerator.BuildFromHeightMap (mapData.heightMap));
		} else if (drawMode == DrawMode.Mesh) {
			display.DrawMesh (MeshGenerator.GenerateTerrainMesh (mapData.heightMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve, previewLOD, terrainData.useFlatShading));
		} else if (drawMode == DrawMode.Falloff) {
			display.DrawTexture (TextureGenerator.BuildFromHeightMap (FalloffGenerator.CreateMap (mapCellSize)));
		}
	}

	public void RequestMapData (Vector2 center, Action<MapData> callback)
	{
		ThreadStart threadStart = delegate {
			MapDataThread (center, callback);
		};

		new Thread (threadStart).Start ();
	}

	private void MapDataThread (Vector2 center, Action<MapData> callback)
	{
		MapData mapData = CreateMapData (center);
		// prevent other threads from accessing queue at same time
		lock (mapDataThreadInfoQueue) {
			mapDataThreadInfoQueue.Enqueue (new MapThreadInfo<MapData> (callback, mapData));
		}
	}

	public void RequestMeshData (MapData mapData, int lod, Action<MeshData> callback)
	{
		ThreadStart threadStart = delegate {
			MeshDataThread (mapData, lod, callback);
		};

		new Thread (threadStart).Start ();
	}

	private void MeshDataThread (MapData mapData, int lod, Action<MeshData> callback)
	{
		MeshData meshData = MeshGenerator.GenerateTerrainMesh (mapData.heightMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve, lod, terrainData.useFlatShading);
		// prevent other threads from accessing queue at same time
		lock (meshDataThreadInfoQueue) {
			meshDataThreadInfoQueue.Enqueue (new MapThreadInfo<MeshData> (callback, meshData));
		}
	}

	private void Update ()
	{
		if (mapDataThreadInfoQueue.Count > 0) {
			for (int i = 0; i < mapDataThreadInfoQueue.Count; i++) {
				MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue ();
				threadInfo.callback (threadInfo.parameter);
			}
		}

		if (meshDataThreadInfoQueue.Count > 0) {
			for (int i = 0; i < meshDataThreadInfoQueue.Count; i++) {
				MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue ();
				threadInfo.callback (threadInfo.parameter);
			}
		}
	}

	private MapData CreateMapData (Vector2 center)
	{
		float[,] map = NoiseUtils.GeneratePerlin (mapCellSize + 2, 
			               mapCellSize + 2, 
			               noiseData.seed, 
			               noiseData.noiseScale, 
			               noiseData.octaves, 
			               noiseData.persistance, 
			               noiseData.lacunarity, 
			               center + noiseData.offset, 
			               noiseData.normalizeMode);

		if (terrainData.isUsingFalloffMap) {
			int size = mapCellSize + 2; 

			if (falloffMap == null) {
				falloffMap = FalloffGenerator.CreateMap (size);
			}

			for (int y = 0; y < size; y++) {
				for (int x = 0; x < size; x++) {

					if (terrainData.isUsingFalloffMap) {
						map [x, y] = Mathf.Clamp01 (map [x, y] - falloffMap [x, y]);
					}
				}
			}
		}

		textureData.UpdateMeshHeights (terrainMaterial, terrainData.minHeight, terrainData.maxHeight);

		return new MapData (map);
	}

	public void OnValidate ()
	{
		if (terrainData != null) {
			terrainData.OnValuesUpdated -= OnValuesUpdated;
			terrainData.OnValuesUpdated += OnValuesUpdated;
		}

		if (noiseData != null) {
			noiseData.OnValuesUpdated -= OnValuesUpdated;
			noiseData.OnValuesUpdated += OnValuesUpdated;
		}

		if (textureData != null) {
			textureData.OnValuesUpdated -= OnTextureValuesUpdated;
			textureData.OnValuesUpdated += OnTextureValuesUpdated;
		}

	}

	private struct MapThreadInfo<T>
	{
		public readonly Action<T> callback;
		public readonly T parameter;

		public MapThreadInfo (Action<T> callback, T parameter)
		{
			this.callback = callback;
			this.parameter = parameter;
		}
	}
}

public struct MapData
{
	public readonly float[,] heightMap;

	public MapData (float[,] heightMap)
	{
		this.heightMap = heightMap;
	}
	
}