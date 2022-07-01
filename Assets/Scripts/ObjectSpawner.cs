using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Coin _spawningObject = default;
    [SerializeField] private bool _isSpawning = false;
    [SerializeField] [Range(0.01f, 10f)] private float _spawnRate = 2f;

    private readonly List<Transform> _spawnPoints = new List<Transform>();
    
    private WaitForSeconds _waitTime = default;
    private WaitUntil _waitIsSpawning = default;
    private Coroutine _spawnObjects = default;
    private float _spawnRateCashe = default;
    private int _maxCoinsCount = default;
    private int _coinSpawned = default;

    private bool IsInactive => _spawnObjects is null; 

    private void Awake()
    {
        SetSpawnPoints();
    }

    private void Start()
    {
        _spawnRateCashe = _spawnRate;
        _waitTime = new WaitForSeconds(_spawnRate);
        _waitIsSpawning = new WaitUntil(() => _isSpawning);
        _maxCoinsCount = transform.childCount;
        BeginSpawning();
    }

    private void OnValidate()
    {
        if (Mathf.Approximately(_spawnRateCashe, _spawnRate)) 
            return;
        
        _spawnRateCashe = _spawnRate;
        _waitTime = new WaitForSeconds(_spawnRate);
    }

    private void BeginSpawning()
    {
        _isSpawning = true;
        _spawnObjects ??= StartCoroutine(SpawnObject());
    }

    private void StopSpawning()
    {
        if (IsInactive) 
            return;
        
        StopCoroutine(_spawnObjects);
        _isSpawning = false;
        _spawnObjects = null;
    }

    private void Pause()
    {
        if (IsInactive) 
            return;
        
        _isSpawning = false;
    }

    private void Resume()
    {
        if (IsInactive) 
            return;

        _isSpawning = true;
    }
    
    private void SetSpawnPoints()
    {
        foreach (Transform spawnPoint in transform)
        {
            _spawnPoints.Add(spawnPoint);
        }
    }

    private void SpawnObject(Vector2 position)
    {
        _coinSpawned++;
        Instantiate(_spawningObject, position, Quaternion.identity);
        
        if (_coinSpawned >= _maxCoinsCount)
            Pause();
    }
    
    private IEnumerator SpawnObject()
    {
        if (_spawnPoints.Count == 0) 
            throw new Exception("Не заданы точки спавна");
        
        IEnumerator spawnPointsEnumerator = _spawnPoints.GetEnumerator();
        
        while (_isSpawning)
        {
            if (spawnPointsEnumerator.MoveNext() == false)
            {
                spawnPointsEnumerator.Reset();
                spawnPointsEnumerator.MoveNext();
            }
                
            Transform spawnPoint = (Transform) spawnPointsEnumerator.Current;

            if (spawnPoint is null) 
                throw new NullReferenceException();

            SpawnObject(spawnPoint.position);
            yield return _waitTime;
            yield return _waitIsSpawning;
        }
    }
}
