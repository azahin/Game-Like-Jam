using NUnit.Framework;
using UnityEngine;

public struct Voxel
{
    public GameObject triggerBlock;

    public byte Id;
    public bool multi1Active;
    public Vector3 currentPos;

    public bool Solid
    {
        get
        {
            return true;
        }
    }


    public void MultiBlock1()
    {
        Vector3[] ScanRadius = new Vector3[4];
        ScanRadius[0] = new Vector3(2, 0, 0);
        ScanRadius[1] = new Vector3(0, 0, 2);
        ScanRadius[2] = new Vector3(-2, 0, 0);
        ScanRadius[3] = new Vector3(0, 0, -2);

        for (int i = 0; i < 4; i++)
        {
            GameObject temp = triggerBlock;
            temp.transform.position = ScanRadius[i] + currentPos;
            temp.SetActive(true);

            if (temp.GetComponent<MultiCheck>().Collide)
            {
                multi1Active = true;
            }
            
        }
        
    }
}
