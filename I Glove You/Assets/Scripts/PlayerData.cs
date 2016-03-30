using UnityEngine;
using System.Collections.Generic;

public delegate void PlayerDataChangedEvent ();
public sealed class PlayerData
{
	private static PlayerData instance = null;
	private static readonly object padlock = new object ();

	private int _cleanData = 0;
	private int _tutorialDone = 0;
	private int _level = 0;

		

	// Event
	public static event PlayerDataChangedEvent SoundSettingChanged;

	PlayerData ()
	{

	}

	public static PlayerData Instance {
		get {
			lock (padlock)
			{
				if (instance == null)
				{
					instance = new PlayerData ();
				}
				return instance;
			}
		}
	}

	/// <summary>
	/// Cleans the data.
	/// </summary>
	/// <param name="clean">If set to <c>true</c> clean.</param>
	public void CleanData (bool clean)
	{
		if (clean)
		{
			PlayerPrefs.DeleteAll ();
		}
	}

	/**
     * read Player Data 
     */
	public void ReadData ()
	{
		bool hasSavedData = false;
		if (PlayerPrefs.HasKey ("CleanData"))
		{
			_cleanData = PlayerPrefs.GetInt ("CleanData");
			hasSavedData = true;
		}
		if (PlayerPrefs.HasKey ("tutorialDone"))
		{
			_tutorialDone = PlayerPrefs.GetInt ("tutorialDone");
			hasSavedData = true;
		}
		if (PlayerPrefs.HasKey ("level"))
		{
			_level = PlayerPrefs.GetInt ("level");
			hasSavedData = true;
		}

		if (!hasSavedData)
			SaveData ();
	}

	public int CheckAndSaveKey (string p_UniqueKey, int p_value)
	{
		if (PlayerPrefs.HasKey (p_UniqueKey))
		{
			p_value = PlayerPrefs.GetInt (p_UniqueKey, p_value);
		}
		else
		{
			PlayerPrefs.SetInt (p_UniqueKey, p_value);
		}
		PlayerPrefs.Save ();
		return p_value;
	}

	public void SaveKey (string p_UniqueKey, int p_value)
	{
		PlayerPrefs.SetInt (p_UniqueKey, p_value);
	}

	/**
     * Instantly saves all the player data
     */
	public void SaveData ()
	{
		PlayerPrefs.SetInt ("level", _level);
		PlayerPrefs.Save ();
	}

	public int Level {
		get {
			return _level;
		}
		set {
			_level = value;
		}
	}


	public int TutorialDone {
		get {
			return _tutorialDone;
		}
		set {
			_tutorialDone = value;
			PlayerPrefs.SetInt ("tutorialDone", _tutorialDone);
			PlayerPrefs.Save ();
		}
	}

}