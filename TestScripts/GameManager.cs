using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject Player;
	public GameObject [] enemyTypes;
	public int killCount = 0;
	public int sentryBossKillCount = 0;
	public int BigBossKillCount = 0;
	//public TextMeshPro killCountText;
	public Text killCountText;
	public Text killDoorText1;
	public Text killDoorText2;
	public Text killDoorText3;
	public Text killDoorText4;
	public Text killDoorText5;
	public Text killDoorText6;
	public Text killBossDoorText1;
	public GameObject KillCountDoor1;
	public GameObject KillCountDoor2;
	public GameObject KillCountDoor3;
	public GameObject KillCountDoor4;
	public GameObject KillCountDoor5;
	public GameObject KillCountDoor6;
	public GameObject KillBossDoor1;
	public GameObject KillBossDoor2;
	// public GameObject SavePoint1;
	// public GameObject SavePoint2;
	// public GameObject SavePoint3;
	// public GameObject SavePoint4;
	// public GameObject SavePoint5;
	public GameObject [] spawningPointsA;
	public GameObject [] spawningPointsB;
	public GameObject [] spawningPointsC;
	public GameObject [] spawningPointsD;
	public GameObject [] BossSentries;
	public GameObject [] BigBoss;
	// public int spawnedinA;
	// public int spawnedinB;
	// public int spawnedinC;
	// public int spawnedinD;
	public int enemySpawned;


	void Awake(){
		instance = this;
		Time.timeScale = 1.0f;
	}
	
	// Use this for initialization
	// IEnumerator Start () {
	// 	while (true) {

	// 		GameObject randEnemyType = enemyTypes [Random.Range (0, enemyTypes.Length)];

    //         GameObject randSpawnA = spawningPointsA [Random.Range (0, spawningPointsA.Length)];
    //         Instantiate (randEnemyType, randSpawnA.transform.position, Quaternion.identity);            

	// 		GameObject randSpawnB = spawningPointsB [Random.Range (0, spawningPointsB.Length)];
	// 		Instantiate (randEnemyType, randSpawnB.transform.position, Quaternion.identity);

	// 		GameObject randSpawnC = spawningPointsC [Random.Range (0, spawningPointsC.Length)];
	// 		Instantiate (randEnemyType, randSpawnC.transform.position, Quaternion.identity);

	// 		GameObject randSpawnD = spawningPointsD [Random.Range (0, spawningPointsD.Length)];
	// 		Instantiate (randEnemyType, randSpawnD.transform.position, Quaternion.identity);
			
    //         yield return new WaitForSeconds(10f);
			
    //     }
	// }

	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		KillDoorConditions ();
		KillDoorTextDisplayConditions ();
		KillBossDoorConditions ();

		if (BossSentries[0] == null && BossSentries[1] == null) {
			sentryBossKillCount = 2;
		}

		if (BigBoss[0] == null) {
			BigBossKillCount = 1;
		}

		if (enemySpawned <= 75) {
			GameObject randEnemyType = enemyTypes [Random.Range (0, enemyTypes.Length)];
			//if (killCount <= 30)
            GameObject randSpawnA = spawningPointsA [Random.Range (0, spawningPointsA.Length)];
            Instantiate (randEnemyType, randSpawnA.transform.position, Quaternion.identity);            

			GameObject randSpawnB = spawningPointsB [Random.Range (0, spawningPointsB.Length)];
			Instantiate (randEnemyType, randSpawnB.transform.position, Quaternion.identity);

			GameObject randSpawnC = spawningPointsC [Random.Range (0, spawningPointsC.Length)];
			Instantiate (randEnemyType, randSpawnC.transform.position, Quaternion.identity);

			GameObject randSpawnD = spawningPointsD [Random.Range (0, spawningPointsD.Length)];
			Instantiate (randEnemyType, randSpawnD.transform.position, Quaternion.identity);
		}

	}

	// IEnumerator SpawnEnemyChaser () {
	// 	GameObject randSpawn = spawningPointsA [Random.Range (0, spawningPointsA.Length)];

    //     Instantiate (enemyChaser, randSpawn.transform.position, Quaternion.identity);            

    //     yield return new WaitForSeconds(5f);
	// }

	void KillDoorConditions () {
		if (killCount >= 5 && Vector3.Distance(Player.transform.position, KillCountDoor1.transform.position) < 10) {
			KillCountDoor1.transform.position = Vector3.MoveTowards (KillCountDoor1.transform.position,
																	new Vector3 (KillCountDoor1.transform.position.x, -3f, KillCountDoor1.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillCountDoor1.transform.position = Vector3.MoveTowards (KillCountDoor1.transform.position,
																	new Vector3 (KillCountDoor1.transform.position.x, 1.5f, KillCountDoor1.transform.position.z), 
																	3 * Time.deltaTime);
		}
		
		if (killCount >= 10 && Vector3.Distance(Player.transform.position, KillCountDoor2.transform.position) < 10) {
			KillCountDoor2.transform.position = Vector3.MoveTowards (KillCountDoor2.transform.position,
																	new Vector3 (KillCountDoor2.transform.position.x, -3f, KillCountDoor2.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillCountDoor2.transform.position = Vector3.MoveTowards (KillCountDoor2.transform.position,
																	new Vector3 (KillCountDoor2.transform.position.x, 1.5f, KillCountDoor2.transform.position.z), 
																	3 * Time.deltaTime);
		}

		if (killCount >= 15 && Vector3.Distance(Player.transform.position, KillCountDoor3.transform.position) < 10) {
			KillCountDoor3.transform.position = Vector3.MoveTowards (KillCountDoor3.transform.position,
																	new Vector3 (KillCountDoor3.transform.position.x, -3f, KillCountDoor3.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillCountDoor3.transform.position = Vector3.MoveTowards (KillCountDoor3.transform.position,
																	new Vector3 (KillCountDoor3.transform.position.x, 1.5f, KillCountDoor3.transform.position.z), 
																	3 * Time.deltaTime);
		}

		if (killCount >= 20 && Vector3.Distance(Player.transform.position, KillCountDoor4.transform.position) < 10) {
			KillCountDoor4.transform.position = Vector3.MoveTowards (KillCountDoor4.transform.position,
																	new Vector3 (KillCountDoor4.transform.position.x, -3f, KillCountDoor4.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillCountDoor4.transform.position = Vector3.MoveTowards (KillCountDoor4.transform.position,
																	new Vector3 (KillCountDoor4.transform.position.x, 1.5f, KillCountDoor4.transform.position.z), 
																	3 * Time.deltaTime);
		}

		if (killCount >= 25 && Vector3.Distance(Player.transform.position, KillCountDoor5.transform.position) < 10) {
			KillCountDoor5.transform.position = Vector3.MoveTowards (KillCountDoor5.transform.position,
																	new Vector3 (KillCountDoor5.transform.position.x, -3f, KillCountDoor5.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillCountDoor5.transform.position = Vector3.MoveTowards (KillCountDoor5.transform.position,
																	new Vector3 (KillCountDoor5.transform.position.x, 1.5f, KillCountDoor5.transform.position.z), 
																	3 * Time.deltaTime);
		}

		if (killCount >= 30 && Vector3.Distance(Player.transform.position, KillCountDoor6.transform.position) < 10) {
			KillCountDoor6.transform.position = Vector3.MoveTowards (KillCountDoor6.transform.position,
																	new Vector3 (KillCountDoor6.transform.position.x, -3f, KillCountDoor6.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillCountDoor6.transform.position = Vector3.MoveTowards (KillCountDoor6.transform.position,
																	new Vector3 (KillCountDoor6.transform.position.x, 1.5f, KillCountDoor6.transform.position.z), 
																	3 * Time.deltaTime);
		}
	}

	void KillDoorTextDisplayConditions () {
		if (Vector3.Distance(Player.transform.position, KillCountDoor1.transform.position) < 10 && killCount < 5) {
			killDoorText1.enabled = true;
		} else {
			killDoorText1.enabled = false;			
		}

		if (Vector3.Distance(Player.transform.position, KillCountDoor2.transform.position) < 10 && killCount < 10) {
			killDoorText2.enabled = true;
		} else {
			killDoorText2.enabled = false;			
		}

		if (Vector3.Distance(Player.transform.position, KillCountDoor3.transform.position) < 10 && killCount < 15) {
			killDoorText3.enabled = true;
		} else {
			killDoorText3.enabled = false;			
		}

		if (Vector3.Distance(Player.transform.position, KillCountDoor4.transform.position) < 10 && killCount < 20) {
			killDoorText4.enabled = true;
		} else {
			killDoorText4.enabled = false;			
		}

		if (Vector3.Distance(Player.transform.position, KillCountDoor5.transform.position) < 10 && killCount < 25) {
			killDoorText5.enabled = true;
		} else {
			killDoorText5.enabled = false;			
		}

		if (Vector3.Distance(Player.transform.position, KillCountDoor6.transform.position) < 10 && killCount < 30) {
			killDoorText6.enabled = true;
		} else {
			killDoorText6.enabled = false;			
		}

		if (Vector3.Distance(Player.transform.position, KillBossDoor1.transform.position) < 10 && sentryBossKillCount < 2) {
			killBossDoorText1.enabled = true;
		} else {
			killBossDoorText1.enabled = false;			
		}

		if (Vector3.Distance(Player.transform.position, KillBossDoor2.transform.position) < 10 && BigBossKillCount < 1) {
			killBossDoorText1.enabled = true;
		} else {
			killBossDoorText1.enabled = false;			
		}
	}

	void KillBossDoorConditions () {
		if (sentryBossKillCount == 2 && Vector3.Distance(Player.transform.position, KillBossDoor1.transform.position) < 10) {
			KillBossDoor1.transform.position = Vector3.MoveTowards (KillBossDoor1.transform.position,
																	new Vector3 (KillBossDoor1.transform.position.x, -3f, KillBossDoor1.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillBossDoor1.transform.position = Vector3.MoveTowards (KillBossDoor1.transform.position,
																	new Vector3 (KillBossDoor1.transform.position.x, 1.5f, KillBossDoor1.transform.position.z), 
																	3 * Time.deltaTime);
		}

		if (BigBossKillCount == 1 && Vector3.Distance(Player.transform.position, KillBossDoor2.transform.position) < 10) {
			KillBossDoor2.transform.position = Vector3.MoveTowards (KillBossDoor2.transform.position,
																	new Vector3 (KillBossDoor2.transform.position.x, -3f, KillBossDoor2.transform.position.z), 
																	3 * Time.deltaTime);
		} else {
			KillBossDoor2.transform.position = Vector3.MoveTowards (KillBossDoor2.transform.position,
																	new Vector3 (KillBossDoor2.transform.position.x, 1.5f, KillBossDoor2.transform.position.z), 
																	3 * Time.deltaTime);
		}
	}

	public void AddKillCount (int kills) {
		killCount += kills;
		SetKillCount ();
	}

	void SetKillCount () {
		killCountText.text = "Kills: " + killCount.ToString ();
	}
}
