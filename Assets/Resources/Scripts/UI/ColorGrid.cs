using UnityEngine;
using System.Collections;

public class ColorGrid : SwatchColour{

    public ColorGrid()
    {
        base.init();

        _swatchColors[0, 0] = new Color(0, 0, 0);
    }

}
