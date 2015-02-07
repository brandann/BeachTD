﻿using UnityEngine;
using System.Collections;

namespace PC2D
{
    public class Input
    {

        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
        public const string JUMP = "Jump";
        public const string DASH = "Fire1";
    }

    public delegate void Notification();

    public class Globals
    {
        // Input threshold in order to take effect. Arbitarily set.
        public const float INPUT_THRESHOLD = 0.1f;
    }
}
