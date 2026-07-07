using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Material worldMaterial;
    public Material worldMaterial2;
    private Container container;

    public int blockNumber = 0;

    private GameObject blocks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject baseContainer = new GameObject("BaseContainer");
        baseContainer.transform.parent = transform;
        container = baseContainer.AddComponent<Container>();

        container.Initialize(worldMaterial, Vector3.zero);
        container.GenerateMesh(Vector3.zero);
        container.UploadMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateContainer()
    {
        GameObject baseContainer = new GameObject("BaseContainer" + blockNumber);
        baseContainer.transform.parent = transform;
        container = baseContainer.AddComponent<Container>();

        container.Initialize(worldMaterial2, Vector3.zero);
        container.GenerateMesh(new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2)));
        container.UploadMesh();

        blockNumber++;
    }

    public void DestroyContainer(Transform voxel)
    {
        
        //Transform[] holder = gameObject.GetComponentsInChildren<Transform>();
        Destroy(voxel.gameObject);
        Debug.Log("Destoryed block " + voxel.name);
        blockNumber--;

    }
}
