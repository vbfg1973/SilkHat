using FluentAssertions;

namespace SilkHat.Domain.Common.Tests
{
    public class PathUtilitiesTests
    {
        [Theory]
        [MemberData(nameof(PathData))]
        public void Given_Paths_Correct_Common_Parent(PathDataObject pathDataObject)
        {
            string actualCommonParent = PathUtilities.CommonParent(pathDataObject.Paths);

            actualCommonParent.Should().Be(pathDataObject.Expected);
        }

        [Theory]
        [InlineData("name.sln", "name")]
        [InlineData("thing.jpg", "thing")]
        public void Given_FileName_Extension_Is_Removed(string fileName, string expected)
        {
            string actual = PathUtilities.RemoveExtension(fileName);

            actual.Should().Be(expected);
        }
        
        public static IEnumerable<object[]> PathData()
        {
            yield return new object[]
            {
                new PathDataObject
                {
                    Paths = new List<string>
                    {
                        @"C:\Data\test",
                        @"C:\Data\test1",
                        @"C:\Data\test2",
                        @"C:\Data\test3"
                    },
                    Expected = @"C:\Data"
                }
            };
            
            yield return new object[]
            {
                new PathDataObject
                {
                    Paths = new List<string>
                    {
                        @"C:\Data1\test",
                        @"C:\Data2\test1",
                        @"C:\Data3\test2",
                        @"C:\Data4\test3"
                    },
                    Expected = @"C:"
                }
            };
        }

        public class PathDataObject
        {
            public List<string> Paths { get; set; }
            public string Expected { get; set; }
        }
    }
}