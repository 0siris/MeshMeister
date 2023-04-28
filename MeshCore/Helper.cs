using System.Numerics;
using Mathematics.Vectors;
using MeshCore.Structures.Mesh;

namespace MeshCore; 

public static class Helper {

  
    public static VectorType Centroid<NumberType, VectorType>(this IEnumerable<Vertex<NumberType, VectorType>> vertices)
        where NumberType : struct, INumber<NumberType> where VectorType : struct, IVector3<NumberType, VectorType> {
        var positions = vertices.Select(v => v.Position);

        var center = VectorType.Zero;
        var count = NumberType.Zero;
        foreach (var position in positions) {
            center += position;
            count += NumberType.One;
        }

        center /= count;

        return center;
    }
}
