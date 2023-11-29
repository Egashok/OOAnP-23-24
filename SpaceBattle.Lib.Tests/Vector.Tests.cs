namespace SpaceBattle.Lib.Tests;

public class VectorTests
{
    [Fact]
    public void VectorGetHashCode()
    {
        var vec1 = new Vector(new int[] { 1, 2 });
        var vec2 = new Vector(new int[] { 1, 2 });
        Assert.False(vec1.GetHashCode() == vec2.GetHashCode());
    }
    [Fact]
    public void VectorSet()
    {
        var vec1 = new Vector(new int[] { 1, 2 });
        vec1[0] = 2;
        var vec2 = new Vector(new int[] { 2, 2 });
        Assert.Equal(vec1, vec2);
    }
    [Fact]
    public void VectorEqual()
    {
        var vec1 = new Vector(new int[] { 1, 2 });
        var vec2 = new Vector(new int[] { 3, 4 });
        Assert.False(vec1.Equals(vec2));
    }
    [Fact]
    public void VectorNoneEqual()
    {
        var vec1 = new Vector(new int[] { 1, 2 });
        var vec2 = new Vector(new int[] { 1, 2 });
        Assert.False(vec1 != vec2);
    }

    [Fact]
    public void VectorEqualExceptSize()
    {
        var vec1 = new Vector(new int[] { 1, 2, 3 });
        var vec2 = new Vector(new int[] { 1, 2 });
        Assert.Throws<ArgumentException>(() => vec1 == vec2);
    }

    [Fact]
    public void VectorEqualNoneVector()
    {
        var obj = 0;
        var vec = new Vector(new int[] { 1, 2 });
        Assert.False(vec.Equals(obj));
    }

    [Fact]
    public void VectorPlusExcept()
    {
        var vec1 = new Vector(new int[] { 1, 2, 3 });
        var vec2 = new Vector(new int[] { 1, 2 });
        Assert.Throws<ArgumentException>(() => vec1 + vec2);
    }
}
