namespace SilkHat.Verbs.Abstract
{
    public interface IVerb
    {
        Task Run<T>(T options) where T : IOptions;
    }
}