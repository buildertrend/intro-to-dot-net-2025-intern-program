using System;


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
        private List<Player> currentPlayers;
        private int targetNumber;
        private int attemptsLimit;
        private int currentAttempts;
        private bool gameWon;
        private DifficultyLevel currentDifficulty;

        private Player currentPlayer
        {
            get { return currentPlayers[currentAttempts % currentPlayers.Count]; }
        }

        // Constructor to initialize the game
        public GameManager()
        {
            currentPlayers = new List<Player>();
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
                SelectGameDifficulty();

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
            Console.WriteLine("===========================================");
            Console.WriteLine("   Welcome to the NUMBER GUESSING GAME!   ");
            Console.WriteLine("===========================================");

            PlayersSelect();

            Console.Write("\nHello");
            foreach (Player player in currentPlayers)
            {
                Console.Write($", {player.Name}");
            }
            
            Console.WriteLine("\nI'm thinking of a number... Can you guess it?");
            Console.WriteLine("\nPress any key to start!");
            Console.ReadKey();
            Console.Clear();
        }

        private void PlayersSelect()
        {
            int players = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPlease enter how many players (1 - 4): ");

                string input = Console.ReadLine();
                validInput = int.TryParse(input, out players);

                if (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
                else if (players < 1 || players > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The players must be between 1 and 4!");
                    validInput = false;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }

            for (int i = 0; i < players; i++)
            {
                Player player = new Player();

                Console.WriteLine($"\nPlayer {i + 1}, please enter your name:");
                string playerName = Console.ReadLine();

                player.Name = string.IsNullOrWhiteSpace(playerName) ? $"Player {i + 1}" : playerName;
                currentPlayers.Add(player);
            }
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

            attemptsLimit *= currentPlayers.Count;
            currentAttempts = 0;
            gameWon = false;
        }

        private void SelectGameDifficulty()
        {
            int selection = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Select a difficulty: ");
                Console.WriteLine($"1. Easy\n2. Medium\n3. Hard");

                string input = Console.ReadLine();
                validInput = int.TryParse(input, out selection);

                if (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
                else if (selection < 1 || selection > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The difficulty must be between 1 and 3!");
                    validInput = false;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }

            currentDifficulty = (DifficultyLevel)(selection - 1);
        }

        private void PlayGame()
        {
            Console.Clear();
            DisplayGameInfo();

            while (currentAttempts < attemptsLimit && !gameWon)
            {
                int guess = GetUserGuess();

                if (guess == targetNumber)
                {
                    gameWon = true;
                    // TODO: Add color to the console output (Part 4)
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nCorrect! {currentPlayer.Name} guessed the number in {currentAttempts} attempts.");
                    
                    if (currentPlayer.BestScore < currentAttempts)
                    {
                        currentPlayer.BestScore = currentAttempts;
                    }
                }
                else
                {
                    // TODO: Add color to the console output (Part 4)
                    currentAttempts++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    string hint = guess < targetNumber ? "higher" : "lower";
                    Console.WriteLine($"Wrong! Try a {hint} number. Attempts left: {attemptsLimit - currentAttempts}");
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void DisplayGameInfo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Difficulty: {currentDifficulty}");
            Console.WriteLine($"Guess a number between 1 and {GetRangeForDifficulty()}");
            Console.WriteLine($"You have {attemptsLimit} attempts.");
            Console.ForegroundColor = ConsoleColor.White;
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\nAttempt {currentAttempts + 1}/{attemptsLimit}. {currentPlayer.Name} enter your guess: ");
                string input = Console.ReadLine();

                validInput = int.TryParse(input, out guess);

                if (!validInput)
                {
                    // TODO: Add color to the console output (Part 4)
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
                else if (guess < 1 || guess > GetRangeForDifficulty())
                {
                    // TODO: Add color to the console output (Part 4)
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Your guess must be between 1 and {GetRangeForDifficulty()}!");
                    validInput = false;
                }
                Console.ForegroundColor = ConsoleColor.White;
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
                Console.WriteLine($"Congratulations! {currentPlayer.Name} won the game!");
                currentPlayer.GamesWon++;

                foreach (Player player in currentPlayers)
                {
                    if (player != currentPlayer)
                    {
                        player.GamesLost++;
                    }
                }

                // TODO: Update player statistics (Part 3)
                // Update the best score for the current difficulty level
            }
            else
            {
                // TODO: Add color to the console output (Part 4)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Over! You've run out of attempts. Everyone lost");
                Console.WriteLine($"The number was: {targetNumber}");

                foreach (Player player in currentPlayers)
                {
                    player.GamesLost++;
                }

                // TODO: Update player statistics (Part 3)
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------");

        }

        private bool AskToPlayAgain()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nWould you like to play again? (Y/N): ");
            string response = Console.ReadLine().Trim().ToUpper();
            Console.ForegroundColor = ConsoleColor.White;
            return response == "Y" || response == "YES";
        }

        private void DisplayFinalStats()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===========================================");
            Console.WriteLine("              FINAL STATS                  ");
            Console.WriteLine("===========================================");

            foreach (Player player in currentPlayers)
            {
                Console.WriteLine($"Player: {player.Name}");
                Console.WriteLine($"Games Played: {player.GamesPlayed}");
                Console.WriteLine($"Games Won: {player.GamesWon}");
                Console.WriteLine($"Best Score: {player.BestScore}");
                Console.WriteLine($"Win Rate: {player.WinRate:P2}");
                Console.WriteLine("===========================================");
            }

            // TODO: Display best scores (Part 3 & 5)
            Console.ForegroundColor = ConsoleColor.White;
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

        public int BestScore { get; set; }

        // Calculate win rate as a percentage
        public double WinRate => GamesPlayed == 0 ? 0 : (double)GamesWon / GamesPlayed;

        // TODO: Implement tracking of best scores (Part 3 & 5)
        // Add properties to track the best score (fewest attempts) for each difficulty level
    }
}
