using UnityEngine;
using UnityEngine.Tilemaps;
// This script generates a CustomTile that does both the Unity Tile/Animated Tile features, while storing TileData/Type, such as fire, cover, etc.
namespace starstruckGrid
{
    [CreateAssetMenu(menuName = "Grid/Custom Tile")]
    public class CustomTile : Tile
    {
        [Header("Custom Tile Type")]
        public TileType tileType;

        [Header("Animation")]
        public Sprite[] animatedSprites;      // Sprites for animation
        public float animationSpeed = 1f;     // Frames per second
        public float animationStartTime = 0f; // Optional offset

        // If this tile has animation, Unity will call this to get animation data
        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            if (animatedSprites != null && animatedSprites.Length > 0)
            {
                tileAnimationData.animatedSprites = animatedSprites;
                tileAnimationData.animationSpeed = animationSpeed;
                tileAnimationData.animationStartTime = animationStartTime;
                return true; // Tile is animated
            }

            return false; // Not animated, will just show base sprite
        }
    }
}
