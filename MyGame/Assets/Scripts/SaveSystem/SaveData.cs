
[System.Serializable]
public class SaveData
{
    private int _levelCompletionTime;

    public static float SettingsSlider1 = 1f;
    public static float SettingsSlider2 = 1f;
    public int LevelCompletionTime
    {
        get { return _levelCompletionTime; }
        set { _levelCompletionTime = value; }
    }

    private bool accessLevel;

    public SaveData(int levelCompletionTime, bool accessLevel)
    {
        this._levelCompletionTime = levelCompletionTime;
        this.accessLevel = accessLevel;
    }

    public SaveData()
    {
        LevelCompletionTime = 0;
        accessLevel = false;
    }


    public static SaveData Level1 = new(0, true);
    public static SaveData Level2 = new();

    public SaveData[] LevelList = { Level1, Level2 };

}
