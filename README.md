# Git & .NET Workshop - Student Handout

## Workshop Overview
This 3-hour workshop will introduce you to Git version control and .NET development through a hands-on project. You'll be enhancing a Number Guessing Game console application using C#.

## Schedule
- **0:00-0:10:** Welcome & Introductions
- **0:10-0:25:** Git Essentials
- **0:25-0:45:** .NET & C# Overview
- **0:45-1:00:** Project Introduction
- **1:00-2:15:** Hands-on Coding
- **2:15-2:45:** Code Review & Discussion
- **2:45-3:00:** Wrap-up & Resources

## Essential Git Commands

| Command | Description | Example |
|---------|-------------|---------|
| `git clone [url]` | Create a local copy of a remote repository | `git clone https://github.com/username/repo.git` |
| `git status` | Check the status of files in your repository | `git status` |
| `git add [file]` | Stage changes for commit | `git add Program.cs` or `git add .` (all files) |
| `git commit -m "[message]"` | Save your changes with a descriptive message | `git commit -m "Fix random number generation"` |
| `git push` | Upload local changes to remote repository | `git push` |
| `git pull` | Download changes from remote repository | `git pull` |

## C# Quick Reference

### Console Input/Output
```csharp
// Output text
Console.WriteLine("Hello, World!");  // With line break
Console.Write("Enter your name: ");  // Without line break

// Input text
string input = Console.ReadLine();

// Parse input to numbers
int number;
bool success = int.TryParse(input, out number);

// Formatted output
Console.WriteLine($"Hello, {input}!");  // String interpolation
Console.WriteLine("Score: {0}/{1}", correct, total);  // Composite formatting
```

### Console Colors
```csharp
// Change text color
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success message");

// Change background color
Console.BackgroundColor = ConsoleColor.Blue;
Console.WriteLine("Highlighted text");

// Reset colors
Console.ResetColor();
Console.WriteLine("Normal text");

// Available colors:
// Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow,
// Gray, DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
```

### Random Numbers
```csharp
// Create a random number generator
Random random = new Random();  // Don't use a seed value!

// Generate random integers
int number = random.Next();         // Any non-negative integer
int number = random.Next(100);      // Between 0 and 99
int number = random.Next(1, 101);   // Between 1 and 100
```

### Control Structures
```csharp
// If-else statement
if (condition) {
    // Code to execute if condition is true
} else if (anotherCondition) {
    // Code to execute if anotherCondition is true
} else {
    // Code to execute if all conditions are false
}

// Switch statement
switch (value) {
    case 1:
        // Code for value 1
        break;
    case 2:
        // Code for value 2
        break;
    default:
        // Code for any other value
        break;
}

// While loop
while (condition) {
    // Code executes repeatedly while condition is true
}

// For loop
for (int i = 0; i < 10; i++) {
    // Code executes 10 times (0 through 9)
}
```

## Number Guessing Game Project

### Core Requirements

#### Part 1: Fix Random Number Generation
- Find the bug in the `GenerateTargetNumber()` method
- Remove the seed value from the Random object constructor
- Look for: `Random random = new Random(42);`

#### Part 2: Add Color to Console Output
- Use different colors for different message types:
  - Success messages (green)
  - Error/wrong guess messages (red)
  - Informational messages (cyan or yellow)
  - Normal text (default white)
- Look for the TODO comments with "Part 4" that mention color

### Stretch Goals (Choose any that interest you)

#### 1. Implement Difficulty Selection
- Add a method to choose difficulty at game start
- Update game settings based on difficulty

#### 2. Track Player Statistics
- Implement best score properties
- Update scores when player wins

#### 3. Display High Scores
- Show best scores in final statistics screen

#### 4. Game Improvements
- Add hints, timers, or other enhancements

#### 5. Multiplayer Mode
- Implement turn-based play for two players

## Testing Your Application

1. **Does the random number change?**
   - Run the game multiple times
   - You should get different target numbers

2. **Is the console output colorful?**
   - Error messages should be in red
   - Success messages should be in green
   - Different message types should use different colors

3. **Does the game work correctly?**
   - Can you guess correctly?
   - Do you get appropriate feedback?
   - Are wins and losses tracked?

## Additional Resources

### Git
- [Git Documentation](https://git-scm.com/doc)
- [GitHub Git Cheat Sheet](https://education.github.com/git-cheat-sheet-education.pdf)

### C# and .NET
- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Console Class Documentation](https://docs.microsoft.com/en-us/dotnet/api/system.console)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/)
