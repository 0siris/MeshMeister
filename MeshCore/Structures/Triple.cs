using System.Collections;

namespace MeshCore.Structures;

public record struct Triple<T>(T V0, T V1, T V2) : IEnumerable<T> {
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<T> GetEnumerator() {
        yield return V0;
        yield return V1;
        yield return V2;
    }
}

