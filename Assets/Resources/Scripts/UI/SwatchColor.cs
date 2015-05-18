using UnityEngine;
using System.Collections;

public class SwatchColour {
    public const int Darkness6 = 0;
    public const int Darkness5 = 5;
    public const int Darkness4 = 4;
    public const int Darkness3 = 3;
    public const int Darkness2 = 2;
    public const int Darkness1 = 1;

    protected Color[,] _swatchColors;

    public void init()
    {
        _swatchColors = new Color[10, 6];

        _swatchColors[0, 0] = new Color(255, 255, 255);
        _swatchColors[0, 1] = new Color(242, 242, 242);
        _swatchColors[0, 2] = new Color(216, 216, 216);
        _swatchColors[0, 3] = new Color(191, 191, 191);
        _swatchColors[0, 4] = new Color(165, 165, 165);
        _swatchColors[0, 5] = new Color(127, 127, 127);

        _swatchColors[1, 0] = new Color(0, 0, 0);
        _swatchColors[1, 1] = new Color(127, 127, 127);
        _swatchColors[1, 2] = new Color(89, 89, 89);
        _swatchColors[1, 3] = new Color(63, 63, 63);
        _swatchColors[1, 4] = new Color(38, 38, 38);
        _swatchColors[1, 5] = new Color(12, 12, 12);
    }

    public Color GetColor(int ColorIndex, int DarknessLevel)
    {
        if(ColorIndex >= _swatchColors.GetLength(0))
        {
            Debug.LogError("Color Index out of range.");
            return Color.white;
        }

        if (ColorIndex >= _swatchColors.GetLength(0))
        {
            Debug.LogError("Color Darkness out of range.");
            return Color.white;
        }

        return _swatchColors[ColorIndex, DarknessLevel];
    }
}
