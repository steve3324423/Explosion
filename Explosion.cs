using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private List<CubeTouched> _touchedCube;
    private Vector3 _scaleCube;
    private float _force = 10f;
    private float _radius = 150f;

    private void Awake()
    {
        _scaleCube = Vector3.one;
    }

    private void OnEnable()
    {
        _spawner.CreatedCubes += OnCreatedCubes;
        SubscribeEventsCubes();
    }

    private void OnDisable()
    {
        foreach (CubeTouched touchedCube in _touchedCube)
            touchedCube.ClickedCube -= OnClickedCube;

        _spawner.CreatedCubes -= OnCreatedCubes;
    }

    private void OnCreatedCubes(List<CubeTouched> touchedCubes)
    {
        _touchedCube = touchedCubes;
        SubscribeEventsCubes();
    }

    private void SubscribeEventsCubes()
    {
        foreach (CubeTouched touchedCube in _touchedCube)
            touchedCube.ClickedCube += OnClickedCube;
    }

    private void OnClickedCube(Transform transformCube)
    {
        SetForceAndRadius(transformCube);
        Collider[] cubes = Physics.OverlapSphere(transformCube.position, _radius);

        foreach (var cube in cubes)
        {
            if(cube.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddForce(Vector3.forward * _force, ForceMode.VelocityChange);
        }
    }

    private void SetForceAndRadius(Transform transformCube)
    {
        int valueIncrease = 2;

        _force = transformCube.localScale.x < _scaleCube.x ? _force * valueIncrease : _force;
        _radius = transformCube.localScale.x < _scaleCube.x ? _radius * valueIncrease : _radius;

        _scaleCube = transform.localScale;
    }
}
