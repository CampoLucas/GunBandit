using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class ChangeLightColor : MonoBehaviour
{
    private Light2D _light;
    [SerializeField] private Vector2 intencity = Vector2.one;
    [SerializeField] private Color colorA;
    [SerializeField] private Color colorB;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    public void ChangeColor()
    {
        _light.intensity = Random.Range(intencity.x, intencity.y);
        _light.color = new Color(Random.Range(colorA.r, colorB.r), Random.Range(colorA.g, colorB.g),
            Random.Range(colorA.b, colorB.b));
    }
}
