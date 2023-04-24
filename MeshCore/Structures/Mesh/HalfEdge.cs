using System.Numerics;
using Mathematics.Vectors;

namespace MeshCore.Structures.Mesh;

public class HalfEdge<NumberType, VectorType> where NumberType : struct, INumber<NumberType>
                                             where VectorType : struct, IVector3<NumberType, VectorType> {

    public Vertex<NumberType, VectorType> EndVertex;
    public Vertex<NumberType, VectorType> StartVertex;

    public HalfEdge<NumberType, VectorType> NextEdge { get; set; }
    public HalfEdge<NumberType, VectorType> PreviousEdge { get; set; }
    public HalfEdge<NumberType, VectorType> OppositeEdge { get; set; }

    public VectorType Vector => EndVertex.Position - StartVertex.Position;
}
