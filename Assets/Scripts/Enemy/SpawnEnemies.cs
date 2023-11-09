using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private GridManager gridManager;

    [SerializeField] private Vector3 startPoint;

    [SerializeField] private int wave;

    [SerializeField] private int enemyCount;
    [SerializeField] private float prepTime;
    [SerializeField] private float firstPrepTime;
    [SerializeField] private int startEnemyCount;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private TMP_Text text;
    [SerializeField] private float timeForTextShowing;

    private void Start()
    {
        startPoint = (Vector3)gridManager.GetStartPoint();
        enemyCount = startEnemyCount;
        StartCoroutine(BattlePreperation(firstPrepTime));
    }

    private void Update()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
        //    var i = Random.Range(0, enemy.Count);
        //    Instantiate(enemy[i], startPoint, Quaternion.identity);
        //}
        //// For debugging purposes ATM
        //if (Input.GetKeyDown("left"))
        //{
        //    List<GameObject> enemyList = new List<GameObject>();
        //    enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        //    foreach (GameObject gameObject in enemyList)
        //    {
        //        Enemy enemy = gameObject.GetComponent<Enemy>();
        //    }
        //}
    }

    public int GetWave()
    {
        return wave;
    }

    private void CalculateEnemyCount()
    {
        //enemyCount = (int)Mathf.Pow(startEnemyCount, wave);
        enemyCount += wave; 
    }

    IEnumerator ShowText(string t)
    {
        text.text = t;
        yield return new WaitForSeconds(timeForTextShowing);
        text.text = "";
        StopCoroutine("ShowText");
    }

    IEnumerator BattlePreperation(float time)
    {
        CalculateEnemyCount();
        yield return new WaitForSeconds(time);
        StartCoroutine(ShowText($"Wave: {wave}"));
        StartCoroutine(Spawn());
        StopCoroutine("BattlePreperation");
    }

    IEnumerator Spawn()
    {
        int tempEnemyCount = enemyCount;
        while(true)
        {
            float waitTime = Random.Range(0f, 1f);
            var i = Random.Range(0, enemy.Count);

            Instantiate(enemy[i], startPoint, Quaternion.identity);

            yield return new WaitForSeconds(waitTime);

            tempEnemyCount--;

            if (tempEnemyCount == 0)
            {
                StopCoroutine("ShowText");
                StartCoroutine(ShowText($"End of wave {wave}"));
                playerStats.IncreaseMoney(49f + wave);
                wave++;
                StartCoroutine(BattlePreperation(prepTime));
                StopCoroutine("Spawn");
                break;
            }
        }
    }


}
