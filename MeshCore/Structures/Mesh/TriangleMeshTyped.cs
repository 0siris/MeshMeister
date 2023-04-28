using Mathematics.Vectors;
using System.Numerics;

namespace MeshCore.Structures.Mesh;

public sealed class TriangleMesh : TriangleMeshTyped<float, Vector3F> {

}


public class TriangleMeshTyped<NumberType, VectorType> where VectorType : struct, IVector3<NumberType, VectorType>
                                                       where NumberType : struct, INumber<NumberType> {

    protected readonly ISet<HalfEdge<NumberType, VectorType>> edges = new HashSet<HalfEdge<NumberType, VectorType>>();
    protected readonly IList<Vertex<NumberType, VectorType>> vertices = new List<Vertex<NumberType, VectorType>>();
    protected readonly IList<Triangle<NumberType, VectorType>> triangles = new List<Triangle<NumberType, VectorType>>();

    public IReadOnlyList<Vertex<NumberType, VectorType>> Vertices => vertices.AsReadOnly();
    public IReadOnlyList<Triangle<NumberType, VectorType>> Triangles => triangles.AsReadOnly();

    public void NeedVertexIndices() {
        for (var i = 0; i < vertices.Count; i++) 
            vertices[i].Index = i;
    }


}
