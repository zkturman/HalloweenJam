# <p align="center">How to Contribute to Halloween Jam</p>

## Table of Contents

1. [Overview](#Overview)
1. [Making Documentation Changes](#Documentation)
1. [Adding Files to GitHub](#AddingFiles)
1. [How to Download and Run Locally](#HowToDownloadAndRun)
    - [Downloading the Project](#DownloadingTheProject)
        - [Using Git + Command Line](#UsingGit)
        - [Using SourceTree](#UsingSourceTree)
    - [Running the Project](#RunningTheProject)
1. [How to Make Local Changes](#HowToMakeChanges)
    - [Using Git + Command Line](#ChangesUsingGit)
    - [Using SourceTree](#ChangesUsingSourceTree)
1. [Submit Changes with Pull Requests](#CreatePullRequest)

## <a name="Overview"></a> Overview

Below are guides on how make different types of changes. If you are adding files or updating documentation, there is no need to download the whole project. If you are adding content into the actual game, programming, or testing, you'll need to refer to the download instructions.

If you need help with your changes, reach out to the project owner.

## <a name="Documentation"></a> Making Documentation Changes

Documentation is one of the key areas that keep projects like HalloweenJam running smoothly. As multiple people are working on the project, being able to find information is crucial to keep game development efficient.

If working with a shareable document, such as Google Docs or Kanban boards in Trello, simply follow the appropriate link and start editing. Certain documents may limit direct edits to subsets of people. If you feel you should have security to make changes and cannot, reach out to the project owner.

If you are making changes to GitHub documentation, such as README.md, follow these steps:

1. In the upper-left corner of GitHub, there is a dropdown menu with branch names, a button labelled 'branches', and a button labelled 'tags'. In the dropdown menu, make sure 'development' is selected.
1. Find and click/open to the file in GitHub
1. In the upper-right corner, click the pencil icon
1. Make your changes
1. Click the 'Preview' button to review your changes
1. Scroll to the bottom of the screen and enter a message briefly describing your changes
    - This is the smaller box, not the 'Add an optional extended description...'
1. Ensure the 'Commit directly to the development branch' radio button is selected
1. Click the 'Commit changes' button
1. [Create and submit a Pull Request](#CreatePullRequest)

## <a name="AddingFiles"></a> Adding Files to GitHub

If adding content, such as artwork or sound files, you may not need to download the whole project. Instead you may add these files directly to GitHub. You can add files by following these steps:

1. In the upper-left corner of GitHub, there is a dropdown menu with branch names, a button labelled 'branches', and a button labelled 'tags'. In the dropdown menu, make sure 'development' is selected.
1. Navigate to the folder in GitHub where you want to upload the file
1. In the upper-right corner, click 'Add file >> Upload files'
1. Either click 'choose your files' and browse to your desired files or drag and drop the files
1. At the bottom of the screen, enter a message briefly describing your changes
    - This is the smaller box, not the 'Add an optional extended description...'
1. Ensure the 'Commit directly to the development branch' radio button is selected
1. Click the 'Commit changes' button
1. [Create and submit a Pull Request](#CreatePullRequest)

## <a name="HowToDownloadAndRun"></a> How to Download and Run Locally

### <a name="DownloadingTheProject"></a> Downloading the Project

#### <a name="UsingGit"></a> Using Git + Command Line

For this method, you will need to have Git installed on your computer. Git is a free source-control software. If not already installed on your computer, [it can be downloaded here](https://git-scm.com/).

1. Open either PowerShell or a terminal window
1. ```CD``` to the directory you want to store your copy of this project
1. Enter ```git clone https://github.com/zkturman/HalloweenJam.git```
    - You may be prompted for credentials at this stage. You can set these properties using a [one-time command](https://git-scm.com/book/en/v2/Getting-Started-First-Time-Git-Setup).
        - ```git config --global user.name "John Doe"```
        - ```git config --global user.email johndoe@example.com```
    - You may also be prompted for GitHub credentials or a personal access token. Following [the steps here](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) will help you configure Git to prevent continued harassment.
        - Treat this token like a password and keep it CONFIDENTIAL.
        - Git should remember this token once you successfully complete an action using the token and restart the terminal.
1. Enter ```git checkout development```

#### <a name="UsingSourceTree"></a> Using SourceTree

For this method, you will need to have SourceTree installed on your computer. SourceTree is a free Git GUI (as in, you don't need the command line). [It can be downloaded here.](https://www.sourcetreeapp.com/) If you haven't already installed Git, you will need to install it as well. [Git can be downloaded here.](https://git-scm.com/)

1. Open SourceTree
1. Open a new tab and click the 'Clone' button
1. In 'Source Path', enter ```https://github.com/zkturman/HalloweenJam.git```
    - Note: this can be copied above by clicking the grey/green Code button and copying the HTTPS URL
1. In 'Destination Path', enter the location on your computer you want this repository to be copied into
1. In 'Name', enter the name you want for your local project folder. E.g., HalloweenJam
1. Click 'Advanced options' and change 'Checkout branch' to 'development'
1. Click the 'Clone' button

### <a name="RunningTheProject"></a> Running the Project

To run this project, you will need UnityHub installed on your computer. [It can be downloaded here.](https://unity.com/download) Once downloaded, you will need to download Unity version 2021.3.11f1. To install this version, follow these steps:

1. Open UnityHub
1. Click the 'Installs' tab (on the left)
1. Click the 'Install Editor' button
1. Locate version 2021.3.11.f1 in the 'Official Releases' tab
1. Click the 'Install' button

To open the HalloweenJam project:

1. Open UnityHub
1. Click the 'Projects' tab on the left (if not already selected)
1. Click 'Open' >> 'Add Project from Disk'
1. Select your HalloweenJam folder
1. Click 'Add Project'
1. If the project does not open automatically, click the HalloweenJam row in UnityHub to open the project.

Currently, there are no special run or build instructions. If you would like to run the game from the start, follow these steps:

1. Navigate to the Assets\\Scenes\\ directory within Unity
1. Double click 'HomeScene'
1. Click the Play button at the top of the Unity window

## <a name="HowToMakeChanges"></a> How to Make Local Changes

After following the instruction in [How to Download and Run](#HowToDownloadAndRun), you can make the desired changes on your machine within your copy of the project.

**NOTE: YOU SHOULD ALWAYS PERFORM A PULL BEFORE MAKING CHANGES TO AVOID CONFLICTS**
- In Git, ```git pull```
- In SourceTree, click the 'Pull' button

Once finished with your changes, you will need to perform some actions to get them onto GitHub (called 'pushing'). Once your changes are 'pushed', you can create a Pull Request as described below. We will then review your changes and add them to the ```main``` branch of source code.

### <a name="ChangesUsingGit"></a> Using Git + Command Line

1. Open either PowerShell or a terminal window
1. ```CD``` to your HalloweenJam directory
1. Enter ```git status``` and review all the files you've made changes to.
    - If you have unwanted changes, you can undo all changes to a file using this command: ```git checkout HEAD -- my-file.extension```
1. To prepare these changes for saving, enter ```git add .```, which will add all your local changes.
    - If you would like to add only some changes, replace ```.``` with a filename.
1. To officially save these changes, enter ```git commit -m "COMMIT MESSAGE THAT DESCRIBES YOUR CHANGES"```
1. Finally, enter ```git push``` to send those changes to GitHub.
    - If Git says your local repository is not up to date with remote, you must enter ```git pull``` first. This command will update your local files with all new changes to the repository. If this creates merge conflicts, you will need to fix them and redo these steps. See [here](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/addressing-merge-conflicts/about-merge-conflicts) for help. There are other strategies for handling this scenario, but approach with caution.
1. [Create and submit a Pull Request](#CreatePullRequest)

### <a name="ChangesUsingSourceTree"></a> Using SourceTree

1. Open SourceTree
1. Open your HalloweenJam tab/repository
1. Click the 'Commit' button and add a message that describes your changes
1. Click the 'Stage All' button, or select certain files and click the 'Stage Selected' button
1. Tick the 'Push changes immediately to <BRANCHNAME>' checkbox
1. Click the 'Commit' button
    - If your repository is out of date, you may need to click the 'Pull' button first. If this creates merge conflicts, you will need to fix them and redo these steps. See [here](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/addressing-merge-conflicts/about-merge-conflicts) for help.
1. [Create and submit a Pull Request](#CreatePullRequest)

## <a name="CreatePullRequest"></a> Submit Changes with Pull Requests

Creating a Pull Request is how you get your changes incorporated into the main project. All changes are initially added to a development copy (i.e. the 'development' branch) of the project, and a Pull Request is the process needed to add changes from this copy to the main project.

In GitHub, click the 'Pull requests' tab above, or [this link](https://github.com/zkturman/HalloweenJam/pulls). If your changes are recent, you may see a banner stating so, and you can follow these steps:
1. Click the 'Compare & pull request' button
1. Scroll down to review the changes are yours
1. Add a brief comment describing your changes
1. Click the 'Create pull request' button
1. On the right, under the Reviewers section, add someone and contact them letting you know you've created a pull request.
1. Someone will review and (hopefully) add your changes to ```main```

If you do not see a banner concerning your changes, follow these steps:
1. Click the 'Pull requests' tab at the top of the screen
1. Click the 'New pull request' button
1. Click the branch with your changes (usually ```development```)
1. Review the changes to make sure they're your changes
1. Click the 'Create pull request' button
1. Add a brief comment describing your changes
1. Click the 'Create pull request' button
1. On the right, under the Reviewers section, add someone and contact them letting you know you've created a pull request.
1. Someone will review and (hopefully) add your changes to ```main```

[<p align="center">Back to Home</p>](README.md)
