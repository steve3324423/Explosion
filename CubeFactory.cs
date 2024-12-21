using System;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    private float _yPosition = 1f;

    public CubeTouched Create(CubeTouched cubePrefab,Transform transformCube)
    {
        Vector3 randomPosition = new Vector3(transformCube.position.x, _yPosition, transformCube.position.z);
        return Instantiate(cubePrefab, randomPosition, Quaternion.identity);
    }
}
