using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public GameObject playerAppearance;
    public GameObject playerObject;
    public LevelLoader levelLoader;
    public GameObject tutorialText;
    public GameObject combatUI;
    public GameObject playerWonScreen;
    public GameObject playerLostScreen;
    public int combatLevel;
    public bool isTutorial;
    public int maxEnemies;
    public List<GameObject> enemiesToChooseFrom;
    public List<GameObject> activeEnemies;
    public List<Transform> enemySpawns;
    public List<Enemy> enemyStats;
    public List<ItemSO> totalLoot;
    public int totalXP;
    public bool allDead;

    void Start()
    {
        allDead = false;
        combatUI.SetActive(true);
        playerWonScreen.SetActive(false);
        playerLostScreen.SetActive(false);
        playerAppearance.GetComponent<SpriteRenderer>().sprite = Manager.instance.headshot;
        combatLevel = Manager.instance.combatLevel;

        if (!isTutorial)
        {
            maxEnemies = Random.Range(1, 4);
        }
        else
        {
            maxEnemies = 1;
        }

        for (int i = 0; i < maxEnemies; i++)
        {
            activeEnemies.Add(Instantiate(enemiesToChooseFrom[Random.Range(0, enemiesToChooseFrom.Count)], enemySpawns[i].position, Quaternion.identity));
        }

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            enemyStats.Add(activeEnemies[i].GetComponent<Enemy>());
            totalLoot.AddRange(activeEnemies[i].GetComponent<Enemy>().lootTable);
            activeEnemies[i].GetComponent<Enemy>().level = combatLevel;
            totalXP += (activeEnemies[i].GetComponent<Enemy>().xp * combatLevel);
        }

        if (tutorialText != null)
        {
            if (Manager.instance.combatTutComplete == false)
            {
                tutorialText.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                tutorialText.SetActive(false);
            }
        }
        else { return; }
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1f;
        Destroy(tutorialText);
        Manager.instance.combatTutComplete = true;
        AudioManager.instance.Play("GeneralButtonClick");
    }

    void Update()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].activeInHierarchy == false)
            {
                allDead = true;
            }
            else
            {
                allDead = false;
            }
        }

        if (allDead)
        {
            StartCoroutine(CombatOver());
        }

        if (playerObject.activeInHierarchy == false)
        {
            StartCoroutine(PlayerDied());
        }
    }

    public void LoadMap(string mapName)
    {
        AudioManager.instance.Play("GeneralButtonClick");
        playerWonScreen.SetActive(false);
        playerLostScreen.SetActive(false);
        combatUI.SetActive(false);
        levelLoader.LoadLevel(mapName);
    }

    public void Retry()
    {
        AudioManager.instance.Play("GeneralButtonClick");
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.name);
    }
    
    IEnumerator CombatOver()
    {
        yield return new WaitForSeconds(0.25f);
        playerWonScreen.SetActive(true);

        for (int i = 0; i < totalLoot.Count; i++)
        {
            Manager.instance.lootToAdd.AddRange(totalLoot);
            totalLoot = new List<ItemSO>();
            Manager.instance.currentXP += totalXP;
        }

        Manager.instance.CheckProgress(Manager.instance.currentNodeID);
    }

    IEnumerator PlayerDied()
    {
        yield return new WaitForSeconds(0.25f);
        playerLostScreen.SetActive(true);
    }
}
