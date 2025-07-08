using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> Panels = new();

    [SerializeField] private TMPro.TextMeshProUGUI TextTimer1;
    [SerializeField] private TMPro.TextMeshProUGUI TextTimer2;

    private bool Active1 = false;
    private bool Active2 = false;
    private bool Active3 = false;
    public void GameExit()
    {
        Application.Quit();
    }
    public void HowPlayPanel()
    {
        Active1 = !Active1;
        Active2 = false;
        Active3 = false;
        Panels[0].gameObject.SetActive(Active1);
        Panels[1].gameObject.SetActive(Active2);
        Panels[2].gameObject.SetActive(Active3);
    }
    public void PlayPanel()
    {
        Active2 = !Active2;
        Active1 = false;
        Active3 = false;
        Panels[0].gameObject.SetActive(Active1);
        Panels[1].gameObject.SetActive(Active2);
        Panels[2].gameObject.SetActive(Active3);

    }
    public void SettingsPanel()
    {
        Active3 = !Active3;
        Active1 = false;
        Active2 = false;
        Panels[0].gameObject.SetActive(Active1);
        Panels[1].gameObject.SetActive(Active2);
        Panels[2].gameObject.SetActive(Active3);
    }

    public void StartLevel_1()
    {
        SceneManager.LoadScene(1);
    }

    public void StartLevel_2()
    {
        SceneManager.LoadScene(2);
    }

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        SaveData data = SaveSystem.LoadData();

        if (data.LevelList[0] == null)  
            Debug.Log("Время прохождения уровня 1 не сохранено");            
        else 
            TextTimer1.text = $"Время прохождения уровня:{data.LevelList[0].LevelCompletionTime} секунд";

        if (data.LevelList[1] == null)
            Debug.Log("Время прохождения уровня 2 не сохранено");
        else
            TextTimer2.text = $"Время прохождения уровня:{data.LevelList[1].LevelCompletionTime} секунд";
    }

}
