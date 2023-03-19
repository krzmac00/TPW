using Program_przyk³adowy;

namespace TestProgram_przykladowy;

public class UnitTestPotega
{
    [Fact]
    public void TestPotegaZero()
    {
        Assert.Equal(1, MainWindow.Potega(1, 0));
        Assert.Equal(1, MainWindow.Potega(0, 0));
        Assert.Equal(1, MainWindow.Potega(999, 0));
    }
    [Fact]
    public void TestDodatniePotegiLiczbCalkowitych()
    {
        Assert.Equal(1, MainWindow.Potega(1, 999999));
        Assert.Equal(1024, MainWindow.Potega(2, 10));
        Assert.Equal(2176782336, MainWindow.Potega(6, 12));
        Assert.Equal(15625, MainWindow.Potega(-5, 6));
        Assert.Equal(-3125, MainWindow.Potega(-5, 5));
    }
    [Fact]
    public void TestUjemnePotegiLiczbCalkowitych()
    {
        Assert.Equal(Convert.ToDecimal(0.125), MainWindow.Potega(2, -3));
        Assert.Equal(Convert.ToDecimal(0.00002143347), MainWindow.Potega(6, -6), 10);
    }
    [Fact]
    public void TestPotegiUlamkow()
    {
        Assert.Equal(Convert.ToDecimal(1.44), MainWindow.Potega(Convert.ToDecimal(1.2), 2));
        Assert.Equal(Convert.ToDecimal(0.0102030405), MainWindow.Potega(Convert.ToDecimal(9.9), -2), 10);
    }
}