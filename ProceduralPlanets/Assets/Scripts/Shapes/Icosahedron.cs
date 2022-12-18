using System;
using UnityEngine;

namespace Shapes
{
    public class Icosahedron : Shape
    {
        public Icosahedron(int resolution) : base(resolution) { }

        private static readonly float goldenRatio = (1 + MathF.Sqrt(5)) / 2;

        protected override Vector3[] baseVertacies => new Vector3[]
        {
            new Vector3(1, goldenRatio, 0),     //0
            new Vector3(-1, goldenRatio, 0),    //1
            new Vector3(1, -goldenRatio, 0),    //2
            new Vector3(-1, -goldenRatio, 0),   //3
            new Vector3(0, 1, goldenRatio),     //4
            new Vector3(0, -1, goldenRatio),    //5
            new Vector3(0, 1, -goldenRatio),    //6
            new Vector3(0, -1, -goldenRatio),   //7
            new Vector3(goldenRatio, 0, 1),     //8
            new Vector3(goldenRatio, 0, -1),    //9
            new Vector3(-goldenRatio, 0, 1),    //10
            new Vector3(-goldenRatio, 0, -1),   //11
        };

        protected override int[] baseTriangles => new int[]
        {
            0, 1, 4,
            0, 4, 8,
            0, 8, 9,
            0, 9, 6,
            0, 6, 1,
            3, 2, 5,
            3, 5, 10,
            3, 10, 11,
            3, 11, 7,
            3, 7, 2,

            1, 11, 10,
            1, 10, 4,
            4, 10, 5,
            4, 5, 8,
            8, 5, 2,
            8, 2, 9,
            9, 2, 7,
            9, 7, 6,
            6, 7, 11,
            6, 11, 1
        };
    }
}

