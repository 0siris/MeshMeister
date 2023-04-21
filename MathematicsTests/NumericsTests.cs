using Xunit;
using Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics.Tests; 

public class NumericsTests {
    [Fact()]
    public void CalculateMachineFloatEpsilonTest() {
        var calculateMachineFloatEpsilon = Numerics.CalculateMachineFloatEpsilon();
        Assert.True(Numerics.FloatEpsilon >= calculateMachineFloatEpsilon);
    }

    [Fact()]
    public void CalculateMachineDoubleEpsilonTest() {
        var machineDoubleEpsilon = Numerics.CalculateMachineDoubleEpsilon();
        Assert.True(Numerics.FloatEpsilon >= machineDoubleEpsilon);
    }
}