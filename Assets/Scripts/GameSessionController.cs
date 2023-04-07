using UnityEngine;
using TMPro;

public class GameSessionController : MonoBehaviour
{
    public static GameSessionController instance;

    [Header("UI Elements To Alter")]
    [SerializeField] GameObject hudToTurnOff;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    [Header("Enemy Logic Component References")]
    [SerializeField] GameObject enemyRootParent;
    [SerializeField] TextMeshProUGUI enemiesFixedDisplay;
    [SerializeField] AudioClip winSFX;

    private int enemiesInScene, enemiesFixed = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        EnemyController[] enemies = enemyRootParent.GetComponentsInChildren<EnemyController>();

        foreach (EnemyController enemy in enemies)
            enemiesInScene++;

        UpdateDisplay();

        hudToTurnOff.SetActive(true);

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void AddToFixedEnemies()
    {
        enemiesFixed++;
        UpdateDisplay();

        if (enemiesFixed >= enemiesInScene)
        {
            hudToTurnOff.SetActive(false);

            GetComponent<AudioSource>().PlayOneShot(winSFX);

            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
    }

    public void LoseGame()
    {
        hudToTurnOff.SetActive(false);

        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    private void UpdateDisplay()
    {
        if (enemiesFixed < 10 && enemiesInScene < 10)
            enemiesFixedDisplay.text = string.Format("0{0}/0{1}", enemiesFixed, enemiesInScene);
        else if (enemiesFixed < 10 && enemiesInScene >= 10)
            enemiesFixedDisplay.text = string.Format("0{0}/ {1}", enemiesFixed, enemiesInScene);
        else if (enemiesFixed >= 10 && enemiesInScene >= 10)
            enemiesFixedDisplay.text = string.Format("{0}/ {1}", enemiesFixed, enemiesInScene);
    }
}
