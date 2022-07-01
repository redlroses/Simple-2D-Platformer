using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> _routePoints = default;
    [SerializeField] private float _speed = 5f;

    private readonly float _distanceOffset = 0.1f;
    
    private IEnumerator _routePointsEnumerator = default;
    private Vector2 _currentRoutePoint = default;
    
    private void Start()
    {
        _routePointsEnumerator = _routePoints.GetEnumerator();
        _currentRoutePoint = GetNextRoutePoint();
    }

    private void Update()
    {
        var direction =  _currentRoutePoint - (Vector2) transform.position;

        if (direction.magnitude < _distanceOffset) 
            _currentRoutePoint = GetNextRoutePoint();
        
        transform.Translate(direction.normalized * (Time.deltaTime * _speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if ( collision.gameObject.TryGetComponent(out PlayerInput playerInput))
           Destroy(collision.gameObject);
    }

    private Vector2 GetNextRoutePoint()
    {
        if (_routePointsEnumerator.MoveNext() == false)
        {
            _routePointsEnumerator.Reset();
            _routePointsEnumerator.MoveNext();
        }

        var nextRoutePoint = (Transform) _routePointsEnumerator.Current;
        
        if (nextRoutePoint is null) 
            throw new NullReferenceException();
        
        return nextRoutePoint.position;
    }
    
}
