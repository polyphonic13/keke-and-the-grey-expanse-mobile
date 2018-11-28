using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour {

	const float VIEW_MOVE_THRESHOLD_FOR_UPDATE = 25f;
	const float SQ_VIEW_MOVE_THRESHOLD_FOR_UPDATE = VIEW_MOVE_THRESHOLD_FOR_UPDATE * VIEW_MOVE_THRESHOLD_FOR_UPDATE; 

	public LODInfo[] detailLevels; 
	public static float maxViewDistance = 300;

	public Transform viewer; 
	public Material mapMaterial; 

	public static Vector2 viewerPosition;
	private Vector2 previousViewerPosition;

	static MapGenerator mapGenerator;

	private int cellSize;
	private int cellsVisibleInViewDistance;

	Dictionary<Vector2, TerrainCell> terrainCellDictionary = new Dictionary<Vector2, TerrainCell>();
	static List<TerrainCell> terrainCellsVisibleLastUpdate = new List<TerrainCell> ();

	private void Start() {
		mapGenerator = FindObjectOfType<MapGenerator> ();
		maxViewDistance = detailLevels [detailLevels.Length - 1].visibilityThreshold;

		cellSize = mapGenerator.mapCellSize - 1;
		cellsVisibleInViewDistance = Mathf.RoundToInt (maxViewDistance / cellSize);

		UpdateVisibleCells ();
	}

	private void Update() {
		viewerPosition = new Vector2 (viewer.position.x, viewer.position.z) / mapGenerator.terrainData.uniformScale;

		if ((previousViewerPosition - viewerPosition).sqrMagnitude > SQ_VIEW_MOVE_THRESHOLD_FOR_UPDATE) {
			previousViewerPosition = viewerPosition;
			UpdateVisibleCells ();
		}
	}

	private void UpdateVisibleCells() {
		for (int i = 0; i < terrainCellsVisibleLastUpdate.Count; i++) {
			terrainCellsVisibleLastUpdate [i].SetVisible (false);
		}
		terrainCellsVisibleLastUpdate.Clear ();

		int currentCellCoordX = Mathf.RoundToInt (viewerPosition.x / cellSize);
		int currentCellCoordY = Mathf.RoundToInt (viewerPosition.y / cellSize);

		for (int yOffset = -cellsVisibleInViewDistance; yOffset <= cellsVisibleInViewDistance; yOffset++) {
			for (int xOffset = -cellsVisibleInViewDistance; xOffset <= cellsVisibleInViewDistance; xOffset++) {
				Vector2 viewedCellCoord = new Vector2 (currentCellCoordX + xOffset, currentCellCoordY + yOffset);

				if (terrainCellDictionary.ContainsKey (viewedCellCoord)) {
					terrainCellDictionary [viewedCellCoord].UpdateCell ();

				} else {
					terrainCellDictionary.Add (viewedCellCoord, new TerrainCell (viewedCellCoord, cellSize, detailLevels, this.transform, mapMaterial));
				}
			}
		}
	}

	public class TerrainCell {

		GameObject meshObject;
		Vector2 position;
		Bounds bounds;

		private MeshRenderer meshRenderer;
		private MeshFilter meshFilter; 
		private MeshCollider meshCollider; 

		private LODInfo[] detailLevels; 
		private LODMesh[] lodMeshes; 
		private LODMesh collisionLODMesh; 

		private MapData mapData;
		private bool hasMapData; 

		private int previousLODIndex = -1; 

		public TerrainCell(Vector2 coords, int size, LODInfo[] detailLevels, Transform parent, Material material) {
			this.detailLevels = detailLevels; 

			position = coords * size;
			bounds = new Bounds(position, Vector2.one * size);

			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			meshObject = new GameObject("terrainCell");
			meshRenderer = meshObject.AddComponent<MeshRenderer>();
			meshFilter = meshObject.AddComponent<MeshFilter>();
			meshCollider = meshObject.AddComponent<MeshCollider>(); 
			Debug.Log("meshObject = " + meshObject);
			meshRenderer.material = material; 

			meshObject.transform.position = positionV3 * mapGenerator.terrainData.uniformScale;
			meshObject.transform.parent = parent;
			meshObject.transform.localScale = Vector3.one * mapGenerator.terrainData.uniformScale;

			SetVisible(false);

			lodMeshes = new LODMesh[detailLevels.Length];
			for(int i = 0; i < detailLevels.Length; i++) {
				lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateCell);

				if(detailLevels[i].useForCollider) {
					collisionLODMesh = lodMeshes[i];
				}
			}
			mapGenerator.RequestMapData(position, OnMapDataReceived);
		}

		public void OnMapDataReceived(MapData mapData) {
			this.mapData = mapData;
			hasMapData = true;

			UpdateCell ();
		}

		public void UpdateCell() {
			if (hasMapData) {
				float viewerDistanceFromNearestEdge = Mathf.Sqrt (bounds.SqrDistance (viewerPosition));
				bool isVisible = viewerDistanceFromNearestEdge <= maxViewDistance;

				if (isVisible) {
					int lodIndex = 0;
					for (int i = 0; i < detailLevels.Length - 1; i++) {
						if (viewerDistanceFromNearestEdge > detailLevels [i].visibilityThreshold) {
							lodIndex = i + 1;
						} else {
							break;
						}
					}

					if (lodIndex != previousLODIndex) {
						LODMesh lodMesh = lodMeshes [lodIndex];
						if (lodMesh.hasMesh) {
							previousLODIndex = lodIndex;
							meshFilter.mesh = lodMesh.mesh;

						} else if (!lodMesh.hasRequestedMesh) {
							lodMesh.RequestMesh (mapData);
						}
					}

					if (lodIndex == 0) {
						if (collisionLODMesh.hasMesh) {
							Debug.Log ("found the mesh to set for collider");
							meshCollider.sharedMesh = collisionLODMesh.mesh;
						} else if (!collisionLODMesh.hasRequestedMesh) {
							collisionLODMesh.RequestMesh (mapData);
						}
					}

					terrainCellsVisibleLastUpdate.Add (this);

				}
				SetVisible(isVisible);
			}
		}

		public void SetVisible(bool isVisible) {
			meshObject.SetActive (isVisible);
		}

		public bool GetIsVisible() {
			return meshObject.activeSelf;
		}
	}

	public class LODMesh {
		public Mesh mesh;
		public bool hasRequestedMesh;
		public bool hasMesh; 
		System.Action updateCallback;

		int lod;

		public LODMesh(int lod, System.Action updateCallback) {
			this.lod = lod;
			this.updateCallback = updateCallback;
		}

		private void OnMeshDataReceived(MeshData meshData) {
			mesh = meshData.CreateMesh ();
			hasMesh = true;

			updateCallback ();
		}

		public void RequestMesh(MapData mapData) {
			hasRequestedMesh = true;
			mapGenerator.RequestMeshData (mapData, this.lod, OnMeshDataReceived);
		}
	}

	[System.Serializable]
	public struct LODInfo {
		public int lod;
		public float visibilityThreshold;
		public bool useForCollider;
	}
}
