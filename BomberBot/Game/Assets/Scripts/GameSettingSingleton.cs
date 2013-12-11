/* Gardette Augustin */
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class GameSettingSingleton
{

	private static GameSettingSingleton instance;
	

	public GameSettingSingleton()
	{
		if (instance != null)
		{
			Debug.LogError ("Cannot have two instances of singleton. Self destruction in 3...");
			return;
		}
 	
		instance = this;
		
		
	}
	
	
	
	public static GameSettingSingleton Instance
	{
		get
		{
			if (instance == null)
			{
				new GameSettingSingleton ();
			}
 
			return instance;
		}
	}
	
	private ArrayList yellowHQRespawnPosition = new ArrayList();
	private ArrayList redHQRespawnPosition = new ArrayList();
	private ArrayList greenHQRespawnPosition = new ArrayList();
	private ArrayList blueHQRespawnPosition = new ArrayList();
	
	public ArrayList BlueHQRespawnPosition 
	{
		get {
			return this.blueHQRespawnPosition;
		}
		set {
			blueHQRespawnPosition = value;
		}
	}

	public ArrayList GreenHQRespawnPosition 
	{
		get {
			return this.greenHQRespawnPosition;
		}
		set {
			greenHQRespawnPosition = value;
		}
	}

	public ArrayList RedHQRespawnPosition
	{
		get {
			return this.redHQRespawnPosition;
		}
		set {
			redHQRespawnPosition = value;
		}
	}

	public ArrayList YellowHQRespawnPosition
	{
		get {
			return this.yellowHQRespawnPosition;
		}
		set {
			yellowHQRespawnPosition = value;
		}
	}

	public enum MenuState 
	{
		logout=0,
		mainMenu=1,
		clientMenu=2,
		serverMenu=3,
		settingMenu=4,
		graphicsSettingMenu=5,
		inputsSettingMenu=6,
		playing=7,
		startServer=8,
		joinServer=9
	}
	
	private MenuState currentMenuState;


	public MenuState CurrentMenuState
	{
		get {
			return this.currentMenuState;
		}
		set {
			currentMenuState = value;
		}
	}

	private bool menuStateHasChanged = false;

	public bool MenuStateHasChanged {
		get {
			return menuStateHasChanged;
		}
		set {
			menuStateHasChanged = value;
		}
	}

	//player configuration

	private string playerName = "Player";
	
	public string PlayerName
	{
		get {
			return this.playerName;
		}
		set {
			playerName = value;
		}
	}	

	private int portToUse = 25001;

	public int PortToUse {
		get {
			return portToUse;
		}
		set {
			portToUse = value;
		}
	}

	private string ipToConnect = "127.0.0.1";

	public string IpToConnect 
	{
		get {
			return ipToConnect;
		}
		set {
			ipToConnect = value;
		}
	}

	private Color[] teamColor = {Color.green,Color.blue, Color.red, Color.yellow};

	public Color[] TeamColor
	{
		get {
			return teamColor;
		}
	}

	private string[] team = {"green","blue","red","yellow"};

	public string[] Team 
	{
		get {
			return team;
		}
	}


	private int indexTeamSelected = 0;

	public int IndexTeamSelected 
	{
		get {
			return indexTeamSelected;
		}
		set {
			indexTeamSelected = value;
		}
	}

	private TextAsset[] arenaFileList = Resources.LoadAll<TextAsset>("Arena") ;

	public TextAsset[] ArenaFileList 
	{
		get {
			return arenaFileList;
		}
		set {
			arenaFileList = value;
		}
	}

	private int indexArenaSelected = 0;

	public int IndexArenaSelected 
	{
		get {
			return indexArenaSelected;
		}
		set {
			indexArenaSelected = value;
		}
	}

	private byte[] currentLoadedArena;
	
	public byte[] CurrentLoadedArena 
	{
		get {
			return currentLoadedArena;
		}
		set {
			currentLoadedArena = value;
		}
	}

	private Dictionary<NetworkViewID, Vector3> breakableBlockList = new Dictionary<NetworkViewID, Vector3>();

	public Dictionary<NetworkViewID, Vector3> BreakableBlockList {
		get {
			return breakableBlockList;
		}
		set {
			breakableBlockList = value;
		}
	}

	private int maxPlayerNumber = 10;

	public int MaxPlayerNumber 
	{
		get {
			return maxPlayerNumber;
		}
		set {
			maxPlayerNumber = value;
		}
	}

	private bool updateArenaViewer = false;

	public bool UpdateArenaViewer
	{
		get {
			return updateArenaViewer;
		}
		set {
			updateArenaViewer = value;
		}
	}

	public enum Winner : int {none = 0,green = 1,blue = 2, red = 3, yellow = 4};

	private Winner winnerTeam = Winner.none;

	public Winner WinnerTeam {
		get {
			return winnerTeam;
		}
		set {
			winnerTeam = value;
		}
	}

	private int currentResolutionIndex;

	public int CurrentResolutionIndex {
		get {
			return currentResolutionIndex;
		}
		set {
			currentResolutionIndex = value;
		}
	}



}
