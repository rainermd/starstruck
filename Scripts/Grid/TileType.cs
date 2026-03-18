using UnityEngine;

namespace starstruckGrid
{
    [CreateAssetMenu(menuName = "Grid/Tile Type")]
    public class TileType : ScriptableObject
    {
        [Header("Visual")]
        public Sprite sprite;

        [Header("Core")]
        public bool isWalkable = true;
        public int movementCost = 1;
        public int height = 0;

        [Header("Gameplay")]
        public bool providesCover = false;
        public int damageOnTurnStart = 0;
        public int damageOnTurnEnd = 0;

        [Header("Special")]
        public bool isConsole = false;
    }
}
