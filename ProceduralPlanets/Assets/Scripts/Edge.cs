using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public int[] vertexIndices;

    public Edge(int[] vertexIndices)
    {
        this.vertexIndices = vertexIndices;
    }

    public int StartVertexIndex
    {
        get { return vertexIndices[0]; }
    }

    public int EndVertexIndex
    {
        get { return vertexIndices[vertexIndices.Length - 1]; }
    }

    public Edge Reverse()
    {
        int[] vertexIndices = new int[this.vertexIndices.Length];
        for (int i = 0; i < this.vertexIndices.Length; i++)
        {
            vertexIndices[i] = this.vertexIndices[this.vertexIndices.Length - 1 - i];
        }
        return new Edge(vertexIndices);
    }
}
