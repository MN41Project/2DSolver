using System;

namespace DSolver
{
    public abstract class ElementAttributes
    {
        public static double YoungModulus = 1000000;
        public static double Diameter = 0.015;

        public static double Section = Math.PI * Diameter * Diameter / 4;
    }
}

