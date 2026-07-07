using UnityEngine;

public struct Voxel
{
    public byte Id;

    public bool Solid
    {
        get
        {
            return Id != 0;
        }
    }

}
