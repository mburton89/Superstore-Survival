using UnityEngine;
using System.Collections;

public class ProceduralNumberGenerator : MonoBehaviour
{
	public int min = 1;
	public int max = 4;
	public string stringNum;
	public static int currentPosition = 0;
	public const string key = "123413142143123413412413241314231412431";

    public static int GetNextNumber() 
	{
		string currentNum = key.Substring(currentPosition++ % key.Length, 1);
		return Random.Range(1, 5);
	}

	

}
