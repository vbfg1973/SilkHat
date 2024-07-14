using System.Reflection;

namespace SilkHat.Domain.Graph.Support
{
    /// <summary>
    ///     A reference to the domain assembly
    /// </summary>
    public sealed class DomainGraphAssemblyReference
    {
        /// <summary>
        ///     The assembly reference
        /// </summary>
        public static readonly Assembly Assembly = typeof(DomainGraphAssemblyReference).Assembly;
    }
}