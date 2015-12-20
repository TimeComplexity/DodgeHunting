using UnityEngine;
using System.Collections;


public static class Variables{
     
    public static int enemySwitch = 3; // 양 -1 멧돼지 0 늑대 1 캥거루 2 수리 3 미니늑대 101 클론캥거루 102
    public static int enemyLevel = 0; // 쉬움 0 보통 1 어려움 2 매우어려움 3 대장 4
	public static string skillType = "Fire"; //Fire Water Wind Earth
    public static string[] enemyName = new string[4] { "wildBoarClearLevel", "wolfClearLevel", "kangarooClearLevel", "suriClearLevel" };
    public static GameObject enemy1;
	public static GameObject enemy2;

    public static int[,] skillEquipped = 
        new int[4, 4] { { 0, 1, 2, 3 }, { 0,1,2,3 }, { 0,1,2,3 }, { 6, 6, 6, 6 } };
    //스킬은 0~5까지, 6은 기본 이미지
    // [0,] 불 / [1,] 물 / [2,] 바람 / [3,] 땅
    public static bool isVibrationOn = true;
}
