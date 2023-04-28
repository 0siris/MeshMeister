using System.Numerics;
using Mathematics.Vectors;

namespace MeshCore.Structures.Mesh;

public class Vertex<Number, VecType> where VecType : struct, IVector3<Number, VecType>
                                     where Number : struct, INumber<Number> {
    public ICollection<HalfEdge<Number, VecType>> InEdges = new List<HalfEdge<Number, VecType>>();
    public ICollection<HalfEdge<Number, VecType>> OutEdges = new List<HalfEdge<Number, VecType>>();
    private VecType position;

    public int Index { get; set; } = -1;

    public VecType Position {
        get => position;
        set {
            position = value;
            PositionChanchged?.Invoke(this);
        }
    }


    public VecType Normal { get; set; }

    public event Action<Vertex<Number, VecType>>? PositionChanchged;
}
