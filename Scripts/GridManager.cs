using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace starstruckGrid
{
    public class GridManager : MonoBehaviour
    {
        private static GridManager _instance;
        public static GridManager Instance => _instance;

        [Header("Prefabs")]
        public GameObject tilePrefab;
        public Transform tileContainer;

        [Header("Settings")]
        public int maxStepHeight = 1;

        public Dictionary<Vector2Int, TileData> map;

        private static readonly Vector2Int[] Directions =
        {
            new(1, 0),
            new(-1, 0),
            new(0, 1),
            new(0, -1)
        };

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Start()
        {
            BuildMap();
        }

        private void BuildMap()
        {
            var tileMaps = GetComponentsInChildren<Tilemap>()
                .OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);

            map = new Dictionary<Vector2Int, TileData>();

            foreach (var tm in tileMaps)
            {
                var renderer = tm.GetComponent<TilemapRenderer>();
                BoundsInt bounds = tm.cellBounds;

                foreach (var pos in bounds.allPositionsWithin)
                {
                    TileBase baseTile = tm.GetTile(pos);
                    if (baseTile == null) continue;

                    if (baseTile is not CustomTile customTile) continue;

                    Vector2Int key = new(pos.x, pos.y);

                    // Only keep top-most tile
                    if (map.ContainsKey(key)) continue;

                    var tileGO = Instantiate(tilePrefab, tileContainer);
                    var tile = tileGO.GetComponent<TileData>();

                    Vector3 worldPos = tm.GetCellCenterWorld(pos);

                    tile.transform.position = new Vector3(
                        worldPos.x,
                        worldPos.y,
                        worldPos.z + 1
                    );

                    tile.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder;

                    tile.Initialize(pos, customTile.tileType);

                    map.Add(key, tile);
                }
            }
        }

        public List<TileData> GetNeighbors(Vector2Int origin)
        {
            var neighbors = new List<TileData>();

            if (!map.ContainsKey(origin))
                return neighbors;

            var originTile = map[origin];

            foreach (var dir in Directions)
            {
                TryAddNeighbor(originTile, origin + dir, neighbors);
            }

            return neighbors;
        }

        private void TryAddNeighbor(TileData originTile, Vector2Int pos, List<TileData> neighbors)
        {
            if (!map.TryGetValue(pos, out var neighbor))
                return;

            if (!neighbor.IsTraversableFrom(originTile, maxStepHeight))
                return;

            neighbors.Add(neighbor);
        }
    }
}
