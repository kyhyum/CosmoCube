using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/BombCubeTile")]
public class BombCubeTile : CubeTile
{
    public int bombRadius = 1; // 현재는 1(상하좌우 한칸)
    public float oxygenDamage = 10f;

    public override bool IsBomb => true;
}