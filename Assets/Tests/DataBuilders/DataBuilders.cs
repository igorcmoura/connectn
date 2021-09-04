using Tests.DataBuilders.Boards;
using Tests.DataBuilders.Disks;

namespace Tests.DataBuilders
{
    public static class A
    {
        public static BoardBuilder Board => new BoardBuilder();
        public static DiskBuilder Disk => new DiskBuilder();
        public static MatchBuilder Match => new MatchBuilder();
        public static DiskPlacementControllerBuilder DiskPlacementController => new DiskPlacementControllerBuilder();
        public static PlayerBuilder Player => new PlayerBuilder();
    }

    public static class An
    {
        public static IBoardBuilder IBoard => new IBoardBuilder();
        public static IDiskBuilder IDisk => new IDiskBuilder();
    }

    public abstract class DataBuilder<T>
    {
        public abstract T Build();

        public static implicit operator T(DataBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}
