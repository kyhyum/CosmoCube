using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/CubeTile")]
public class CubeTile : Tile
{

    public GameObject destroyObj;
    public CubeColor color;
    public virtual bool IsBomb => false;
}