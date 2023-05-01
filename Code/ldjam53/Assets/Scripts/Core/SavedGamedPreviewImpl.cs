using System;

using GameFrame.Core.SavedGames;

public class SavedGamedPreviewImpl : SavedGamePreview<Assets.Scripts.Core.GameState>
{
    public String Name { get; set; }
    public String CreatedOn { get; set; }
    public String SavedOn { get; set; }
    public String CompletedLevelsCount { get; set; }
    public String CurrentLevel { get; set; }
    public String OwlType { get; set; }

    public override void Init(Assets.Scripts.Core.GameState savedGame, string key)
    {
        base.Init(savedGame, key);

        CreatedOn = String.Format("{0:G}", savedGame.CreatedOn);
        Name = savedGame.CurrentScene;
        SavedOn = String.Format("{0:G}", savedGame.SavedOn);
        CompletedLevelsCount = savedGame.CompletedLevels.Count.ToString();
        CurrentLevel = GetCurrentLevel(savedGame);
        OwlType = savedGame.SelectedOwlType;
    }

    private String GetCurrentLevel(Assets.Scripts.Core.GameState savedGame)
    {
        if (savedGame.CurrentLevel != default)
        {
            if (savedGame.GameMode.LevelsByID.TryGetValue(savedGame.CurrentLevel.ID, out var levelDefinition))
            {
                return levelDefinition.Name;
            }
        }

        return "World";
    }
}
