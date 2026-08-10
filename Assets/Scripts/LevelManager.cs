using NUnit.Framework;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    [Header("Level Pieces")]
    public List<PieceBase> levelPieces;
    public int piecesNumber = 10;
    public PieceBase firstPiece;
    public PieceBase finalPiece;

    private List<PieceBase> _spawnedPieces = new List<PieceBase>();


    [SerializeField]private int _index;

    private GameObject _currentLevel;

    public ArtManager.ArtType currentArtType;


    private void Awake()
    {
        //SpawnNextLevel();
     
        CreateLevelPieces();
    }
    /*private void SpawnNextLevel()
    {

        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;
            if(_index >= levels.Count)
            {
                _index = 0;
            }
        }
        _currentLevel = Instantiate(levels[_index],container);
        _currentLevel.transform.localPosition = Vector3.zero;    
    }*/

    private void CreateLevelPieces()
    {
        
        CleanSpawnedPieces();

        for (int i = 0; i< piecesNumber; i++)
        {
            CreateLevelPiece(i);
        }

        ColorManager.Instance.ChangeColorByType(currentArtType);

        var setup = ArtManager.Instance.GetSetupByType(currentArtType);

        if (setup != null)
        {
            BackgroundManager.Instance.ChangeSkybox(setup.skybox);
        }
    }

    private void CreateLevelPiece(int index)
    {
        PieceBase piece;

        if (index == 0)
        {
            piece = firstPiece;
        }
        else if (index == piecesNumber - 1)
        {
            piece = finalPiece;
        }
        else
        {
            piece = levelPieces[Random.Range(0, levelPieces.Count)];
        }

        var spawnedPiece = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            Vector3 offset = spawnedPiece.startPoint.position - spawnedPiece.transform.position;
            spawnedPiece.transform.position = lastPiece.endPoint.position - offset;

        }
        else
        {
            spawnedPiece.transform.position = Vector3.zero;
        }

        foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(currentArtType).gameObject);
        }

        _spawnedPieces.Add(spawnedPiece);


    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count - 1; i >= 0 ; i--)
            {
                Destroy(_spawnedPieces[i].gameObject);
            }
            _spawnedPieces.Clear();
    }
}
