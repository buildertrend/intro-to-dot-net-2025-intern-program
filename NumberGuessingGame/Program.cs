using System;
using System.Reflection.Metadata.Ecma335;

namespace NumberGuessingGame
{
    class Program
    {
        // NOTE TO STUDENTS: This is the main entry point for our Number Guessing Game
        static void Main(string[] args)
        {
            // Initialize game manager
            GameManager gameManager = new GameManager();
            
            // Start the game loop
            gameManager.StartGame();
        }
    }

    // This class handles the main game logic
    class GameManager
    {
        private Player currentPlayer;
        private int targetNumber;
        private int attemptsLimit;
        private int currentAttempts;
        private bool gameWon;
        private DifficultyLevel currentDifficulty;
        private int bestScore = 0;
        private int numPlayers = 1;
        private int[] playerScores;
        
        

        // Constructor to initialize the game
        public GameManager()
        {
            currentPlayer = new Player();
            // Default difficulty is set to medium
            currentDifficulty = DifficultyLevel.Medium;
        }

        // Main game loop
        public void StartGame()
        {
            bool continuePlaying = true;

            DisplayWelcomeMessage();

            while (continuePlaying)
            {
                // TODO: Add difficulty selection menu here (Part 2)
                
                // Initialize game settings based on difficulty
                InitializeGameSettings();
                Console.WriteLine($"{numPlayers} players playing");
                
                for (int i = 0; i < numPlayers; i++)
                {
                    // Generate target number - FIX ME! (Part 1)
                    GenerateTargetNumber();
                    
                    Console.Clear();
                    
                    if (numPlayers > 1)
                    {
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine($"Player {i + 1}'s Turn");
                        Console.WriteLine("------------------------------------------");

                    }
                    playerScores[i] = PlayGame();
                
                    DisplayGameResults();
                    if (numPlayers > 1 && i < numPlayers - 1)
                    {
                        Console.WriteLine("\nPress any key to start next player's turn.");
                        Console.ReadKey();
                    }
                }

                if (numPlayers > 1)
                {
                    Console.WriteLine("==========================================");
                    int minValue = 100;
                    int minPlayer = 0;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        if (playerScores[i] <= minValue)
                        {
                            minValue = playerScores[i];
                            minPlayer = i + 1;
                        }
                        Console.WriteLine($"\nPlayer {i + 1}: {playerScores[i]}");
                    }
                    Console.WriteLine($"\n\nPlayer {minPlayer} with a score of {playerScores[minPlayer - 1]} wins!!\n");
                    Console.WriteLine("==========================================");
                }
                continuePlaying = AskToPlayAgain();
            }

            DisplayFinalStats();
        }

        private void DisplayWelcomeMessage()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   Welcome to the NUMBER GUESSING GAME!   ");
            Console.WriteLine("===========================================");
            Console.WriteLine("\nPlease enter your name:");
            
            string playerName = Console.ReadLine();
            currentPlayer.Name = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName;
            
            Console.WriteLine($"\nHello, {currentPlayer.Name}!");
            Console.WriteLine("Please enter a difficulty level: Easy, Medium, or Hard --> (E/M/H)");
            string difficultyEntry = Console.ReadLine();
            switch(difficultyEntry){
                case "E":
                    currentDifficulty = DifficultyLevel.Easy;
                    break;
                case "M":
                    currentDifficulty = DifficultyLevel.Medium;
                    break;
                case "H":
                    currentDifficulty = DifficultyLevel.Hard;
                    break;
                default:
                    currentDifficulty = DifficultyLevel.Medium;
                    break;
            }

            while (true) 
            {
                Console.WriteLine("Please enter the amount of players (1-4)");
                numPlayers = int.Parse(Console.ReadLine());
                if (numPlayers > 0 && numPlayers <= 4) 
                {
                    playerScores = new int[numPlayers];
                    break;
                }
            }

            Console.WriteLine("I'm thinking of a number... Can you guess it?");
            Console.WriteLine("\nPress any key to start!");
            Console.ReadKey();
            Console.Clear();
        }

        // FIX ME: This method has a bug in the random number generation (Part 1)
        private void GenerateTargetNumber()
        {            
            Random random = new Random(); 
            
            // The range depends on the difficulty level
            switch (currentDifficulty)
            {
                case DifficultyLevel.Easy:
                    targetNumber = random.Next(1, 51); // 1-50 for easy
                    break;
                case DifficultyLevel.Medium:
                    targetNumber = random.Next(1, 101); // 1-100 for medium
                    break;
                case DifficultyLevel.Hard:
                    targetNumber = random.Next(1, 501); // 1-500 for hard
                    break;
                default:
                    targetNumber = random.Next(1, 101);
                    break;
            }
        }
        
        private void InitializeGameSettings()
        {
            // Initialize game variables based on difficulty
            switch (currentDifficulty)
            {
                case DifficultyLevel.Easy:
                    attemptsLimit = 10;
                    break;
                case DifficultyLevel.Medium:
                    attemptsLimit = 7;
                    break;
                case DifficultyLevel.Hard:
                    attemptsLimit = 5;
                    break;
                default:
                    attemptsLimit = 7; // Default to medium
                    break;
            }
            
            currentAttempts = 0;
            gameWon = false;
        }

        private int PlayGame()
        {
            
            DisplayGameInfo();

            while (currentAttempts < attemptsLimit && !gameWon)
            {
                int guess = GetUserGuess();
                currentAttempts++;
                
                if (guess == targetNumber)
                {
                    gameWon = true;
                    // TODO: Add color to the console output (Part 4)
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nCorrect! You guessed the number in {currentAttempts} attempts.");
                    bestScore = currentAttempts;
                    Console.WriteLine($"Best Score: {bestScore}");

                    Console.ForegroundColor = ConsoleColor.White;

                    bestScore = currentAttempts;
                }
                else
                {
                    // TODO: Add color to the console output (Part 4)
                    string hint = guess < targetNumber ? "higher" : "lower";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Wrong! Try a {hint} number. Attempts left: {attemptsLimit - currentAttempts}");
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }

            return currentAttempts;
        }

        private void DisplayGameInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Difficulty: {currentDifficulty}");
            if (bestScore > 0)
            {
                Console.WriteLine($"Best Score: {bestScore}");
            }
            Console.WriteLine($"Guess a number between 1 and {GetRangeForDifficulty()}");
            Console.WriteLine($"You have {attemptsLimit} attempts.");
            Console.WriteLine("----------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;

        }

        private int GetRangeForDifficulty()
        {
            return currentDifficulty switch
            {
                DifficultyLevel.Easy => 50,
                DifficultyLevel.Medium => 100,
                DifficultyLevel.Hard => 500,
                _ => 100
            };
        }

        private int GetUserGuess()
        {
            int guess = 0;
            bool validInput = false;
            
            while (!validInput)
            {
                Console.Write($"\nAttempt {currentAttempts + 1}/{attemptsLimit}. Enter your guess: ");
                string input = Console.ReadLine();
                
                validInput = int.TryParse(input, out guess);
                
                if (!validInput)
                {
                    // TODO: Add color to the console output (Part 4)
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else if (guess < 1 || guess > GetRangeForDifficulty())
                {
                    // TODO: Add color to the console output (Part 4)
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Your guess must be between 1 and {GetRangeForDifficulty()}!");
                    Console.ForegroundColor = ConsoleColor.White;
                    validInput = false;
                }
            }
            
            return guess;
        }
        
        private void DisplayGameResults()
        {
            Console.WriteLine("\n----------------------------------------");
            
            if (gameWon)
            {
                // TODO: Add color to the console output (Part 4)
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You won the game!");
                Console.ForegroundColor = ConsoleColor.White;
                currentPlayer.GamesWon++;
                
                // TODO: Update player statistics (Part 3)
                // Update the best score for the current difficulty level
            }
            else
            {
                // TODO: Add color to the console output (Part 4)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Over! You've run out of attempts.");
                Console.WriteLine($"The number was: {targetNumber}");
                Console.ForegroundColor = ConsoleColor.White;
                currentPlayer.GamesLost++;
                
                // TODO: Update player statistics (Part 3)
            }

            gameWon = false;
            currentAttempts = 0;
            Console.WriteLine("----------------------------------------");
        }
        
        private bool AskToPlayAgain()
        {
            Console.Write("\nWould you like to play again? (Y/N): ");
            string response = Console.ReadLine().Trim().ToUpper();
            
            return response == "Y" || response == "YES";
        }
        
        private void DisplayFinalStats()
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("              FINAL STATS                  ");
            Console.WriteLine("===========================================");
            Console.WriteLine($"Player: {currentPlayer.Name}");
            Console.WriteLine($"Number of Players: {numPlayers}");
            Console.WriteLine($"Games Played: {currentPlayer.GamesPlayed}");
            Console.WriteLine($"Games Won: {currentPlayer.GamesWon}");
            Console.WriteLine($"Win Rate: {currentPlayer.WinRate:P2}");
            
            // TODO: Display best scores (Part 3 & 5)
            Console.WriteLine($"Best Score: {bestScore}");
            Console.WriteLine("\nThanks for playing!");
            Console.WriteLine("===========================================");
        }
    }

    // Enumeration for difficulty levels
    enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    // Class to store player information and statistics
    class Player
    {
        public string Name { get; set; }
        public int GamesPlayed => GamesWon + GamesLost;
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        
        // Calculate win rate as a percentage
        public double WinRate => GamesPlayed == 0 ? 0 : (double)GamesWon / GamesPlayed;

        // TODO: Implement tracking of best scores (Part 3 & 5)
        // Add properties to track the best score (fewest attempts) for each difficulty level
    }
}
