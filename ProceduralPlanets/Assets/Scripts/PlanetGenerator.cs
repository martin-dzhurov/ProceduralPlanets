using UnityEngine;
using Shapes;

[RequireComponent(typeof(MeshFilter))]
public class PlanetGenerator : MonoBehaviour
{
    [SerializeField]
    private ShapeSettings shapeSettings;
    [SerializeField]
    private NoiseSettings noiseSettings;

    private Mesh _mesh;
    private Shape _shape;
    private NoiseFilter _noiseFilter;

    private void Update()
    {
        UpdateShape();
        UpdateNoise();
    }

    private void UpdateShape()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        _shape = GetBaseShape(shapeSettings.baseShape);

        _mesh.Clear();
        _mesh.vertices = _shape.Vertaces;
        _mesh.triangles = _shape.Triangles;

        _mesh.RecalculateNormals();

    }

    private void UpdateNoise()
    {
        _noiseFilter = new NoiseFilter(noiseSettings);

        Vector3[] verticesOnSphere = _mesh.vertices;
        for (int i = 0; i < _mesh.vertexCount; i++)
        {
            verticesOnSphere[i].Normalize();

            float elevation = _noiseFilter.Evaluate(verticesOnSphere[i]);
            elevation = shapeSettings.Size * (1 + elevation);
            verticesOnSphere[i] = verticesOnSphere[i] * elevation;
        }
        _mesh.vertices = verticesOnSphere;
        _mesh.RecalculateNormals();
    }

    private Shape GetBaseShape(BaseShape baseShape)
    {
        switch (baseShape)
        {
            case BaseShape.Tetrahedron:
                return new Tetrahedron(shapeSettings.Resolution);
            case BaseShape.Octahedron:
                return new Octahedron(shapeSettings.Resolution);
            case BaseShape.Icosahedron:
                return new Icosahedron(shapeSettings.Resolution);
            default:
                return new Icosahedron(shapeSettings.Resolution);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_mesh != null && _mesh.vertices != null)
        {
            foreach (Vector3 vertex in _mesh.vertices)
            {
                Gizmos.DrawSphere(vertex, 0.05f);
            }
        }

        Gizmos.DrawLine(transform.position * 10, transform.position * -10);
    }
}
