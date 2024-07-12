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
            
            yield return new object[]
            {
                new PathDataObject
                {
                    Paths = new List<string>
                    {
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Config\SqlDatabaseConfiguration.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Migrations\20230511162934_InitialMigration.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Migrations\20230511162934_InitialMigration.Designer.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Migrations\ClaimsAreUsContextModelSnapshot.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Models\Claim.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Models\ClaimType.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Models\Company.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\Support\DataAssemblyReference.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\TypeConfigurations\ClaimTypeConfiguration.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\TypeConfigurations\ClaimTypeTypeConfiguration.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\TypeConfigurations\CompanyTypeConfiguration.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\obj\Debug\net7.0\ClaimsAreUs.Data.GlobalUsings.g.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\obj\Debug\net7.0\.NETCoreApp,Version=v7.0.AssemblyAttributes.cs",
                        @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data\obj\Debug\net7.0\ClaimsAreUs.Data.AssemblyInfo.cs"
                    },
                    Expected = @"C:\Users\44780\RiderProjects\ClaimsAreUs\src\ClaimsAreUs.Data"
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