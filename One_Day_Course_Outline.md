# Introduction to Git and .NET - One-Day Course Outline

## Course Overview
This one-day course provides a practical introduction to Git version control and .NET development for interns at various levels in their college careers. By the end of this course, students will understand basic Git workflows and be able to build and modify simple C# console applications.

## Schedule (6 hours total)

### Session 1: Introduction to Git (1.5 hours)
- **15 min:** Welcome and course overview
- **30 min:** Git basics concepts
  - What is version control
  - Repositories, commits, branches
  - Setting up Git
- **45 min:** Hands-on Git practice
  - Configure Git
  - Create a repository
  - Make changes, commit, and check history
  - Clone the course repository

### Session 2: Introduction to .NET and C# (1.5 hours)
- **30 min:** .NET ecosystem overview
  - What is .NET
  - C# language features review
  - Console application basics
- **60 min:** Basic C# programming
  - Creating a simple console application
  - Variables, control structures, methods
  - Reading/writing to console

### Session 3: Number Guessing Game Project (3 hours)
- **15 min:** Project introduction and requirements
- **15 min:** Code walkthrough of scaffold application
- **2 hours:** Hands-on coding session
- **30 min:** Code review and solution discussion

## Number Guessing Game Project

### Project Overview
Students will work on a partially completed Number Guessing Game console application. The project has intentional bugs and incomplete sections that students need to fix or implement.

### Parts to Complete

#### Part 1: Fix Random Number Generation
- Fix the bug in the `GenerateTargetNumber()` method that causes it to generate the same sequence of numbers each time
- **Requirements:** Remove the seed value from the Random object

#### Part 2: Implement Difficulty Selection
- Add a method to allow players to select the game difficulty
- **Requirements:**
  - Create a menu for selecting Easy, Medium, or Hard difficulty levels
  - Update the game settings based on the selected difficulty

#### Part 3: Track Player Statistics
- Add code to track and update the player's best scores for each difficulty level
- **Requirements:**
  - Implement the missing best score properties in the Player class
  - Update the best score when a player wins a game

#### Part 4: Add Color to Console Output
- Enhance the user interface by adding color to console output
- **Requirements:**
  - Use Console.ForegroundColor and Console.BackgroundColor
  - Use different colors for success messages, errors, and regular text

#### Part 5: Stretch Goal - Display High Scores
- Implement a system to display the player's best scores
- **Requirements:**
  - Add a method to display best scores in the final statistics

## Resources
- Sample code with TODO comments
- Reference solutions (available if students get stuck)

## Assessment
Success is measured by:
- Completion of Parts 1-4 of the Number Guessing Game
- Proper use of Git for version control
- Code quality and functionality
