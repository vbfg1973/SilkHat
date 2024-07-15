using FluentAssertions;
using SilkHat.Domain.Common.Locations;

namespace SilkHat.Domain.Common.Tests.Locations
{
    public class LocationTests
    {
        [Theory]
        [MemberData(nameof(A_OverlapsEntirely_B))]
        [MemberData(nameof(A_OverlapsStartOf_B))]
        [MemberData(nameof(A_OverlapsEndOf_B))]
        public void Given_Locations_Overlap(Location a, Location b)
        {
            a.Overlaps(b).Should().BeTrue();
        }
        
        [Theory]
        [MemberData(nameof(A_OverlapsStartOf_B))]
        public void Given_Locations_OverlapStart(Location a, Location b)
        {
            a.OverlapsStart(b).Should().BeTrue();
        }
        
        [Theory]
        [MemberData(nameof(A_OverlapsEndOf_B))]
        public void Given_Locations_DoesNot_OverlapStart(Location a, Location b)
        {
            a.OverlapsStart(b).Should().BeFalse();
        }
        
        [Theory]
        [MemberData(nameof(A_OverlapsEndOf_B))]
        public void Given_Locations_OverlapEnd(Location a, Location b)
        {
            a.OverlapsEnd(b).Should().BeTrue();
        }
        
        [Theory]
        [MemberData(nameof(A_OverlapsStartOf_B))]
        public void Given_Locations_DoesNot_OverlapEnd(Location a, Location b)
        {
            a.OverlapsEnd(b).Should().BeFalse();
        }

        public static IEnumerable<object[]> A_OverlapsEndOf_B()
        {
            yield return new[]
            {
                new Location(new LocationPosition(10, 10), new LocationPosition(20, 20)),
                new Location(new LocationPosition(0, 0), new LocationPosition(15, 15))
            };
        }

        public static IEnumerable<object[]> A_OverlapsStartOf_B()
        {
            yield return new[]
            {
                new Location(new LocationPosition(0, 0), new LocationPosition(10, 10)),
                new Location(new LocationPosition(1, 1), new LocationPosition(15, 15))
            };
        }

        public static IEnumerable<object[]> A_OverlapsEntirely_B()
        {
            yield return new[]
            {
                new Location(new LocationPosition(0, 0), new LocationPosition(10, 10)),
                new Location(new LocationPosition(1, 1), new LocationPosition(5, 5))
            };
        }
    }
}