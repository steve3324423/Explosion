using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    private byte _opacityValue = 0;
    private byte _randomValueColor;

    private void Awake()
    {
        int minColorValue = 0;
        int maxColorValue = 255;

        _randomValueColor = (byte)Random.Range(minColorValue, maxColorValue);
        GetComponent<Renderer>().material.color = new Color32(_randomValueColor, _randomValueColor, _randomValueColor, _opacityValue);
    }
}
