using System;
using UnityEngine;

namespace Shapes
{
    public class Tetrahedron : Shape
    {
        public Tetrahedron(int resolution) : base(resolution) { }

        protected override Vector3[] baseVertacies => new Vector3[]
        {
            new Vector3(1, 0, -1/MathF.Sqrt(2)),
            new Vector3(-1, 0, -1/MathF.Sqrt(2)),
            new Vector3(0, 1, 1/MathF.Sqrt(2)),
            new Vector3(0, -1, 1/MathF.Sqrt(2))
        };

        protected override int[] baseTriangles => new int[]
        {
            0, 1, 2,
            0, 3, 1,
            0, 2, 3,
            1, 3, 2
        };

    }
}

