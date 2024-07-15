namespace SilkHat.Domain.Common.Locations
{
    public record Location(LocationPosition Start, LocationPosition End)
    {
        public bool IsBefore(Location other)
        {
            return End.Character < other.Start.Character;
        }

        public bool IsAfter(Location other)
        {
            return Start.Character > other.End.Character;
        }

        public bool OverlapsStart(Location other)
        {
            return Start.Character < other.Start.Character &&
                   End.Character > other.Start.Character;
        }
        
        public bool OverlapsEnd(Location other)
        {
            return Start.Character < other.End.Character &&
                   End.Character > other.End.Character;
        }

        public bool Overlaps(Location other)
        {
            return OverlapsStart(other) || OverlapsEnd(other);
        }

        public bool ContainedBy(Location other)
        {
            return Start.Character > other.Start.Character && 
                   End.Character < other.End.Character;
        }
        
        public bool Wraps(Location other)
        {
            return Start.Character < other.Start.Character && 
                   End.Character > other.End.Character;
        }
    }

    public record LocationPosition(int Character, int Line);
}