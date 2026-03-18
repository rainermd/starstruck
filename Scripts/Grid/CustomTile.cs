using UnityEngine;
using UnityEngine.Tilemaps;

namespace starstruckGrid
{
    [CreateAssetMenu(menuName = "Grid/Custom Tile")]
    public class CustomTile : Tile
    {
        public TileType tileType;
    }
}
