public static class SceneNames
{
    // Scene Names
    public const string Game = "Game";

    // Enum Mapping
    public enum SceneEnum
    {
        Game,
    }

    // Map Scene Enum to Scene Name
    public static string GetSceneName(SceneEnum sceneEnum)
    {
        return sceneEnum switch
        {
            SceneEnum.Game => Game,
            _ => string.Empty
        };
    }
}
