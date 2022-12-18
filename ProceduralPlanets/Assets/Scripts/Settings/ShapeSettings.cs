using UnityEngine;

public enum BaseShape
{
    Tetrahedron, Octahedron, Icosahedron
}

[System.Serializable]
public class ShapeSettings
{
    public BaseShape baseShape;
    [Range(0, 20)]
    public int Resolution = 5;
    [Range(1, 20)]
    public float Size = 1;
}
