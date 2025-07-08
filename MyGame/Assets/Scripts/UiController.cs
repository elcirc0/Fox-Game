using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject SettingsPanel;

    public bool PlayerWinOrLoseOrOpenSettings { get; private set; }

    public Slider SettingsSlider1;
    public Slider SettingsSlider2;

    public int _timer;

    public static UiController instance;

    private bool _pauseMenuOn = false;
    private bool WinMenu = false;
    private bool LoseMenu = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SettingsSlider1.value = SaveData.SettingsSlider1;
        SettingsSlider2.value = SaveData.SettingsSlider2;
        PlayerWinOrLoseOrOpenSettings = false;
        InvokeRepeating(nameof(TimerUpdate), 0, 1f);
    }

    public void TimerUpdate()
    {
        _timer += 1;
        _textMeshPro.text = _timer.ToString();
    }

    public void OnOfPauseMenu()
    {
        _pauseMenuOn = !_pauseMenuOn;

        PauseMenuPanel.SetActive(_pauseMenuOn);
    }

    public void OnWinPanel()
    {
        WinMenu = true;
        AudioManager.Instance.PlaySFX("Win");
        int ActiveSceneIndex = GameManager.Instance.ActiveSceneIndex;

        WinPanel.SetActive(true);
        PlayerWinOrLoseOrOpenSettings = true;
        Debug.Log(PlayerWinOrLoseOrOpenSettings);

        GameManager.Instance.PauseUnPauseGame();

        SaveData data = new();

        data.LevelList[ActiveSceneIndex - 1].LevelCompletionTime = _timer;

        SaveSystem.Save();
    }

    public void OnLosePanel()
    {
        LoseMenu = true; 
        AudioManager.Instance.PlaySFX("Lose");
        LosePanel.SetActive(true);
        PlayerWinOrLoseOrOpenSettings = true;
        GameManager.Instance.PauseUnPauseGame();
    }

    public void OnSettings()
    {
        SettingsPanel.SetActive(true);
        PlayerWinOrLoseOrOpenSettings = true;
        PauseMenuPanel.SetActive(false);
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    public void OffSettingsAndSave()
    {
        SettingsPanel.SetActive(false);
        SaveData.SettingsSlider1 = instance.SettingsSlider1.value;
        SaveData.SettingsSlider2 = instance.SettingsSlider2.value;
        //AudioManager.Instance.musicSource = ;
        if (LoseMenu == true)
            LosePanel.SetActive(true);
        if (WinMenu == true)
            WinPanel.SetActive(true);
        if (_pauseMenuOn == true)
            PauseMenuPanel.SetActive(_pauseMenuOn);
    }
}
