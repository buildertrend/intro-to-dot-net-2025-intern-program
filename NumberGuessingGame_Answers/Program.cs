using System;

namespace NumberGuessingGame_Answers
{
    class Program
    {
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
        }

        // Main game loop
        public void StartGame()
        {
            bool continuePlaying = true;

            DisplayWelcomeMessage();

            while (continuePlaying)
            {
                SelectDifficulty(); // Part 2: Added difficulty selection
                
                // Initialize game settings based on difficulty
                InitializeGameSettings();
                
                // Generate target number - Fixed random number generation (Part 1)
                GenerateTargetNumber();
                
                PlayGame();
                
                DisplayGameResults();
                
                continuePlaying = AskToPlayAgain();
            }

            DisplayFinalStats();
        }

        private void DisplayWelcomeMessage()
        {
            Console.BackgroundColor = ConsoleColor.Blue; // Part 4: Added color
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===========================================");
            Console.WriteLine("   Welcome to the NUMBER GUESSING GAME!   ");
            Console.WriteLine("===========================================");
            Console.ResetColor();
            
            Console.WriteLine("\nPlease enter your name:");
            
            string playerName = Console.ReadLine();
            currentPlayer.Name = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName;
            
            Console.ForegroundColor = ConsoleColor.Cyan; // Part 4: Added color
            Console.WriteLine($"\nHello, {currentPlayer.Name}!");
            Console.WriteLine("I'm thinking of a number... Can you guess it?");
            Console.ResetColor();
            
            Console.WriteLine("\nPress any key to start!");
            Console.ReadKey();
            Console.Clear();
        }

        // Part 2: Added difficulty selection menu
        private void SelectDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Select Difficulty:");
            Console.WriteLine("1. Easy (1-50, 10 attempts)");
            Console.WriteLine("2. Medium (1-100, 7 attempts)");
            Console.WriteLine("3. Hard (1-500, 5 attempts)");
            Console.Write("\nEnter your choice (1-3): ");
            
            string input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    currentDifficulty = DifficultyLevel.Easy;
                    break;
                case "2":
                    currentDifficulty = DifficultyLevel.Medium;
                    break;
                case "3":
                    currentDifficulty = DifficultyLevel.Hard;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow; // Part 4: Added color
                    Console.WriteLine("Invalid choice. Defaulting to Medium difficulty.");
                    Console.ResetColor();
                    currentDifficulty = DifficultyLevel.Medium;
                    break;
            }
            
            Console.WriteLine($"\nSelected difficulty: {currentDifficulty}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Part 1: Fixed random number generation by removing the seed
        private void GenerateTargetNumber()
        {
            // Fixed: Removed the seed value of 42 to get truly random numbers
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
                    Console.ForegroundColor = ConsoleColor.Green; // Part 4: Added color
                    Console.WriteLine($"\nCorrect! You guessed the number in {currentAttempts} attempts.");
                    Console.ResetColor();
                }
                else
                {
                    string hint = guess < targetNumber ? "higher" : "lower";
                    Console.ForegroundColor = ConsoleColor.Red; // Part 4: Added color
                    Console.WriteLine($"Wrong! Try a {hint} number. Attempts left: {attemptsLimit - currentAttempts}");
                    Console.ResetColor();
                }
            }
        }

        private void DisplayGameInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Part 4: Added color
            Console.WriteLine($"Difficulty: {currentDifficulty}");
            Console.WriteLine($"Guess a number between 1 and {GetRangeForDifficulty()}");
            Console.WriteLine($"You have {attemptsLimit} attempts.");
            Console.WriteLine("----------------------------------------");
            Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Yellow; // Part 4: Added color
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Console.ResetColor();
                }
                else if (guess < 1 || guess > GetRangeForDifficulty())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Part 4: Added color
                    Console.WriteLine($"Your guess must be between 1 and {GetRangeForDifficulty()}!");
                    Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Green; // Part 4: Added color
                Console.WriteLine("Congratulations! You won the game!");
                Console.ResetColor();
                
                currentPlayer.GamesWon++;
                
                // Part 3: Update player statistics with best scores
                UpdateBestScore();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // Part 4: Added color
                Console.WriteLine("Game Over! You've run out of attempts.");
                Console.WriteLine($"The number was: {targetNumber}");
                Console.ResetColor();
                
                currentPlayer.GamesLost++;
            }
            
            Console.WriteLine("----------------------------------------");
        }
        
        // Part 3: Implemented tracking of best scores
        private void UpdateBestScore()
        {
            switch (currentDifficulty)
            {
                case DifficultyLevel.Easy:
                    if (currentPlayer.BestScoreEasy == 0 || currentAttempts < currentPlayer.BestScoreEasy)
                    {
                        currentPlayer.BestScoreEasy = currentAttempts;
                    }
                    break;
                case DifficultyLevel.Medium:
                    if (currentPlayer.BestScoreMedium == 0 || currentAttempts < currentPlayer.BestScoreMedium)
                    {
                        currentPlayer.BestScoreMedium = currentAttempts;
                    }
                    break;
                case DifficultyLevel.Hard:
                    if (currentPlayer.BestScoreHard == 0 || currentAttempts < currentPlayer.BestScoreHard)
                    {
                        currentPlayer.BestScoreHard = currentAttempts;
                    }
                    break;
            }
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
            Console.BackgroundColor = ConsoleColor.DarkBlue; // Part 4: Added color
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===========================================");
            Console.WriteLine("              FINAL STATS                  ");
            Console.WriteLine("===========================================");
            Console.ResetColor();
            
            Console.WriteLine($"Player: {currentPlayer.Name}");
            Console.WriteLine($"Games Played: {currentPlayer.GamesPlayed}");
            Console.WriteLine($"Games Won: {currentPlayer.GamesWon}");
            Console.WriteLine($"Win Rate: {currentPlayer.WinRate:P2}");
            
            // Part 3 & 5: Display best scores
            DisplayBestScores();
            
            Console.WriteLine("\nThanks for playing!");
            Console.BackgroundColor = ConsoleColor.DarkBlue; // Part 4: Added color
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===========================================");
            Console.ResetColor();
        }
        
        // Part 3 & 5: Method to display best scores
        private void DisplayBestScores()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nBest Scores (fewest attempts):");
            Console.ResetColor();
            
            if (currentPlayer.BestScoreEasy > 0)
            {
                Console.WriteLine($"- Easy: {currentPlayer.BestScoreEasy} attempts");
            }
            if (currentPlayer.BestScoreMedium > 0)
            {
                Console.WriteLine($"- Medium: {currentPlayer.BestScoreMedium} attempts");
            }
            if (currentPlayer.BestScoreHard > 0)
            {
                Console.WriteLine($"- Hard: {currentPlayer.BestScoreHard} attempts");
            }
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
        public string Name { get; set; } = "Player";
        public int GamesPlayed => GamesWon + GamesLost;
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        
        // Calculate win rate as a percentage
        public double WinRate => GamesPlayed == 0 ? 0 : (double)GamesWon / GamesPlayed;

        // Part 3 & 5: Properties to track the best score for each difficulty level
        public int BestScoreEasy { get; set; }
        public int BestScoreMedium { get; set; }
        public int BestScoreHard { get; set; }
    }
}
