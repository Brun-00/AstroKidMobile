using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform container;
    [SerializeField] private Transform player;

    [Header("Peças")]
    [SerializeField] private List<PieceBase> levelPieces;
    [SerializeField] private PieceBase firstPiece;

    [Header("Configuração")]
    [SerializeField] private int piecesAhead = 10;

    private Queue<PieceBase> _spawnedPieces = new Queue<PieceBase>();
    private PieceBase _lastSpawnedPiece;

    private void Awake()
    {
        // Create the initial set of level pieces.
        CreateInitialPieces();
    }

    private void Update()
    {
        // Check whether the player has moved past the oldest piece.
        CheckPlayerProgress();
    }

    private void CreateInitialPieces()
    {
        // Clear any previously spawned pieces.
        CleanSpawnedPieces();

        // Fill the level with the configured number of pieces.
        for (int i = 0; i < piecesAhead; i++)
        {
            SpawnNextPiece(i == 0);
        }
    }

    private void CheckPlayerProgress()
    {
        if (player == null)
            return;

        if (_spawnedPieces.Count == 0)
            return;

        PieceBase oldestPiece = _spawnedPieces.Peek();

        // Replace the oldest piece once the player passes it.
        if (HasPlayerPassedPiece(oldestPiece))
        {
            SpawnNextPiece(false);
            RemoveOldestPiece();
        }
    }

    private bool HasPlayerPassedPiece(PieceBase piece)
    {
        if (piece == null || piece.endPoint == null)
            return false;

        Vector3 direction =
            piece.endPoint.position -
            piece.startPoint.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return false;

        direction.Normalize();

        Vector3 toPlayer =
            player.position -
            piece.endPoint.position;

        toPlayer.y = 0f;

        // Check whether the player has crossed the piece's endpoint.
        return Vector3.Dot(direction, toPlayer) > 0f;
    }

    private void SpawnNextPiece(bool isFirst)
    {
        if (levelPieces == null || levelPieces.Count == 0)
            return;

        PieceBase prefab;

        // Use the special first piece when spawning the initial section.
        if (isFirst && firstPiece != null)
        {
            prefab = firstPiece;
        }
        else
        {
            // Pick a random piece for the rest of the level.
            prefab = levelPieces[
                Random.Range(0, levelPieces.Count)
            ];
        }

        PieceBase spawnedPiece =
            Instantiate(prefab, container);

        if (_lastSpawnedPiece != null)
        {
            // Align the new piece with the end of the previous piece.
            Vector3 offset =
                spawnedPiece.startPoint.position -
                spawnedPiece.transform.position;

            spawnedPiece.transform.position =
                _lastSpawnedPiece.endPoint.position -
                offset;
        }
        else
        {
            // Place the first piece at the origin.
            spawnedPiece.transform.position =
                Vector3.zero;
        }

        // Add the new piece to the active queue.
        _spawnedPieces.Enqueue(spawnedPiece);

        _lastSpawnedPiece = spawnedPiece;
    }

    private void RemoveOldestPiece()
    {
        if (_spawnedPieces.Count == 0)
            return;

        PieceBase piece =
            _spawnedPieces.Dequeue();

        if (piece != null)
        {
            // Delay destruction to keep the transition smooth.
            Destroy(piece.gameObject, 3f);
        }
    }

    private void CleanSpawnedPieces()
    {
        // Remove all currently tracked level pieces.
        foreach (PieceBase piece in _spawnedPieces)
        {
            if (piece != null)
            {
                Destroy(piece.gameObject);
            }
        }

        _spawnedPieces.Clear();
        _lastSpawnedPiece = null;
    }
}