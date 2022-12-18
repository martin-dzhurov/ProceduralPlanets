using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
    public abstract class Shape
    {
        protected abstract Vector3[] baseVertacies { get; }
        protected abstract int[] baseTriangles { get; }

        private List<Vector3> _vertices;
        private List<int> _triangles;
        private List<Edge> _edges;

        private int _resolution;
        private int _numVerticesInEdge;

        public Shape(int resolution)
        {
            _resolution = resolution;
            _numVerticesInEdge = _resolution + 2;

            _vertices = new List<Vector3>();
            _vertices.AddRange(baseVertacies);

            _triangles = new List<int>();
            _edges = new List<Edge>();

            CreateSphere();
        }

        private void CreateSphere()
        {
            for (int i = 0; i < baseTriangles.Length; i += 3)
            {
                Edge[] triangle = new Edge[3];

                for (int j = 0; j < 3; j++)
                {
                    int startIndex = baseTriangles[i + j];
                    int endIndex = baseTriangles[(j == 2) ? i : i + j + 1];

                    Edge edge = GetEdge(startIndex, endIndex);
                    triangle[j] = edge;
                }

                CreateFace(triangle);
            }
        }

        private Edge GetEdge(int startIndex, int endIndex)
        {
            foreach (Edge edge in _edges)
            {
                if (edge.StartVertexIndex == startIndex && edge.EndVertexIndex == endIndex)
                {
                    return edge;
                }
                else if (edge.StartVertexIndex == endIndex && edge.EndVertexIndex == startIndex)
                {
                    return edge.Reverse();
                }
            }

            Edge newEdge = CreateEdge(startIndex, endIndex);
            _edges.Add(newEdge);

            return newEdge;
        }

        private Edge CreateEdge(int startVertexIndex, int endVertexIndex)
        {
            Vector3 startVertex = _vertices[startVertexIndex];
            Vector3 endVertex = _vertices[endVertexIndex];

            int divisions = _resolution;

            int[] edgeVertexIndices = new int[_numVerticesInEdge];

            // The starting vertex of the edge
            edgeVertexIndices[0] = startVertexIndex;

            // The vertices along edge
            for (int divisionIndex = 0; divisionIndex < divisions; divisionIndex++)
            {
                float t = (divisionIndex + 1f) / (divisions + 1f);
                Vector3 innerVertex = Vector3.Lerp(startVertex, endVertex, t);

                _vertices.Add(innerVertex);
                edgeVertexIndices[divisionIndex + 1] = _vertices.Count - 1;
            }

            // The ending vertex of the edge
            edgeVertexIndices[divisions + 1] = endVertexIndex;

            return new Edge(edgeVertexIndices);
        }

        private void CreateFace(Edge[] triangle)
        {
            if (!ValidateTriangle(triangle))
            {
                Debug.LogError("Triangle not defined correctly");
                return;
            }

            int[] vertexMap = GetVertexMap(triangle);
            TriangulateFace(vertexMap);
        }

        private int[] GetVertexMap(Edge[] triangle)
        {
            List<int> vertexMap = new List<int>();

            // Add the top vertex
            vertexMap.Add(triangle[0].StartVertexIndex);

            Edge leftEdge = triangle[2].Reverse();
            Edge rightEdge = triangle[0];
            Edge bottomEdge = triangle[1].Reverse();

            for (int i = 1; i < _numVerticesInEdge - 1; i++)
            {
                // Add the vertex on left edge
                vertexMap.Add(leftEdge.vertexIndices[i]);

                // Add the vertices between the left and right edges
                int numInnerVertices = i - 1;
                for (int j = 0; j < numInnerVertices; j++)
                {
                    Vector3 sideAVertex = _vertices[leftEdge.vertexIndices[i]];
                    Vector3 sideBVertex = _vertices[rightEdge.vertexIndices[i]];

                    float t = (j + 1f) / (numInnerVertices + 1f);
                    Vector3 innerVertex = Vector3.Lerp(sideAVertex, sideBVertex, t);

                    _vertices.Add(innerVertex);
                    vertexMap.Add(_vertices.Count - 1);
                }

                // Add the vertex on right edge
                vertexMap.Add(rightEdge.vertexIndices[i]);
            }

            // Add bottom edge vertices
            vertexMap.AddRange(bottomEdge.vertexIndices);

            return vertexMap.ToArray();
        }

        private void TriangulateFace(int[] vertexMap)
        {
            for (int row = 0; row < _numVerticesInEdge - 1; row++)
            {
                // Vertices along the left edge follow the sequance 0, 1, 3, 6, 10, 15...
                // Calculated with n * (n + 1) / 2
                int topVertex = row * (row + 1) / 2;
                int bottomVertex = (row + 1) * (row + 2) / 2;

                int numTrianglesInRow = row * 2 + 1;

                for (int triangleNr = 0; triangleNr < numTrianglesInRow; triangleNr++)
                {
                    int v0, v1, v2;

                    if (triangleNr % 2 == 0)
                    {
                        // Rightside up triangle
                        v0 = topVertex;
                        v1 = bottomVertex + 1;
                        v2 = bottomVertex;
                    }
                    else
                    {
                        // Upside down triangle
                        v0 = topVertex;
                        v1 = topVertex + 1;
                        v2 = bottomVertex + 1;
                        topVertex++;
                        bottomVertex++;
                    }

                    _triangles.Add(vertexMap[v0]);
                    _triangles.Add(vertexMap[v1]);
                    _triangles.Add(vertexMap[v2]);
                }
            }
        }

        private static bool ValidateTriangle(Edge[] triangle)
        {
            if (triangle.Length != 3 ||
                triangle[0].EndVertexIndex != triangle[1].StartVertexIndex ||
                triangle[1].EndVertexIndex != triangle[2].StartVertexIndex ||
                triangle[2].EndVertexIndex != triangle[0].StartVertexIndex)
            {
                return false;
            }
            return true;
        }

        public Vector3[] Vertaces
        {
            get { return _vertices.ToArray(); }
        }

        public int[] Triangles
        {
            get { return _triangles.ToArray(); }
        }
    }
}
