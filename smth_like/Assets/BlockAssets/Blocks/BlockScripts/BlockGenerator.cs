using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    public bool isActive = false;

    private int tick = 0;
    private void Update()
    {
        if (tick > 120)
        {
            MultiBlock1();
            tick = 0;
        }
        

        tick++;
    }

    public void MultiBlock1() //Move to a block generator class
    {
        Vector3[] ScanRadius = new Vector3[4];
        ScanRadius[0] = new Vector3(1, 0, 0);
        ScanRadius[1] = new Vector3(0, 0, 1);
        ScanRadius[2] = new Vector3(-1, 0, 0);
        ScanRadius[3] = new Vector3(0, 0, -1);



       
        RaycastHit blockFound;

        for (int i = 0; i < 4; i++)
        {
            bool holder;
           
            holder = Physics.Raycast(gameObject.transform.position, ScanRadius[i], out blockFound, 2);
            //Debug.Log("Distance: " + blockFound.distance);
           
            if (holder && (blockFound.transform.GetComponent<Block>().blockId == 1 && blockFound.distance >= 1.5)) //There has to be a better way to do this
            {

                gameObject.transform.GetComponent<Block>().CreateBlock(blockFound, 2);

            }

        }

        
    }
}
