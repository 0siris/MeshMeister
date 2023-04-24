using Mathematics.Vectors;
using System.Numerics;

namespace MeshCore.Structures.Mesh;

public class TriangleMesh<NumberType, VectorType> where VectorType : struct, IVector3<NumberType, VectorType>
                                                  where NumberType : struct, INumber<NumberType> {

    private readonly ISet<HalfEdge<NumberType, VectorType>> edges = new HashSet<HalfEdge<NumberType, VectorType>>();
    private readonly IList<Vertex<NumberType, VectorType>> vertices = new List<Vertex<NumberType, VectorType>>();
    private readonly IList<Triangle<NumberType, VectorType>> triangles = new List<Triangle<NumberType, VectorType>>();

    public IReadOnlyList<Vertex<NumberType, VectorType>> Vertices => vertices.AsReadOnly();
    public IReadOnlyList<Triangle<NumberType, VectorType>> Triangles => triangles.AsReadOnly();
}
