using UnityEngine;

public static  class StaticSet
{
    public static readonly Vector3Int[] neighborOffsets = new Vector3Int[]
    {
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0)
    };
}
