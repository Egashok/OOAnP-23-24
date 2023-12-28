namespace SpaceBattle.Lib;
using Hwdtech;

public class CollisionTreeBuilder : ICollisionTreeBuild
{
    private static IEnumerable<IEnumerable<int>> ReadArrayFile(string dataPath)
    {
        return File.ReadAllLines(dataPath).Select(line => line.Split().Select(int.Parse));
    }

    private static void BuildTree(IEnumerable<IEnumerable<int>> dataFile)
    {
        dataFile.ToList().ForEach(line =>
        {
            var node = IoC.Resolve<IDictionary<int, object>>("Collision.CollisionTree");
            line.ToList().ForEach(n =>
            {
                node.TryAdd(n, new Dictionary<int, object>());
                node = (Dictionary<int, object>)node[n];
            });
        });
    }
    public void BuildArrayFile(string path) => BuildTree(ReadArrayFile(path));
}
