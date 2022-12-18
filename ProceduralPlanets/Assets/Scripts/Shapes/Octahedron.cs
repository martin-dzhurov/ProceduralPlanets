using UnityEngine;

namespace Shapes
{
    public class Octahedron : Shape
    {
        public Octahedron(int resolution) : base(resolution) { }

        protected override Vector3[] baseVertacies => new Vector3[]
        {
            Vector3.right, Vector3.forward, Vector3.left, Vector3.back, Vector3.up, Vector3.down
        };

        protected override int[] baseTriangles => new int[]
        {
            0, 4, 1,
            0, 3, 4,
            0, 5, 3,
            0, 1, 5,

            2, 1, 4,
            2, 4, 3,
            2, 5, 1,
            2, 3, 5
        };
    }
}
