using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{

  const float viewerMoveThresholdForChunkUpdate = 25f;
  const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;


  public int colliderLODIndex;
  public LODInfo[] detailLevels;

  public MeshSettings meshSettings;
  public HeightMapSettings heightMapSettings;
  public TextureData textureSettings;

  public Transform viewer;
  public Material mapMaterial;

  Vector2 viewerPosition;
  Vector2 viewerPositionOld;

  float meshWorldSize;
  int chunksVisibleInViewDst;

  Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
  List<TerrainChunk> visibleTerrainChunks = new List<TerrainChunk>();

  void Start()
  {

    textureSettings.ApplyToMaterial(mapMaterial);
    textureSettings.UpdateMeshHeights(mapMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);

    float maxViewDst = detailLevels[detailLevels.Length - 1].visibleDstThreshold;
    meshWorldSize = meshSettings.meshWorldSize;
    chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / meshWorldSize);

    FirstChunk();

  }

  void Update()
  {
    viewerPosition = new Vector2(viewer.position.x, viewer.position.z);

    if (viewerPosition != viewerPositionOld)
    {
      foreach (TerrainChunk chunk in visibleTerrainChunks)
      {
        chunk.UpdateCollisionMesh();
      }
    }

    if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
    {
      viewerPositionOld = viewerPosition;
      UpdateVisibleChunks();
    }
  }

  void UpdateVisibleChunks()
  {
    HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2>();
    for (int i = visibleTerrainChunks.Count - 1; i >= 0; i--)
    {
      //Debug.Log(visibleTerrainChunks.Count);
      alreadyUpdatedChunkCoords.Add(visibleTerrainChunks[i].coord);
      visibleTerrainChunks[i].UpdateTerrainChunk();
    }

    int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshWorldSize);
    int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshWorldSize);

    for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
    {
      for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
      {
        Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
        if (!alreadyUpdatedChunkCoords.Contains(viewedChunkCoord))
        {
          if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
          {
            terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
          }
          else
          {
            TerrainChunk newChunk = new TerrainChunk(viewedChunkCoord, heightMapSettings, meshSettings, detailLevels, colliderLODIndex, transform, viewer, mapMaterial, false);
            terrainChunkDictionary.Add(viewedChunkCoord, newChunk);
            newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
            newChunk.Load();
          }
        }

      }
    }
  }
  void FirstChunk()
  {
    HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2>();
    for (int i = visibleTerrainChunks.Count - 1; i >= 0; i--)
    {
      Debug.Log(visibleTerrainChunks.Count);
      alreadyUpdatedChunkCoords.Add(visibleTerrainChunks[i].coord);
      visibleTerrainChunks[i].UpdateTerrainChunk();
    }

    int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshWorldSize);
    int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshWorldSize);

    for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
    {
      for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
      {
        Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
        if (!alreadyUpdatedChunkCoords.Contains(viewedChunkCoord))
        {
          if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
          {
            terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
          }
          else
          {
            TerrainChunk newChunk = new TerrainChunk(viewedChunkCoord, heightMapSettings, meshSettings, detailLevels, colliderLODIndex, transform, viewer, mapMaterial, heightMapSettings.noiseSettings.createSpawn);
            terrainChunkDictionary.Add(viewedChunkCoord, newChunk);
            newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
            newChunk.Load();
          }
        }

      }
    }
    UpdateVisibleChunks();
  }

  void OnTerrainChunkVisibilityChanged(TerrainChunk chunk, bool isVisible)
  {
    if (isVisible)
    {
      visibleTerrainChunks.Add(chunk);
    }
    else
    {
      visibleTerrainChunks.Remove(chunk);
    }
  }
  /*
  public float BlackBox(float x, float y, Vector2 sampleCentre)
  {
      float res = heightMapSettings.heightCurve.Evaluate(Noise.GetPosZ(x, y, heightMapSettings.noiseSettings, meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, sampleCentre)) * heightMapSettings.heightMultiplier;

      // Raycast test
      RaycastHit hit;
      if (Physics.Raycast(new Vector3(x, 10000, y), Vector3.down, out hit, 50000))
      {
          float raycastDistance = hit.distance;
          raycastDistance = 10000 - raycastDistance;
          // Debug raycast result and mathematical result
          Debug.Log("Raycast hit at " + hit.point + " with height " + raycastDistance);
          Debug.Log("Math result is " + res);
      }
      else
      {
          Debug.LogError("Raycast missed");
      }

      return res;
  }*/
}

[System.Serializable]
public struct LODInfo
{
  [Range(0, MeshSettings.numSupportedLODs - 1)]
  public int lod;
  public float visibleDstThreshold;


  public float sqrVisibleDstThreshold
  {
    get
    {
      return visibleDstThreshold * visibleDstThreshold;
    }
  }
}
