using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class LevelBuilder : MonoBehaviour, IObservable
{
    [SerializeField] private float Scatter;
    [SerializeField] private int CountUnits;
    [SerializeField] private float SpawnRate = 1f;
    private FactoryUnits _factory;
    private float _leftPoint, _rightPoint, _topPoint;
    private GameModel _gameModel;
    //private IEnumerator _coroutine;

    private void Awake()
    {
        _gameModel = ServiceLocator.ServiceLocator.Resolve<GameModel>();
        _gameModel.AddObserver(this);
    }

    private void Start()
    {
        _rightPoint = _gameModel.RightBorder - Scatter;
        _topPoint = _gameModel.TopBorder + Scatter;
        _leftPoint = _rightPoint * -1;
        _gameModel.SetCountUnits(CountUnits);
        StartCoroutine(NextUnit());
    }

    IEnumerator NextUnit()
    {
        for (var i = 0; i < CountUnits; i++)
        {
            var RandomPosition = new Vector3(UnityEngine.Random.Range(_leftPoint, _rightPoint), _topPoint, 0f);
            _factory.GetUnit((FactoryUnits.TypeUnit) UnityEngine.Random.Range(1,3), RandomPosition);
            yield return new WaitForSeconds(SpawnRate);
        }
    }

    public void OnObjectChanged(IObserver observer)
    {
        if (_gameModel.GameEnd != GameModel.GameEndResult.NotEnded)
        {
            StopCoroutine(NextUnit());
        }
    }
}
