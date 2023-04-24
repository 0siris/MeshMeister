using System.Numerics;
using System.Runtime.Intrinsics;
using Mathematics;
using Mathematics.Vectors;

namespace MeshCore.Structures.Mesh; 

public class Triangle<NumberType, VectorType> where NumberType : struct, INumber<NumberType> where VectorType : struct, IVector3<NumberType, VectorType> {
    public int Index { get; set; }

    private VectorType? normal;
    public Triple<HalfEdge<NumberType,VectorType>> Edges { get; private set; }

    public Triple<Vertex<NumberType, VectorType>> Vertices => new(Edges.V0.StartVertex, Edges.V1.StartVertex, Edges.V2.StartVertex);

    public Vertex<NumberType, VectorType> V0 => Edges.V0.StartVertex;
    public Vertex<NumberType, VectorType> V1 => Edges.V1.StartVertex;
    public Vertex<NumberType, VectorType> V2 => Edges.V2.StartVertex;

    public VectorType V0Pos => Edges.V0.StartVertex.Position;
    public VectorType V1Pos => Edges.V2.StartVertex.Position;
    public VectorType V2Pos => Edges.V2.StartVertex.Position;

    public HalfEdge<NumberType,VectorType> E0 => Edges.V0;
    public HalfEdge<NumberType,VectorType> E1 => Edges.V1;
    public HalfEdge<NumberType,VectorType> E2 => Edges.V2;

    public VectorType Normal => normal ??= E0.Vector.Cross(E1.Vector).Normalized();

    public NumberType Area() => E0.Vector.Cross(E1.Vector).LengthSquared() * NumberType.CreateTruncating(0.5);

    public VectorType MassCenter() => (V0Pos + V1Pos + V2Pos) / NumberType.CreateTruncating(3);
}
