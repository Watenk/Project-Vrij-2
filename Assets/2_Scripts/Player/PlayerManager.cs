using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ICollectionManager<Player, Player, int>, IUpdateable
{
	private List<Player> players = new List<Player>();
	
	public void Update()
	{
		foreach (Player player in players) player.Update();
	}
	
	public void Add(Player data)
	{
		players.Add(data);
	}

	public Player Get(int getter)
	{
		return players[getter];
	}

	public int GetCount()
	{
		return players.Count;
	}

	public void Remove(Player instance)
	{
		players.Remove(instance);
	}

    public void Remove(int getter)
    {
        players.Remove(Get(getter));
    }
}
