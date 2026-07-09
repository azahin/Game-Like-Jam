using NUnit.Framework;
using TMPro;
using UnityEditor.Experimental.GraphView;
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

    public byte getID
    {
        get
        {
            return Id;
        }
    }

    


}
