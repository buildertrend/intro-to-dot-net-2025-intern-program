# Introduction to Git and .NET - Compact Workshop Outline

## Course Overview
This compact workshop provides a practical introduction to Git version control and .NET development for interns at various levels in their college careers. By the end of this course, students will understand basic Git workflows and be able to build and modify simple C# console applications.

## Schedule (3 hours total)

### Session 1: Quick Intro to Git and .NET (45 minutes)
- **10 min:** Welcome and course overview
- **15 min:** Git essentials
  - What is version control
  - Basic Git commands overview
  - Clone the course repository
- **20 min:** .NET ecosystem overview
  - What is .NET
  - C# language features overview
  - Console application basics

### Session 2: Number Guessing Game Project (2 hours 15 minutes)
- **15 min:** Project introduction and requirements
- **15 min:** Code walkthrough of scaffold application
- **1 hour 15 min:** Hands-on coding session
- **30 min:** Code sharing, review, and solution discussion

## Number Guessing Game Project

### Project Overview
Students will work on a partially completed Number Guessing Game console application. The project has intentional bugs and incomplete sections that students need to fix or implement.

### Core Requirements (Must Complete)

#### Part 1: Fix Random Number Generation
- Fix the bug in the `GenerateTargetNumber()` method that causes it to generate the same sequence of numbers each time
- **Requirements:** Remove the seed value from the Random object

#### Part 2: Add Color to Console Output
- Enhance the user interface by adding color to console output
- **Requirements:**
  - Use Console.ForegroundColor and Console.BackgroundColor
  - Use different colors for success messages, errors, and regular text

### Stretch Goals (For High-Achievers)

#### Stretch Goal 1: Implement Difficulty Selection
- Add a method to allow players to select the game difficulty
- **Requirements:**
  - Create a menu for selecting Easy, Medium, or Hard difficulty levels
  - Update the game settings based on the selected difficulty

#### Stretch Goal 2: Track Player Statistics
- Add code to track and update the player's best scores for each difficulty level
- **Requirements:**
  - Implement the missing best score properties in the Player class
  - Update the best score when a player wins a game

#### Stretch Goal 3: Display High Scores
- Implement a system to display the player's best scores
- **Requirements:**
  - Add a method to display best scores in the final statistics

#### Stretch Goal 4: Game Improvements
- Enhance the game with additional features like:
  - Hint system (e.g., "hot/cold" indicators)
  - Timer to track how long it takes to guess
  - Saving/loading player profiles
  - Customizable number ranges
  - "Impossible mode" with changing target number

#### Stretch Goal 5: Multiplayer Mode
- Implement a basic turn-based multiplayer mode
- **Requirements:**
  - Allow two players to take turns guessing
  - Track separate statistics for each player
  - Declare a winner at the end

## Resources
- Sample code with TODO comments
- Reference solutions (available if students get stuck)

## Assessment
Success is measured by:
- Completion of Core Requirements (Parts 1-2) 
- At least one Stretch Goal attempted
- Proper use of Git for version control
- Code quality and functionality
