namespace KnifeHitTest
{
    /// <summary>
    /// The IStageSettings interface.
    /// </summary>
    public interface IStageSettings
    {
        ILogSettings LogSettings { get; }
        int Knives { get; }
    }
}
