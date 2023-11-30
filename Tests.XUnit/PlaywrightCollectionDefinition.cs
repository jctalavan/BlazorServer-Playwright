namespace Tests.xUnit
{
    /// <summary>
    /// PlaywrightCollection name that is used in the Collection attribute on each test classes.
    /// Like "[Collection(PlaywrightFixture.PlaywrightCollection)]"
    /// </summary>
    [CollectionDefinition(Constantes.Collections.PlaywrightCollection)]
    public class PlaywrightCollectionDefinition : ICollectionFixture<PlaywrightFixture>
    {
    }
}
