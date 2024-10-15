namespace Eldergrove.Engine.Core.Data.Game;

public class GameConfig
{

    public string TitleName { get; set; }

    public FontsConfig Fonts { get; set; }
    public MapGeneratorConfig Map { get; set; }

    public PlayerConfig Player { get; set; }


    public SchedulerConfig Scheduler { get; set; }


}
