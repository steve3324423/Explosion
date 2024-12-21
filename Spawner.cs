using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeTouched _touchedCube;
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeTouched _cubePrefab;

    private List<CubeTouched> _cubes;
    private int _chanceOfSeparation = 100;
    private int _valueDivision = 2;

    public event Action<List<CubeTouched>> CreatedCubes;

    private void Awake()
    {
        _cubes = new List<CubeTouched>();
    }

    private void OnEnable()
    {
        _touchedCube.ClickedCube += OnClickedCube;
        SubscribeEvents();
    }

    private void OnDisable()
    {
        foreach (CubeTouched touchedCube in _cubes)
            touchedCube.ClickedCube -= OnClickedCube;

        _touchedCube.ClickedCube -= OnClickedCube;
    }

    private void SubscribeEvents()
    {
        foreach (CubeTouched touchedCube in _cubes)
            touchedCube.ClickedCube += OnClickedCube;
    }

    private void SetScale(CubeTouched newCube,Transform transformCube)
    {
        int minChangeValue = 25;
        if (_chanceOfSeparation > minChangeValue)
            newCube.transform.localScale = transformCube.localScale / _valueDivision;
        else
            Destroy(newCube);
    }

    private void SetNewCube(CubeTouched newCube,Transform transformCube)
    {
        SetScale(newCube,transformCube);
        ReportAboutCubes();
    }

    private void ReportAboutCubes()
    {
        CreatedCubes?.Invoke(_cubes);
        SubscribeEvents();
    }

    private void OnClickedCube(Transform transformCube)
    {
        int minValue = 2;
        int maxValue = 6;
        int randomCount = UnityEngine.Random.Range(minValue, maxValue);

        for (int i = 0; i < randomCount; i++)
        {
            CubeTouched cube = _cubeFactory.Create(_cubePrefab,transformCube);
            _cubes.Add(cube);

            SetNewCube(cube, transformCube);
        }

        _chanceOfSeparation = _chanceOfSeparation / _valueDivision;
    }
}
