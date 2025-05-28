using System;
using System.Runtime.InteropServices;

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

        // Constructor to initialize the game
        public GameManager()
        {
            currentPlayer = new Player();
            // Default difficulty is set to medium
            currentDifficulty = DifficultyLevel.Medium;
            // default best attempts to attempt limit
            currentPlayer.BestAttempts.Add(DifficultyLevel.Easy, 10);
            currentPlayer.BestAttempts.Add(DifficultyLevel.Medium, 7);
            currentPlayer.BestAttempts.Add(DifficultyLevel.Hard, 5);
        }

        // Main game loop
        public void StartGame()
        {
            bool continuePlaying = true;

            DisplayWelcomeMessage();

            while (continuePlaying)
            {
                // TODO: Add difficulty selection menu here (Part 2)
                SelectDifficulty();
                
                // Initialize game settings based on difficulty
                InitializeGameSettings();
                
                // Generate target number - FIX ME! (Part 1)
                GenerateTargetNumber();
                
                PlayGame();
                
                DisplayGameResults();
                
                continuePlaying = AskToPlayAgain();
            }

            DisplayFinalStats();
        }

        private void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===========================================");
            Console.WriteLine("   Welcome to the NUMBER GUESSING GAME!   ");
            Console.WriteLine("===========================================");
            Console.WriteLine("\nPlease enter your name:");
            
            string playerName = Console.ReadLine();
            currentPlayer.Name = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName;
            
            Console.WriteLine($"\nHello, {currentPlayer.Name}!");
            Console.WriteLine("I'm thinking of a number... Can you guess it?");
            Console.WriteLine("\nPress any key to start!");
            Console.ReadKey();
            Console.Clear();
        }

        // asks user to select a difficulty
        private void SelectDifficulty()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Select Difficulty (Press E, M, or H): ");
            Console.WriteLine("E - Easy");
            Console.WriteLine("M - Medium");
            Console.WriteLine("H - Hard");
            bool valid = false;
            while(!valid)
            {
                Console.WriteLine("\nEnter your desired difficulty: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "e":
                    case "E":
                        currentDifficulty = DifficultyLevel.Easy;
                        valid = true;
                        break;
                    case "m":
                    case "M":
                        currentDifficulty = DifficultyLevel.Medium;
                        valid = true;
                        break;
                    case "h":
                    case "H":
                        currentDifficulty = DifficultyLevel.Hard;
                        valid = true;
                        break;
                    default:
                        Console.WriteLine("** Invalid input. Please choose a valid difficulty (E, M, H) **");
                        break;
                }
            }

            Console.Clear();
            
        }

        // FIX ME: This method has a bug in the random number generation (Part 1)
        private void GenerateTargetNumber()
        {      
            // DONE - got rid of random seed
            Random random = new Random(); 
            
            // The range depends on the difficulty level
            switch (currentDifficulty)
            {
                case DifficultyLevel.Easy:
                    targetNumber = random.Next(1, 50); // 1-50 for easy
                    break;
                case DifficultyLevel.Medium:
                    targetNumber = random.Next(1, 100); // 1-100 for medium
                    break;
                case DifficultyLevel.Hard:
                    targetNumber = random.Next(1, 500); // 1-500 for hard
                    break;
                default:
                    targetNumber = random.Next(1, 100);
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

        private void PlayGame()
        {
            Console.Clear();
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
                }
                else
                {
                    // TODO: Add color to the console output (Part 4)
                    string hint = guess < targetNumber ? "higher" : "lower";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Wrong! Try a {hint} number. Attempts left: {attemptsLimit - currentAttempts}");
                }
            }
        }

        private void DisplayGameInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Difficulty: {currentDifficulty}");
            Console.WriteLine($"Guess a number between 1 and {GetRangeForDifficulty()}");
            Console.WriteLine($"You have {attemptsLimit} attempts.");
            Console.WriteLine("----------------------------------------");
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\nAttempt {currentAttempts + 1}/{attemptsLimit}. Enter your guess: ");
                string input = Console.ReadLine();
                
                validInput = int.TryParse(input, out guess);
                
                if (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    // TODO: Add color to the console output (Part 4)
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
                else if (guess < 1 || guess > GetRangeForDifficulty())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    // TODO: Add color to the console output (Part 4)
                    Console.WriteLine($"Your guess must be between 1 and {GetRangeForDifficulty()}!");
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
                currentPlayer.GamesWon++;

                // TODO: Update player statistics (Part 3)
                // Update the best score for the current difficulty level
                if (currentAttempts < currentPlayer.BestAttempts[currentDifficulty])
                {
                    currentPlayer.BestAttempts[currentDifficulty] = currentAttempts;
                }
            }
            else
            {
                // TODO: Add color to the console output (Part 4)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Over! You've run out of attempts.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"The number was: {targetNumber}");
                currentPlayer.GamesLost++;
                
                // TODO: Update player statistics (Part 3)

            }
            
            Console.WriteLine("----------------------------------------");
        }
        
        private bool AskToPlayAgain()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nWould you like to play again? (Y/N): ");
            string response = Console.ReadLine().Trim().ToUpper();
            
            return response == "Y" || response == "YES";
        }
        
        private void DisplayFinalStats()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===========================================");
            Console.WriteLine("              FINAL STATS                  ");
            Console.WriteLine("===========================================");
            Console.WriteLine($"Player: {currentPlayer.Name}");
            Console.WriteLine($"Games Played: {currentPlayer.GamesPlayed}");
            Console.WriteLine($"Games Won: {currentPlayer.GamesWon}");
            Console.WriteLine($"Win Rate: {currentPlayer.WinRate:P2}");

            // TODO: Display best scores (Part 3 & 5)
            Console.WriteLine($"Best Easy Attempts: {currentPlayer.BestAttempts[DifficultyLevel.Easy]}");
            Console.WriteLine($"Best Medium Attempts: {currentPlayer.BestAttempts[DifficultyLevel.Medium]}");
            Console.WriteLine($"Best Hard Attempts: {currentPlayer.BestAttempts[DifficultyLevel.Hard]}");

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
        public Dictionary<DifficultyLevel, int> BestAttempts = new Dictionary<DifficultyLevel, int>();
        
        // Add properties to track the best score (fewest attempts) for each difficulty level
    }
}
