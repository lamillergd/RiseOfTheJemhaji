using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject player;
    public LevelLoader levelLoader;
    public GameObject combatUI;
    public GameObject combatOverScreen;
    public bool isTutorial;
    public int maxEnemies;
    public List<GameObject> enemiesToChooseFrom;
    public List<GameObject> activeEnemies;
    public List<Transform> enemySpawns;
    public List<Enemy> enemyStats;
    public List<ItemSO> totalLoot;
    public bool allDead;

    void Start()
    {
        allDead = false;
        combatUI.SetActive(true);
        combatOverScreen.SetActive(false);

        if (!isTutorial)
        {
            maxEnemies = Random.Range(1, 4);
        }
        else
        {
            maxEnemies = 1;
        }

        player = GameObject.Find("Player");

        for (int i = 0; i < maxEnemies; i++)
        {
            activeEnemies.Add(Instantiate(enemiesToChooseFrom[Random.Range(0, enemiesToChooseFrom.Count)], enemySpawns[i].position, Quaternion.identity));
        }

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            enemyStats.Add(activeEnemies[i].GetComponent<Enemy>());
            totalLoot.AddRange(activeEnemies[i].GetComponent<Enemy>().lootTable);
        }
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
    }

    public void LoadMap(string mapName)
    {
        combatOverScreen.SetActive(false);
        combatUI.SetActive(false);
        levelLoader.LoadLevel(mapName);
    }
    
    IEnumerator CombatOver()
    {
        yield return new WaitForSeconds(0.25f);
        combatOverScreen.SetActive(true);

        for (int i = 0; i < totalLoot.Count; i++)
        {
            Manager.instance.inventory.AddItem(totalLoot[i], 1);
            totalLoot.Remove(totalLoot[i]);
        }

        Manager.instance.currentNode.isCompleted = true;
    }
}
