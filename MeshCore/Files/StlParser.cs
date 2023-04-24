using Mathematics.Vectors;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Mathematics.Matrix;
using Mathematics.Quaternion;
using MeshCore.Structures.Mesh;

namespace MeshCore.Files;

public class StlParser{
    public const int IndexNormal = 0;
    public const int IndexV1 = 1;
    public const int IndexV2 = 2;
    public const int IndexV3 = 3;


    /// <summary>
    /// The 80byte header of the stl file is saved here.
    /// </summary>
    public string? Header { get; private set; }

    /// <summary>
    ///     Layout Vector[n,i] with i=
    ///     <list type="number">
    ///         <item>Normal <see cref="IndexNormal" /></item>
    ///         <item>A <see cref="IndexV1" /></item>
    ///         <item>B <see cref="IndexV2" /></item>
    ///         <item>C <see cref="IndexV3" /></item>
    ///     </list>
    /// </summary>
    public Vector3F[,]? Triangles { get; private set; }

    private bool adjustModelCenter = true;
    private bool? composeTransform;

    private bool invertNormals;
    private int parsingAttempts;

    private string? path;
    private Quaternion rotation = Quaternion.Identity;
    private float scale = 1f;

    private Stream? stream;

    private AffineTransform<float, Vector4F, QuaternionF> transform = AffineTransform<float, Vector4F, QuaternionF>.Identity;
    private Vector3F translate = default;


    private bool ParseStlFile() {
        var fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
            return false;


        if (!fileInfo.Extension.ToUpperInvariant().Equals(".STL", StringComparison.InvariantCultureIgnoreCase) )
            return false;


        stream = fileInfo.OpenRead();

        if (!stream.CanRead)
            return false;

        return ParseStlStream();
    }

    private bool ParseStlStream() {
        bool result;
        try {
            result = ParseBinaryStream();
        } catch (Exception) {
            try {
                Clear();
                stream!.Position = 0;
                result = ParseAsciiStream();
            } catch (Exception e) {
                Clear();
                return false;
            }
        }

        if (result)
            AdjustCenter();
        return result;
    }


    private void Clear() => Triangles = null;

    private bool ParseBinaryStream() {
        // in case we try to run multiple times on the stream we reset the position
        stream!.Flush();
        stream!.Position = 0;

        //UINT8[80] - HEADER
        var buffer = new byte[80];
        stream!.Read(buffer, 0, 80);
        Header = Encoding.ASCII.GetString(buffer).TrimEnd('\0');

        var firstSpace = Header.IndexOf(' ', StringComparison.InvariantCulture);
        if (firstSpace > 0 && Header[..firstSpace].ToUpperInvariant()
                                                  .Equals("SOLID", StringComparison.InvariantCulture))
            throw new InvalidOperationException("STL file is a ascii file!");


        //UINT32 - Triangle count
        stream!.Read(buffer, 0, 4);
        var triangleCount = BitConverter.ToInt32(buffer, 0);

        //TRIANGLE -- LITTLE ENDIAN
        //REAL32[3] - Normalvector 
        //REAL32[3] - Vertex 1      
        //REAL32[3] - Vertex 2
        //REAL32[3] - Vertex 3
        //UINT16    - Attribute byte count            
        var totalBytes = triangleCount * (4 * 3 * 4 + 2);
        Triangles = new Vector3F[triangleCount, 4]; //Layout: NORMAL,X,Y,Z,
        buffer = new byte[totalBytes];

        stream.Read(buffer, 0, totalBytes);


        Parallel.For(0L,
                     triangleCount,
                     i => {
                         var offset = ((int) i) * 50;
                         ReadNormal(buffer, offset, ref Triangles[i, IndexNormal]);
                         ReadVertex(buffer, offset + 12, ref Triangles[i, IndexV1]);
                         ReadVertex(buffer, offset + 24, ref Triangles[i, IndexV2]);
                         ReadVertex(buffer, offset + 36, ref Triangles[i, IndexV3]);
                     });

        //we don't need to close the stream here, instead the dispose
        //pattern will close the stream but only if we created it


        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ReadVertex(byte[] buffer, int pos, ref Vector3F vector) {
        vector.X = BitConverter.ToSingle(buffer, pos);
        vector.Y = BitConverter.ToSingle(buffer, pos + 4);
        vector.Z = BitConverter.ToSingle(buffer, pos + 8);
        vector = vector.TransformCoordinate(transform.ToMatrix4X4().AsFloat());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ReadNormal(byte[] buffer, int pos, ref Vector3F normal) {
        normal.X = BitConverter.ToSingle(buffer, pos);
        normal.Y = BitConverter.ToSingle(buffer, pos + 4);
        normal.Z = BitConverter.ToSingle(buffer, pos + 8);
        normal = normal.TransformNormal(transform.ToMatrix4X4().AsFloat());
        normal.Normalize();

        if (invertNormals)
            normal = -normal;
    }

    private bool ParseAsciiStream() {
        var reader = new StreamReader(stream!, Encoding.ASCII);
        var triangles = new LinkedList<Vector3[]>();

        var normal = Vector3F.Zero;
        IList<Vector3> vertices = new List<Vector3>(3);

        while (!reader.EndOfStream) {
            var line = reader.ReadLine();
            if (string.IsNullOrEmpty(line))
                continue;

            line = line.Trim();
            if (line.Length == 0)
                continue;

            var token = NextToken(line);

            switch (token.first.ToUpperInvariant()) {
                case "SOLID":
                    Header = token.rest;
                    break;

                case "FACET":
                    vertices.Clear();
                    var normalString = token.rest.Split(' ');
                    
                    if (normalString[0].ToUpperInvariant().Equals("NORMAL", StringComparison.InvariantCulture) ) {
                        normal = new Vector3F {
                            X = float.Parse(normalString[1], CultureInfo.InvariantCulture),
                            Y = float.Parse(normalString[2], CultureInfo.InvariantCulture),
                            Z = float.Parse(normalString[3], CultureInfo.InvariantCulture)
                        };
                        normal = normal.TransformNormal(transform.ToMatrix4X4().AsFloat());
                        normal.Normalize();
                    }

                    break;

                case "VERTEX":
                    var vertexString = token.rest.Trim().Split(' ');
                    Debug.Assert(vertexString.Length == 3);

                    var vector = new Vector3F {
                        X = float.Parse(vertexString[0], CultureInfo.InvariantCulture),
                        Y = float.Parse(vertexString[1], CultureInfo.InvariantCulture),
                        Z = float.Parse(vertexString[2], CultureInfo.InvariantCulture)
                    };
                    vector.TransformCoordinate(transform.ToMatrix4X4().AsFloat());
                    vertices.Add(vector);
                    break;

                case "ENDFACET":
                    var triangle = new Vector3[vertices.Count + 1];
                    triangle[IndexNormal] = normal;
                    for (var i = 0; i < vertices.Count; i++)
                        triangle[i + 1] = vertices[i];
                    triangles.AddLast(triangle);
                    break;
            }
        }


        reader.Close();
        if (triangles.Count == 0)
            return false;

        Triangles = new Vector3F[triangles.Count, 4];
        var totalCount = triangles.Count;
        for (var i = 0; i < totalCount; i++) {
            var vector3 = triangles.First!.Value;
            triangles.RemoveFirst();

            Triangles[i, IndexNormal] = vector3[IndexNormal];
            Triangles[i, IndexV1] = vector3[IndexV1];
            Triangles[i, IndexV2] = vector3[IndexV2];
            Triangles[i, IndexV3] = vector3[IndexV3];
        }

        return true;
    }

    private static (string first, string rest) NextToken(string txt) {
        var idx = txt.IndexOf(' ', StringComparison.InvariantCulture);
        return idx > 0 ? (txt[..idx], txt[(idx + 1)..]) : (txt, string.Empty);
    }

    private void AdjustCenter() {
        Center = Positions().Centroid();

        if (!adjustModelCenter)
            return;

        for (var i = 0; i < FaceCount; i++) {
            Triangles![i, IndexV1] = Triangles[i, IndexV1] - Center;
            Triangles![i, IndexV2] = Triangles[i, IndexV2] - Center;
            Triangles![i, IndexV3] = Triangles[i, IndexV3] - Center;
        }
    }


    public int FaceCount => Triangles?.GetLength(0) ?? 0;

    public int VertexCount => FaceCount * 3;


    /// <summary>
    ///     Calculates the model center and shifts all vertices to the origin. In order to get unmodified position the origin
    ///     need to be added to the vertex position. This behavior is by default enabled.
    ///     <code>
    ///     OriginalVertex = Vertex + Origin
    /// </code>
    /// </summary>
    public StlParser AdjustModelCenter(bool adjustCenter = true) {
        adjustModelCenter = adjustCenter;
        return this;
    }

    /// <summary>
    /// Checks the mesh for degenerate triangles. Checks for collapsed triangles that correspond to a line or triangles that are too small. 
    /// </summary>
    /// <returns>true if degenerated triangles are found</returns>
    public bool IsDegenerated() {
        for (var i = 0; i < FaceCount; i++) {
            //var n = Triangles[i, 0];
            var v1 = Triangles![i, 1];
            var v2 = Triangles![i, 2];
            var v3 = Triangles![i, 3];

            if (v1.Equals(v2) || v1.Equals(v3) || v2.Equals(v3))
                return true;

            v1 = Vector3.Normalize(v1);
            v2 = Vector3.Normalize(v2);
            v3 = Vector3.Normalize(v3);

            var alpha = Vector3.Dot(v1, v2);
            var beta = Vector3.Dot(v2, v3);
            var gamma = Vector3.Dot(v3, v1);

            if (alpha is < -1 or > 1 ||
                beta is < -1 or > 1 ||
                gamma is < -1 or > 1)
                return true;
        }

        return false;
    }

    public IEnumerable<string> SupportedExtensions { get; } = new[] {"STL"};

    public bool IsSuccessfullyParsed { get; private set; }


    public IEnumerable<Vector3F> Positions() {
        if (Triangles is null)
            throw new InvalidOperationException("No positions available. Parse file first.");

        var length = Triangles.GetLength(0);
        for (var i = 0; i < length; i++) {
            yield return Triangles[i, IndexV1];
            yield return Triangles[i, IndexV2];
            yield return Triangles[i, IndexV3];
        }
    }

    public Vector3 Center { get; private set; }

    public StlParser Scale(float scale) {
        if (composeTransform.HasValue && !composeTransform.Value) //composeTransform == false?
            throw new InvalidOperationException($"{nameof(Scale)}/{nameof(Rotation)}/{nameof(Translate)} function cannot be mixed with {nameof(Transform)}");
        composeTransform = true;

        this.scale = scale;
        return this;
    }

    public StlParser Rotation(Quaternion rotation) {
        if (composeTransform.HasValue && !composeTransform.Value) //composeTransform == false?
            throw new InvalidOperationException($"{nameof(Scale)}/{nameof(Rotation)}/{nameof(Translate)} function cannot be mixed with {nameof(Transform)}");
        composeTransform = true;

        this.rotation = rotation;
        return this;
    }

    public StlParser Translate(Vector3 translate) {
        if (composeTransform.HasValue && !composeTransform.Value) //composeTransform == false?
            throw new InvalidOperationException($"{nameof(Scale)}/{nameof(Rotation)}/{nameof(Translate)} function cannot be mixed with {nameof(Transform)}");
        composeTransform = true;

        this.translate = translate;
        return this;
    }

    public StlParser Transform(AffineTransform<float, Vector4F, QuaternionF> transform) {
        if (composeTransform.HasValue && composeTransform.Value) //composeTransform == true?
            throw new InvalidOperationException($"{nameof(Scale)}/{nameof(Rotation)}/{nameof(Translate)} function cannot be mixed with {nameof(Transform)}");
        composeTransform = false;

        this.transform = transform;
        return this;
    }

    public StlParser ModelPath(string path) {
        if (stream is not null)
            throw new InvalidOperationException($"{nameof(ModelPath)} and {nameof(Stream)} can not be combined.");

        this.path = path;
        return this;
    }

    public StlParser Stream(Stream stream) {
        if (path is not null)
            throw new InvalidOperationException($"{nameof(ModelPath)} and {nameof(Stream)} can not be combined.");

        this.stream = stream;
        return this;
    }

    public StlParser InvertNormals(bool invert = false) {
        invertNormals = invert;
        return this;
    }

    public bool ParseModel() {
        if (path is null && stream is null)
            throw new InvalidOperationException($"Configure either a {nameof(Path)} or set a stream to use {nameof(Stream)}");

        parsingAttempts++;
        try {
            return IsSuccessfullyParsed = !string.IsNullOrEmpty(path) ? ParseStlFile() : ParseStlStream();
        } catch (Exception e) {
            Clear();
            return IsSuccessfullyParsed = false;
        }
    }

    public StlParser ParseModel(out bool successful) {
        successful = ParseModel();
        return this;
    }

    public TriangleMesh<NumberType, VectorType> GetMesh<NumberType, VectorType>()
        where VectorType : struct, IVector3<NumberType, VectorType> 
        where NumberType : struct, INumber<NumberType> {

        if (!IsSuccessfullyParsed && parsingAttempts == 0)
            ParseModel();

        if (Triangles is null) {
            return null;
        }


        var triangleMesh = new TriangleMesh<NumberType, VectorType>{};
        triangleMesh.SetTriangles(Triangles);
        
        if(UpdateMeshAsync)
            triangleMesh.UpdateAsync();

        return triangleMesh;
    }

    public void Dispose() {
        //dispose the stream only when was created by this class
        //if path was set, then also a stream was opened
        if (path is null) return;
        stream?.Dispose();
        stream = null;
    }

}
