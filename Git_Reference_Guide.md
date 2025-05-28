# Comprehensive Git Reference Guide

## Introduction to Git

Git is a distributed version control system that enables tracking changes in source code during software development. It was created by Linus Torvalds in 2005 for Linux kernel development and has since become the most widely used version control system in the world.

## Key Concepts

### Basic Terms

| Term | Definition |
|------|------------|
| **Repository (Repo)** | A storage location for a project containing all files and their complete history |
| **Working Directory** | Your local directory containing project files at a specific state |
| **Staging Area (Index)** | Intermediate area where changes are added before committing |
| **Commit** | A snapshot of your project at a specific point in time with a unique identifier (hash) |
| **Branch** | A separate line of development that diverges from the main project |
| **HEAD** | A pointer to the current commit you're viewing/working on |
| **Remote** | A version of your repository hosted on a server (like GitHub, GitLab) |
| **Clone** | Creating a local copy of a remote repository |
| **Fork** | Creating a personal copy of someone else's project on a server |

### Git's Three-Tree Architecture

1. **Working Directory** - Where you modify files
2. **Staging Area (Index)** - Where you mark files for the next commit
3. **Repository** - Where Git permanently stores changes as commits

## Basic Workflow Commands

### Setting Up

```bash
# Configure user information
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"

# Configure default editor
git config --global core.editor "code --wait"  # For VS Code

# Initialize a new repository
git init

# Clone an existing repository
git clone https://github.com/username/repository.git
```

### Daily Workflow

```bash
# Check status of your working directory
git status

# Add files to staging area
git add filename.txt          # Add specific file
git add .                     # Add all files
git add *.cs                  # Add all C# files
git add src/                  # Add all files in directory

# Commit changes
git commit -m "Your commit message"
git commit -a -m "Commit message"  # Add tracked files and commit

# View commit history
git log
git log --oneline             # Compact view
git log --graph --oneline     # Visual representation with branches
```

### Working with Remotes

```bash
# Add a remote
git remote add origin https://github.com/username/repo.git

# View remotes
git remote -v

# Fetch changes from remote
git fetch origin

# Pull changes (fetch + merge)
git pull origin main

# Push changes to remote
git push origin main

# Push a new branch to remote
git push -u origin new-branch
```

### Branching and Merging

```bash
# List branches
git branch

# Create a new branch
git branch new-feature

# Switch to a branch
git checkout new-feature
git switch new-feature        # Git 2.23+

# Create and switch in one command
git checkout -b new-feature
git switch -c new-feature     # Git 2.23+

# Merge branch into current branch
git merge feature-branch

# Delete a branch
git branch -d feature-branch  # Safe delete (if merged)
git branch -D feature-branch  # Force delete
```

## Intermediate Git Commands

### Inspecting Changes

```bash
# View changes between working directory and staging area
git diff

# View changes between staging area and last commit
git diff --staged

# View changes between two commits
git diff commit1..commit2

# View changes in a specific file
git diff -- path/to/file
```

### Undoing Changes

```bash
# Unstage a file
git restore --staged filename.txt  # Git 2.23+
git reset HEAD filename.txt        # Older Git versions

# Discard changes in working directory
git restore filename.txt           # Git 2.23+
git checkout -- filename.txt       # Older Git versions

# Amend the last commit
git commit --amend -m "New commit message"

# Revert a commit (creates new commit that undoes changes)
git revert commit-hash

# Reset to a previous commit (dangerous for shared branches!)
git reset --soft commit-hash   # Keep changes staged
git reset --mixed commit-hash  # Keep changes unstaged (default)
git reset --hard commit-hash   # Discard changes
```

### Temporarily Stashing Changes

```bash
# Save changes without committing
git stash

# List stashes
git stash list

# Apply most recent stash
git stash apply

# Apply specific stash
git stash apply stash@{2}

# Apply and remove stash
git stash pop

# Create a new branch from stash
git stash branch new-branch
```

## Advanced Git Concepts

### Rebasing

Rebasing rewrites commit history by moving commits from one branch to another.

```bash
# Basic rebase
git checkout feature
git rebase main

# Interactive rebase
git rebase -i HEAD~3  # Interactively modify last 3 commits
```

During interactive rebasing, you can:
- `pick` - Keep the commit as is
- `reword` - Change commit message
- `edit` - Pause to amend the commit
- `squash` - Combine with previous commit and keep messages
- `fixup` - Combine with previous commit and discard message
- `drop` - Remove the commit

### Cherry-picking

Apply specific commits from one branch to another.

```bash
# Apply a specific commit to current branch
git cherry-pick commit-hash

# Cherry-pick multiple commits
git cherry-pick commit1 commit2

# Cherry-pick without committing
git cherry-pick -n commit-hash
```

### Reflog

Git reference logs record when the tips of branches are updated.

```bash
# View reflog
git reflog

# Recover deleted commits or branches
git checkout HEAD@{2}  # Go back 2 ref changes
```

### Submodules

Include external repositories within your project.

```bash
# Add a submodule
git submodule add https://github.com/user/repo path/to/submodule

# Initialize submodules after cloning
git submodule init
git submodule update

# Clone repository with submodules
git clone --recurse-submodules https://github.com/user/repo
```

## Git Workflows

### Feature Branch Workflow

1. Create a feature branch from main
2. Develop the feature in isolation
3. Push feature branch to remote
4. Create pull request for review
5. Merge into main after approval

```bash
git checkout -b feature-x main
# Make changes
git add .
git commit -m "Add feature X"
git push -u origin feature-x
# Create pull request through web interface
```

### Gitflow Workflow

- **main** - Production code
- **develop** - Integration branch for features
- **feature/** - New features
- **release/** - Preparing for release
- **hotfix/** - Emergency fixes for production

### Simplified Workflow (for this workshop)

1. Clone repository
2. Make changes
3. Commit with descriptive messages
4. Push changes

```bash
git clone https://github.com/instructor/workshop-repo.git
cd workshop-repo
# Make changes
git add .
git commit -m "Fix random number generation"
git push
```

## Best Practices

### Commit Messages

- Use present tense ("Add feature" not "Added feature")
- First line: concise summary (50 chars or less)
- Followed by blank line
- Detailed explanation if necessary

Example:
```
Fix random number generation bug

- Remove seed value from Random constructor
- Add tests to verify randomness
- Update documentation to explain the fix
```

### Commit Frequency

- Commit logical, complete changes
- Avoid "WIP" (Work In Progress) commits
- Create atomic commits that address one issue

### When to Create Branches

- For new features
- For bug fixes
- For experimental changes
- For version releases

### Code Reviews

- Keep pull requests small and focused
- Review your own changes first
- Be specific in review comments
- Automate checks where possible

## Git for Visual Studio Users

### Visual Studio Integration

- **Team Explorer** tab provides Git functionality
- Commit changes using the Changes view
- Manage branches from Branch view
- View history and compare files

### Visual Studio Code Integration

- Source Control icon in sidebar
- Stage changes with + icon
- Commit with checkmark icon
- Push/pull with arrows icon
- Built-in diff viewer

## Troubleshooting Common Issues

### Merge Conflicts

When Git can't automatically merge changes:

1. Open conflicted files (look for `<<<<<<<`, `=======`, `>>>>>>>` markers)
2. Edit files to resolve conflicts
3. Add resolved files
4. Commit the merge

```bash
# After encountering merge conflict
git status  # Shows conflicted files
# Edit files to resolve conflicts
git add resolved-file.txt
git commit  # Completes the merge
```

### Detached HEAD State

When HEAD points directly to a commit instead of a branch:

```bash
# Create new branch at current position
git switch -c new-branch

# Or return to a branch
git switch main
```

### Authentication Issues

For HTTPS authentication:
```bash
# Store credentials
git config --global credential.helper store  # Less secure, saves plaintext
git config --global credential.helper cache  # Temporarily store in memory
```

For SSH authentication:
```bash
# Generate SSH key
ssh-keygen -t ed25519 -C "your.email@example.com"

# Add key to SSH agent
eval "$(ssh-agent -s)"
ssh-add ~/.ssh/id_ed25519

# Add public key to GitHub/GitLab account
```

## Git Resources

### Official Documentation
- [Git Documentation](https://git-scm.com/doc)
- [Git Book](https://git-scm.com/book)

### Interactive Learning
- [Learn Git Branching](https://learngitbranching.js.org/)
- [Visualizing Git](http://git-school.github.io/visualizing-git/)

### Cheat Sheets
- [GitHub Git Cheat Sheet](https://education.github.com/git-cheat-sheet-education.pdf)
- [Atlassian Git Cheat Sheet](https://www.atlassian.com/git/tutorials/atlassian-git-cheatsheet)
