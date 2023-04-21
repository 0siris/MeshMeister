using Xunit;

namespace Mathematics.Tests; 

public class ConstantsTests {
    [Fact]
    public void TestT2() {
        var floatPi = Constants<float>.Pi;
        var doublePi = Constants<double>.Pi;
        var intPi = Constants<int>.Pi;
        var decimalPi = Constants<decimal>.Pi;
    }
}
