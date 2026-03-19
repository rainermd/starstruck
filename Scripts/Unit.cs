using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace starstruckGrid
{
    public class Unit : MonoBehaviour
    {
        [Header("Grid Position")]
        public Vector3Int gridPosition;

        [Header("Stats")]
        public int movementRange = 5;
        public float moveSpeed = 5f;

        private void Start()
        {
            SnapToGrid();
            RegisterOnGrid();
        }

        void SnapToGrid()
        {
            transform.position = GridManager.Instance.GridToWorld(gridPosition);
        }

        void RegisterOnGrid()
        {
            GridManager.Instance.grid[gridPosition].occupyingUnit = this;
        }

        public void SetGridPosition(Vector3Int pos)
        {
            // Clear old tile
            GridManager.Instance.grid[gridPosition].occupyingUnit = null;

            gridPosition = pos;
            transform.position = GridManager.Instance.GridToWorld(pos);

            // Occupy new tile
            GridManager.Instance.grid[gridPosition].occupyingUnit = this;
        }

        public void MoveAlongPath(List<Vector3Int> path)
        {
            StartCoroutine(MoveCoroutine(path));
        }

        private IEnumerator MoveCoroutine(List<Vector3Int> path)
        {
            GridManager.Instance.grid[gridPosition].occupyingUnit = null;

            foreach (var step in path)
            {
                Vector3 target = GridManager.Instance.GridToWorld(step);

                while (Vector3.Distance(transform.position, target) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        target,
                        moveSpeed * Time.deltaTime
                    );
                    yield return null;
                }

                gridPosition = step;
            }

            GridManager.Instance.grid[gridPosition].occupyingUnit = this;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Only run in editor, not play mode
            if (!Application.isPlaying && GridManager.Instance != null)
            {
                gridPosition = Vector3Int.RoundToInt(transform.position);
            }
        }
#endif
    }
}
