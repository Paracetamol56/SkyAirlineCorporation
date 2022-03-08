using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TerrainChunk : MonoBehaviour
{

    const float colliderGenerationDistanceThreshold = 1000000;
    public event System.Action<TerrainChunk, bool> onVisibilityChanged;
    public Vector2 coord;

    GameObject meshObject;
    Vector2 sampleCentre;
    Bounds bounds;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    MeshRenderer waterRenderer;
    MeshFilter waterFilter;
    MeshCollider waterCollider;

    LODInfo[] detailLevels;
    LODMesh[] lodMeshes;
    int colliderLODIndex;

    HeightMap heightMap;
    bool heightMapReceived;
    int previousLODIndex = -1;
    bool hasSetCollider;
    float maxViewDst;
    ArrayList Forest;


    HeightMapSettings heightMapSettings;
    MeshSettings meshSettings;
    Transform viewer;

    Vector3 chunkPos;
    List<GameObject> tree;
    int numberOfTree;

    bool createSpawn, hastree = false;


    public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LODInfo[] detailLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material, bool CreateSpawn, List<GameObject> treeGameObject, int numberOfTreePerChunck)
    {
        this.coord = coord;
        this.detailLevels = detailLevels;
        this.colliderLODIndex = colliderLODIndex;
        this.heightMapSettings = heightMapSettings;
        this.meshSettings = meshSettings;
        this.viewer = viewer;
        tree = treeGameObject;
        numberOfTree = numberOfTreePerChunck;


        sampleCentre = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
        Vector2 position = coord * meshSettings.meshWorldSize;
        bounds = new Bounds(position, Vector2.one * meshSettings.meshWorldSize);
        chunkPos = new Vector3(position.x, 0, position.y);

        meshObject = new GameObject("Terrain Chunk");
        meshObject.tag = "Ground";
        meshObject.layer = LayerMask.NameToLayer("Ground");
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        meshCollider = meshObject.AddComponent<MeshCollider>();
        meshRenderer.material = material;

        meshObject.transform.position = new Vector3(position.x, 0, position.y);
        meshObject.transform.parent = parent;
        SetVisible(false);

        lodMeshes = new LODMesh[detailLevels.Length];
        if (CreateSpawn)
        {
            createSpawn = true;
        }
        for (int i = 0; i < detailLevels.Length; i++)
        {
            lodMeshes[i] = new LODMesh(detailLevels[i].lod);
            lodMeshes[i].updateCallback += UpdateTerrainChunk;
            if (i == colliderLODIndex)
            {
                lodMeshes[i].updateCallback += UpdateCollisionMesh;
            }
        }


        maxViewDst = detailLevels[detailLevels.Length - 1].visibleDstThreshold;
        Vector3 ReturnPos()
        {
            return new Vector3(position.x, 0, position.y);
        }
    }

    public void Load()
    {
        ThreadedDataRequester.RequestData(() => HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, sampleCentre, createSpawn), OnHeightMapReceived);
    }



    void OnHeightMapReceived(object heightMapObject)
    {
        this.heightMap = (HeightMap)heightMapObject;
        heightMapReceived = true;

        UpdateTerrainChunk();
    }

    Vector2 viewerPosition
    {
        get
        {
            return new Vector2(viewer.position.x, viewer.position.z);
        }
    }


    public void UpdateTerrainChunk()
    {
        if (heightMapReceived)
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));

            bool wasVisible = IsVisible();
            bool visible = viewerDstFromNearestEdge <= maxViewDst;

            if (visible)
            {
                int lodIndex = 0;

                for (int i = 0; i < detailLevels.Length - 1; i++)
                {
                    if (viewerDstFromNearestEdge > detailLevels[i].visibleDstThreshold)
                    {
                        lodIndex = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (lodIndex != previousLODIndex)
                {
                    LODMesh lodMesh = lodMeshes[lodIndex];
                    if (lodMesh.hasMesh)
                    {
                        previousLODIndex = lodIndex;
                        meshFilter.mesh = lodMesh.mesh;
                    }
                    else if (!lodMesh.hasRequestedMesh)
                    {
                        lodMesh.RequestMesh(heightMap, meshSettings);
                    }
                }


            }

            if (wasVisible != visible)
            {

                SetVisible(visible);
                if (onVisibilityChanged != null)
                {
                    onVisibilityChanged(this, visible);
                }
            }
        }
    }

    public void UpdateCollisionMesh()
    {
        if (!hasSetCollider)
        {
            float sqrDstFromViewerToEdge = bounds.SqrDistance(viewerPosition);

            if (sqrDstFromViewerToEdge < detailLevels[colliderLODIndex].sqrVisibleDstThreshold)
            {
                if (!lodMeshes[colliderLODIndex].hasRequestedMesh)
                {
                    lodMeshes[colliderLODIndex].RequestMesh(heightMap, meshSettings);
                }
            }

            if (sqrDstFromViewerToEdge < colliderGenerationDistanceThreshold * colliderGenerationDistanceThreshold)
            {
                if (lodMeshes[colliderLODIndex].hasMesh)
                {
                    meshCollider.sharedMesh = lodMeshes[colliderLODIndex].mesh;
                    hasSetCollider = true;
                    if (!hastree)
                    {
                        CreateTree();
                        hastree = true;
                    }
                }
            }
        }
    }

    public void SetVisible(bool visible)
    {
        meshObject.SetActive(visible);
    }

    public bool IsVisible()
    {
        return meshObject.activeSelf;
    }
    public void CreateTree()
    {

        
        for (int i = 0; i < numberOfTree; i++)
        {

            GameObject newTree = Instantiate(tree[Random.Range(0, tree.Count - 1)]);
            newTree.transform.parent = meshObject.transform;

            newTree.transform.position = FindPosOfTree();

        }
    }
    private Vector3 FindPosOfTree()
    {
        Vector3 pos = new Vector3(1, 1000, 1);
        RaycastHit hit;

        float randPosX = Random.Range(-bounds.size.x / 2, bounds.size.x / 2);
        float randPosZ = Random.Range(-bounds.size.y / 2, bounds.size.y / 2);
        pos = new Vector3(randPosX + chunkPos.x, 1000, randPosZ + chunkPos.z);
        if (Physics.Raycast(pos, Vector3.down, out hit, 10000))
        {

            Debug.DrawRay(pos, Vector3.down * hit.distance, Color.red);
            float posY = hit.point.y;
            pos = new Vector3(pos.x, posY, pos.z);
            if (hit.point.y < 365)
            {
                if (Physics.Raycast(new Vector3(pos.x, 365, pos.z), Vector3.left, out hit, 10000))
                {
                    pos = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                }
            }
        }
        return pos;
    }
}

class LODMesh
{

    public Mesh mesh;
    public bool hasRequestedMesh;
    public bool hasMesh;
    int lod;
    public event System.Action updateCallback;

    public LODMesh(int lod)
    {
        this.lod = lod;
    }

    void OnMeshDataReceived(object meshDataObject)
    {
        mesh = ((MeshData)meshDataObject).CreateMesh();
        hasMesh = true;
        updateCallback();
    }

    public void RequestMesh(HeightMap heightMap, MeshSettings meshSettings)
    {
        hasRequestedMesh = true;
        ThreadedDataRequester.RequestData(() => MeshGenerator.GenerateTerrainMesh(heightMap.values, meshSettings, lod), OnMeshDataReceived);
    }
}
