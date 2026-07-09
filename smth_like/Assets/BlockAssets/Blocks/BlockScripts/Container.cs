using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Container : MonoBehaviour
{
    public Vector3 containerPos;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    MeshData meshData = new MeshData();

    public GameObject trigger;

    public Voxel activeBlock;

    public void Initialize(Material mat, Vector3 pos)
    {
        ConfigureComponents();
        meshRenderer.sharedMaterial = mat;
        containerPos = pos;
        trigger = gameObject.GetComponentInParent<WorldManager>().triggerBlock;
    }

    private void ConfigureComponents()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }


    public void GenerateMesh(Vector3 initPos, byte blockId)
    {
        meshData.ClearData();

        Vector3 blockPos = initPos;
        activeBlock = new Voxel()
        {
            Id = blockId,
            currentPos = initPos,
            triggerBlock = trigger
        };

        int count = 0;
        Vector3[] faceVertices = new Vector3[4];
        Vector2[] faceUVs = new Vector2[4];

        //Iterate face direction
        for (int i = 0; i < 6; i++)
        {
            //Draw face
            //i is face, j/k is verticies
            for (int j = 0; j < 4; j++)
            {
                faceVertices[j] = voxelVertices[voxelVertexes[i, j]] + blockPos;
                faceUVs[j] = voxelUVs[j];
            }

            for (int k = 0; k < 6; k++)
            {
                meshData.vertices.Add(faceVertices[voxelTriangles[i, k]]);
                meshData.UVs.Add(faceUVs[voxelTriangles[i, k]]);

                meshData.triangles.Add(count++);
            }
        }
    }

    public void UploadMesh()
    {
        meshData.UploadMesh();

        if (meshRenderer == null)
        {
            ConfigureComponents();
        }

        meshFilter.mesh = meshData.mesh;
        if (meshData.vertices.Count > 3)
        {
            meshCollider.sharedMesh = meshData.mesh;
        }
    }

    #region MultiBlock

    public List<RaycastHit> MultiBlock1(RaycastHit origin)
    {
        Vector3[] ScanRadius = new Vector3[4];
        ScanRadius[0] = new Vector3(1, 0, 0);
        ScanRadius[1] = new Vector3(0, 0, 1);
        ScanRadius[2] = new Vector3(-1, 0, 0);
        ScanRadius[3] = new Vector3(0, 0, -1);

        Vector3 offset = new Vector3(0.5f, 0, 0.5f);

        List<RaycastHit> blocksFound = new List<RaycastHit>();
        RaycastHit blockFound;

        for (int i = 0; i < 4; i++)
        {
            bool holder;
            //gameObject.transform.GetComponentInParent<WorldManager>().CreateTriggers(gameObject.transform, ScanRadius[i], 1);
            holder = Physics.Raycast(gameObject.transform.position + offset, ScanRadius[i], out blockFound, 2);

            //activeBlock.multi1Active = gameObject.transform.parent.Find("TriggerContainer").GetComponent<MultiCheck>().Collide;
            if (holder)
            {
               
                blocksFound.Add(blockFound);
               
            }
            
        }

        return blocksFound;
    }

    #endregion


    #region Voxels

    static readonly Vector3[] voxelVertices = new Vector3[8]
            {
                    new Vector3(0, 0, 0),
                    new Vector3(1, 0, 0),
                    new Vector3(0, 1, 0),
                    new Vector3(1, 1, 0),
                    new Vector3(0, 0, 1),
                    new Vector3(1, 0, 1),
                    new Vector3(0, 1, 1),
                    new Vector3(1, 1, 1),
            };

    static readonly int[,] voxelVertexes = new int[6, 4]
    {
        {0, 1, 2, 3},
        {4, 5, 6, 7},
        {4, 0, 6, 2},
        {5, 1, 7, 3},
        {0, 1, 4, 5},
        {2, 3, 6, 7}
    };

    static readonly Vector2[] voxelUVs = new Vector2[4]
    {
        new Vector2(0, 0),
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(1, 1)
    };

    static readonly int[,] voxelTriangles = new int[6, 6]
    {
        {0, 2, 3, 0, 3, 1},
        {0, 1, 2, 1, 3, 2},
        {0, 2, 3, 0, 3, 1},
        {0, 1, 2, 1, 3, 2},
        {0, 1, 2, 1, 3, 2},
        {0, 2, 3, 0, 3, 1}
    };

    #endregion

    #region Mesh Data

    public struct MeshData
    {
        public Mesh mesh;
        public List<Vector3> vertices;
        public List<int> triangles;
        public List<Vector2> UVs;

        public bool Initialized;

        public void ClearData()
        {
            if (!Initialized)
            {
                vertices = new List<Vector3>();
                triangles = new List<int>();
                UVs = new List<Vector2>();

                Initialized = true;
                mesh = new Mesh();
            }
            else
            {
                vertices.Clear();
                triangles.Clear();
                UVs.Clear();
                mesh.Clear();
            }
        }

        public void UploadMesh(bool sharedVertices = false)
        {
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0, false);
            mesh.SetUVs(0, UVs);

            mesh.Optimize();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            mesh.UploadMeshData(false);
        }
    }
}
    #endregion
