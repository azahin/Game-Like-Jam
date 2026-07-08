using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private List<Material> workingMaterial;

    private Container container;

    public int blockNumber = 0;

    private GameObject blocks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject baseContainer = new GameObject("BaseContainer");
        baseContainer.transform.parent = transform;
        container = baseContainer.AddComponent<Container>();

        container.Initialize(workingMaterial[0], Vector3.zero);
        container.GenerateMesh(Vector3.zero, 1);
        container.UploadMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateContainer(RaycastHit voxelBase, byte blockId)
    {
        Vector3 newPos = voxelBase.normal + voxelBase.transform.GetComponent<Container>().containerPos; //Get face of where you want to place, add to container position
        GameObject baseContainer = new GameObject("BaseContainer" + blockNumber);
        baseContainer.transform.parent = transform;
        container = baseContainer.AddComponent<Container>();

        container.Initialize(workingMaterial[blockId], newPos); //Block selected is based on keyboard input, thus needs -1 for indexing
        container.GenerateMesh(newPos, blockId);
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
