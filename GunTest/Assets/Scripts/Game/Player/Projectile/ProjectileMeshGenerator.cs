using UnityEngine;

namespace Game.Player
{
    public class ProjectileMeshGenerator
    {
        private Vector3[] _vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
        };
        
        private int[] _triangles = new int[]
        {
            0, 2, 1,
            0, 3, 2,
            4, 5, 6,
            4, 6, 7,
            0, 1, 5,
            0, 5, 4,
            1, 2, 6,
            1, 6, 5,
            2, 3, 7,
            2, 7, 6,
            3, 0, 4,
            3, 4, 7,
        };
        
        public void UpdateMesh(Mesh mesh, float scale, float randomMagnitude)
        {
            mesh.Clear();

            var vertices = new Vector3[_vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = _vertices[i] * scale + Random.onUnitSphere * randomMagnitude;
            }
            
            mesh.vertices = vertices;
            mesh.triangles = _triangles;
        }
    }
}