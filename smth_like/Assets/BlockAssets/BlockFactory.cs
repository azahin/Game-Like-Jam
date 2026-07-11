using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockFactory : MonoBehaviour
{
    [SerializeField] private List<Material> workingMaterial;
    [SerializeField] private Transform map;

 

    public int blockNumber = 0;

   
 
    public void BlockCreation(int mat, Vector3 pos, int blockId)
    {
        GameObject newBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newBlock.transform.name = "Block " + blockNumber;
        newBlock.transform.localPosition = pos;
        newBlock.AddComponent<Block>();
        newBlock.GetComponent<Block>().blockId = blockId;
        newBlock.GetComponent<Renderer>().material = workingMaterial[mat]; //Replace with just a main texture later
        newBlock.transform.parent = map;

        if (blockId == 0) //Remove later w/ better method
        {
            newBlock.AddComponent<BlockGenerator>();
        }
    }
    
    

    
}
