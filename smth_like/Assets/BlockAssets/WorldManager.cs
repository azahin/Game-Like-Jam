using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private List<Material> workingMaterial;
    [SerializeField] public GameObject triggerBlock;

    private int tickNumber = 0;

    private Container container;

    public int blockNumber = 0;

    private GameObject blocks;

    List<RaycastHit> factoriesActive = new List<RaycastHit>();

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
        Debug.Log(factoriesActive.Count);
        for (int i = 0; i < factoriesActive.Count && tickNumber == 120; i++)
        {
            FactoryCheck(factoriesActive[i]);

        }
        if (tickNumber == 120)
        {
            tickNumber = 0;
        }
        tickNumber++;
    }

    private void LateUpdate()
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

    public void FactoryCheck(RaycastHit voxelBase)
    {
      
        List<RaycastHit> foundRays = new List<RaycastHit>();
        foundRays = voxelBase.transform.GetComponent<Container>().MultiBlock1(voxelBase); //Get a list of found valid multiblock structres to produce blocks in
        if (foundRays.Count > 0)
        {
            for (int i = 0; i < foundRays.Count; i++)
            {
                if (foundRays[i].transform.GetComponent<Container>().activeBlock.Id == 1)
                {
                    CreateContainer(foundRays[i], 2);
                }
               

            }
            foreach (RaycastHit hit in factoriesActive) //Make sure the same source block doesn't get added to the tick update multiple times. Ask abrar for help
            {
                if (hit.transform != voxelBase.transform)
                {
                    factoriesActive.Add(voxelBase);
                }
            }
            if (factoriesActive.Count <= 0)
            {
                factoriesActive.Add(voxelBase);
            }
        }
        else { factoriesActive.Remove(voxelBase); }

        
    }
}
