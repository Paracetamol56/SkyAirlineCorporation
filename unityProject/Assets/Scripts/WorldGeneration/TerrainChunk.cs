using UnityEngine;
using UnityEditor;
using System.Collections;

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
    GameObject tree;

    bool createSpawn,hastree=false;


    public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LODInfo[] detailLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material, bool CreateSpawn, GameObject treeGameObject)
    {
        this.coord = coord;
        this.detailLevels = detailLevels;
        this.colliderLODIndex = colliderLODIndex;
        this.heightMapSettings = heightMapSettings;
        this.meshSettings = meshSettings;
        this.viewer = viewer;
        tree = treeGameObject;
        

        sampleCentre = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
        Vector2 position = coord * meshSettings.meshWorldSize;
        bounds = new Bounds(position, Vector2.one * meshSettings.meshWorldSize);
        chunkPos = new Vector3(position.x,0,position.y);

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
            return new Vector3(position.x,0,position.y);
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
        bool endloop = true; ;
        int numberOfTree = 1;
        float randPosX = Random.Range(-bounds.size.x / 2, bounds.size.x / 2);
        float randPosZ = Random.Range(-bounds.size.y / 2, bounds.size.y / 2);
        Vector3 pos = new Vector3(randPosX+chunkPos.x, 1000, randPosZ+chunkPos.z);

        RaycastHit hit;
        //float res = heightMapSettings.heightCurve.Evaluate(Noise.GetPosZ(x, y, heightMapSettings.noiseSettings, meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, sampleCentre)) * heightMapSettings.heightMultiplier;

        if (Physics.Raycast(pos, Vector3.down, out hit, 10000))
        {
            Debug.LogError("Raycast hit");
            Debug.DrawRay(pos, Vector3.down * hit.distance, Color.red);
            float posY = hit.point.y;
            //float posX = hit.point.x;
            //float posZ = hit.point.z;
            pos = new Vector3(pos.x, posY, pos.z);
            Debug.LogError(hit.point.y + " " +hit.distance);
            Debug.LogError(hit.collider.gameObject.name);
            endloop = false;
        }
        else
        {
            Debug.LogError("RayCast Didn't hit");
        }
      
        for (int i = 0; i < numberOfTree; i++)
        {
            GameObject newTree = Instantiate(tree);
            newTree.transform.parent = meshObject.transform;

            newTree.transform.position = pos;

            //Forest.Add(newTree);
        }
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