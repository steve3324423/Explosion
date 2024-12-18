using System;
using UnityEngine;

public class CubeTouched : MonoBehaviour
{
    public event Action<Transform> ClickedCube;

    private void OnMouseDown()
    {
        ClickedCube?.Invoke(transform);
        Destroy(gameObject);
    }
}
