using UnityEngine;

public class Block : MonoBehaviour
{
    public int blockId;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBlock(RaycastHit voxelBase, int blockId)
    {
        Vector3 newPos = voxelBase.normal + voxelBase.transform.position; //Get face of where you want to place, add to container position
        gameObject.transform.parent.GetComponent<BlockFactory>().BlockCreation(blockId, newPos, blockId); //First block ID meant to be a material, to overhaul later
  
    }

    public void DestroyBlock(Transform blockDestoryed) //Moce to block class
    {

        //Transform[] holder = gameObject.GetComponentsInChildren<Transform>();
        Destroy(blockDestoryed.gameObject);
        Debug.Log("Destoryed block " + blockDestoryed.name);

    }
}
