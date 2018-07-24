using UPPY.Logic.Classes;

namespace UPPY.Tools
{
    public class GrouppedTechRoutes
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {

            return $"{Name} записей: {Count}";
        }

        public object[] Tag { get; set; }
    }

    public class BadHierarchicalData
    {
        public int IdCollection { get; set; }

        public int IdRecord { get; set; }

        public string Name{get; set;}

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}