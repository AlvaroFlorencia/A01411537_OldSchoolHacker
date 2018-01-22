using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Hacker : MonoBehaviour {
	//menuHint it's a constant to show to te user that he can return to the menu en any time
	const string menuHint = "You can type menu at any time.";


	// These arrays hold the passwords for the levels
	ArrayList uno = new ArrayList();
	ArrayList dos = new ArrayList();  //To charge the words in the text field
	ArrayList tres = new ArrayList();
	ArrayList cuatro = new ArrayList();

	string[] level1Passwords;  //Way to convert in a consistent array
	string[] level2Passwords;
	string[] level3Passwords ;
	string[] level4Passwords;

	TextAsset game;
	//Game state variables
 	int level;                                      
	enum GameState { MainMenu, Password, Win };     //  Game's states
	GameState currentScreen = GameState.MainMenu;   //  Instance of	current screen state
	string password;   
	string levelr;
	//  To contain the password


	bool changePassword;                            //  Flag to know when to change the password
	bool continued;
	 
	void Start () {
		ShowMainMenu(); //  Show the main menu to the user
	}



	void Update () {
		

		using (StreamReader leer = new StreamReader (@"Assets\WM2000\veterri.txt")) {  //method used to read the fiels .txt
			while (!leer.EndOfStream) { //while not be  the final of the file 
				string a= leer.ReadLine (); //the text is saved in  string x
				uno.Add (a); //Added to arraylist
				level1Passwords = (String[]) uno.ToArray( typeof( string ) );//Changed to array
				}
			}
		//The same function for the all levels
		using (StreamReader leer = new StreamReader (@"Assets\WM2000\bank.txt")) {
			while (!leer.EndOfStream) {
				string b= leer.ReadLine ();
				dos.Add (b);
				level2Passwords = (String[]) dos.ToArray( typeof( string ) );
			}
		}
		using (StreamReader leer = new StreamReader (@"Assets\WM2000\game.txt")) {
			while (!leer.EndOfStream) {
				string c= leer.ReadLine ();
				tres.Add (c);
				level3Passwords = (String[]) tres.ToArray( typeof( string ) );
			}
		}
		using (StreamReader leer = new StreamReader (@"Assets\WM2000\alien.txt")) {
			while (!leer.EndOfStream) {
				string d= leer.ReadLine ();
				cuatro.Add (d);
				level4Passwords = (String[]) cuatro.ToArray( typeof( string ) );
			}
		}


	}

	//  This method will show the user the game's main menu
	void ShowMainMenu()
	{
		//  We clear the screen
		Terminal.ClearScreen();

		//  We show the menu


		Terminal.WriteLine("What would you like to hack into?");
		Terminal.WriteLine("");
		Terminal.WriteLine(@"
 ");
		Terminal.WriteLine("Press 1 for a veterinary");
		Terminal.WriteLine("Press 2 for the bank");
		Terminal.WriteLine("Press 3 for a Steam ");
		Terminal.WriteLine("Press 4 for the NASA");
		Terminal.WriteLine("");
		Terminal.WriteLine("Enter your selection:");

		//  We set our current screen state as the main menu
		currentScreen = GameState.MainMenu;

		//  The changePassword is changed to trueattbecause we are going
		//  to try to crack a new password.
		changePassword = true;
	}

	//  The OnUserInput is called everytime the user hits enter on their keyboard.  
	void OnUserInput(string input)
	{
		//  If user types "menu",the menu is showed
		if (input == "menu") {
			ShowMainMenu ();
		}
		//  If the user types quit, close or  exit,then the game is closed
		else if (input == "quit" || input == "close" || input == "exit") {
			Terminal.WriteLine ("Please, close the browser's tab");
			Application.Quit ();
		}
		//  If the user inputs another word different and we are going to handle the game depends on the states 
		//  If the game state is on MainMenu, then we call the RunMainMenu() method

		else if (currentScreen == GameState.MainMenu) {
			RunMainMenu (input);
		}
		//  But if the current game state is Password, then we call the method CheckPassword method
		else if (currentScreen == GameState.Password) {
			CheckPassword(input);
		} 
	
			
		else if (currentScreen == GameState.Win) {  //If the game state is "Win" 
			if (input == "yes"&&level<=4) {  //leve 1 to 3
				
				input = level.ToString (); //The input is now de next level

				SetRandomPassword (); //Change the word in its corresponding level
				RunMainMenu (input);//Again try to hack the word
			


			}
			else if(input=="no"){  // if not, we go the menu
				ShowMainMenu ();
			}
		
		}
	}

	//CheckPassword Method
	private void CheckPassword(string input)
	{
		//  If the user inpus it's the corret,the  DisplayWinScreen() method is activated
		if (input == password)
		{
			DisplayWinScreen();
		}
		//  Otherwise,the AskForPassword() method is activated
		else
		{
			AskForPassword();
		}
	}

	//  This method updates the current game state and displays the win screen
	private void DisplayWinScreen()
	{
		
		//  We set the current game state to be the Win screen  
		currentScreen = GameState.Win;

		//  Then we clear the screen
		Terminal.ClearScreen();

		//  he ShowLevelReward is showed and show the user  the menu hint, so he can return to the menu
		ShowLevelReward();
		Terminal.WriteLine(menuHint);
	}


	private void ShowLevelReward()
	{
		//  Depending on the difficult of  level, this method shows a different reward.
	
		switch (level)
		{
		case 1:
			Terminal.WriteLine ("You have a cat!");
			Terminal.WriteLine (@" /
░░░░░▄░░░░░░█▄█░░░░░
░░░▄▀░░░░░▄▄██▀░░░░░
░░░▀▄░░▄██████░░░░░░
▒░▒░▀▄████▀█▀█▒░▒░▒▒
░▒░▒░▒▀██▄▒█▒█░▒░▒░░        
            ");
			Terminal.WriteLine ("¿Next level? yes/no");

			level =  2;//The level number becomes the next one, in case we want to go to the next level

			break;
		case 2: 
			Terminal.WriteLine ("The lock is open!");
			Terminal.WriteLine (@"
  ▄▀▀▀▄─────────────── 
──█───█─────────────── 
─███████─────────▄▀▀▄─ 
░██───██░░█▀█▀▀▀▀█░░█░ 
░███▄███░░▀░▀░░░░░▀▀░░

                ");
			Terminal.WriteLine ("¿Next level? yes/no");
			
			level = 3;//The level number becomes the next one, in case we want to go to the next level

			break;
		case 3:
			Terminal.WriteLine ("Full games for you!");
			Terminal.WriteLine (@"
░░┌──┐░░░░░░░░░░┌──┐░░ 
░╔╡▐▐╞╝░░┌──┐░░╔╡▐▐╞╝░ 
░░└╥╥┘░░╚╡▌▌╞╗░░└╥╥┘░░ 
░░░╚╚░░░░└╥╥┘░░░░╚╚░░░ 
░░░░░░░░░░╝╝░░░░░░░░░░
");
			Terminal.WriteLine ("¿Next level? yes/no");
			Terminal.WriteLine (" ");

			level = 4; //The level number becomes the next one, in case we want to go to the next level
			break;
		case 4:
			Terminal.WriteLine("Greetings...");
			Terminal.WriteLine(@"
──────▀▄▄▄▄▄▄▄▀───────
───────█▀█▀█▀█────────
────█▀▄█▄▄▄▄▄█▄▀█─────
───────███████────────
──────██▀▀▀▀▀██─
");
			Terminal.WriteLine("Welcome to space,welcome to the NASA!");
			break;
		default:
			Debug.LogError("Invalid level reached.");
			break;
		}
	}


	//  This method validates the iput when the game state is MainMenu.
	void RunMainMenu(string input)
	{
		//  Valid inputs
		bool isValidInput = (input == "1") || (input == "2") || (input == "3")|| (input == "4");


		if (isValidInput)
		{
			level = int.Parse(input); //The input is converted to a int type
			AskForPassword();
		}
		//Some little jokes
		else if (input == "007")
		{
			Terminal.WriteLine("Please enter a valid level, Mr. Bond");
		}
		else if (input == "5")
		{
			Terminal.WriteLine("Near, Try a less number");
		}
		else if (input == "0")
		{
			Terminal.WriteLine("Near, Try a one number more bigger than 0");
		}
		// For the other possible inputs
		else
		{
			Terminal.WriteLine("Enter a valid level");
		}
	}

	//Method whe the state of the game is password
	private void AskForPassword()
	{
		//  We set our current game state as Password
		currentScreen = GameState.Password;

		//  The screen is cleaned
		Terminal.ClearScreen();

		//If change password is true, we call the method  SetRandomPassword()
		if (changePassword) {
			SetRandomPassword();
		}

		//  Show the instruction and the angram
		Terminal.WriteLine("Enter your password. Hint: " + password.Anagram());
		Terminal.WriteLine(menuHint);
	}


	private void SetRandomPassword()
	{
		//The changepassword is flase because,in this way the word is not changed if it is incorrect
		changePassword = false;

		//  Depending on the level ,the word that is showed
		switch(level)
		{
		case 1:
			password = level1Passwords[UnityEngine.Random.Range(0, level1Passwords.Length)];
			break;
		case 2:
			password = level2Passwords[UnityEngine.Random.Range(0, level2Passwords.Length)];
			break;
		case 3:
			password = level3Passwords[UnityEngine.Random.Range(0, level3Passwords.Length)];
			break;
		case 4:
			password = level4Passwords[UnityEngine.Random.Range(0, level4Passwords.Length)];
			break;
		default:
			Debug.LogError("Invalid level.  How did you manage that?");
			//Default message error is shoeed, and changePassword is changed to true;
			changePassword = true;
			break;
		}
	}
}