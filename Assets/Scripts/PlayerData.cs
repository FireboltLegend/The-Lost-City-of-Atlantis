using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public int level;
	public float[] position;
	public float health;
	public float oxygen;

	public PlayerData(PlayerController player)
	{
		level = player.level;
		position = new float[3];
		position[0] = player.transform.position.x;
		position[1] = player.transform.position.y;
		position[2] = player.transform.position.z;
		health = player.health;
		oxygen = player.oxygen;
	}
}
