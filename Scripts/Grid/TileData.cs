using UnityEngine;

namespace starstruckGrid
{
    public class TileData : MonoBehaviour
    {
        [Header("Grid")]
        public Vector3Int gridLocation;

        [Header("Definition")]
        public TileType tileType;

        [Header("Overrides")]
        public int heightOverride = -1;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Vector3Int position, TileType type)
        {
            gridLocation = position;
            SetTileType(type);
        }

        public void SetTileType(TileType newType)
        {
            tileType = newType;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (spriteRenderer != null && tileType != null && tileType.sprite != null)
            {
                spriteRenderer.sprite = tileType.sprite;
            }
        }

        // ========================
        // Core Properties
        // ========================

        public bool IsWalkable => tileType != null && tileType.isWalkable;

        public int MovementCost => tileType != null ? tileType.movementCost : 1;

        public int Height
        {
            get
            {
                if (heightOverride >= 0)
                    return heightOverride;

                return tileType != null ? tileType.height : gridLocation.z;
            }
        }

        // ========================
        // Gameplay
        // ========================

        public int DamageOnTurnStart => tileType != null ? tileType.damageOnTurnStart : 0;

        public int DamageOnTurnEnd => tileType != null ? tileType.damageOnTurnEnd : 0;

        public bool ProvidesCover => tileType != null && tileType.providesCover;

        public bool IsConsole => tileType != null && tileType.isConsole;

        // ========================
        // Traversal
        // ========================

        public bool IsTraversableFrom(TileData fromTile, int maxStepHeight)
        {
            if (!IsWalkable) return false;

            int heightDiff = Mathf.Abs(Height - fromTile.Height);
            return heightDiff <= maxStepHeight;
        }
    }
}
