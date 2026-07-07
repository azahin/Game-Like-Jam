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

    public void CreateContainer(RaycastHit voxelBase)
    {
        Vector3 newPos = voxelBase.normal + voxelBase.transform.GetComponent<Container>().containerPos; //Get face of where you want to place, add to container position
        GameObject baseContainer = new GameObject("BaseContainer" + blockNumber);
        baseContainer.transform.parent = transform;
        container = baseContainer.AddComponent<Container>();

        container.Initialize(worldMaterial2, newPos);
        container.GenerateMesh(newPos);
        container.UploadMesh();

        blockNumber++;
    }

    public void DestroyContainer(Transform voxelDestoryed)
    {
        
        //Transform[] holder = gameObject.GetComponentsInChildren<Transform>();
        Destroy(voxelDestoryed.gameObject);
        Debug.Log("Destoryed block " + voxelDestoryed.name);
        blockNumber--;

    }
}
