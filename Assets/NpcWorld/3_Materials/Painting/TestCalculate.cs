using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TestCalculate : MonoBehaviour
{
    public Texture2D _texture;
    public Color color;
    public Color32 testColor;
    Renderer _renderer;
    public float percent;

    public int totalPixels;
    public float redPixels;
    public float greenPixels;
    public float bluePixels;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        if (_renderer != null)
        {
            _texture = (Texture2D)_renderer.material.GetTexture("_BaseMap") ;
        }

    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (_texture != null)
            {
                testColor = AverageColorFromTexture(_texture);
            }
        }
    }

    Color32 AverageColorFromTexture(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        totalPixels = texColors.Length;
        redPixels = 0;
        greenPixels = 0;
        bluePixels = 0;

        for (int i = 0; i < totalPixels; i++)
        {
            redPixels += texColors[i].r;
            greenPixels += texColors[i].g;
            bluePixels += texColors[i].b;
        }

        percent = redPixels / totalPixels*100;
        return new Color32((byte)(redPixels / totalPixels), (byte)(greenPixels / totalPixels), (byte)(bluePixels / totalPixels), 0);

    }
}
