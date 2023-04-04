# **Football Tables**
The Football Tables Project is a C# console application that allows you to input match results and generate league tables for a football league. This project was developed as part of a software development course.
## **Usage**
To use the application, you must have the following files:
- **teams.csv:** This file contains information about the teams in the league.
- **rounds:** This directory contains CSV files for each round of the league, which include the match results.

To generate a league table, run the application and follow the prompts to input the file paths for the teams.csv and rounds directory. The application will parse the CSV files and generate a league table.

## **Outlining of the Solution**

The Football Tables Project consists of the following components:

- **Team.cs:** This class defines the attributes and methods for a team in the league.
- **Round.cs:** This class defines the attributes and methods for a round in the league.
- **League.cs:** This class defines the attributes and methods for the league, including the generation of the league table.
- **Program.cs:** This class is the entry point for the console application, which prompts the user for file paths and generates the league table.

# **Rule/Error Help Information**

The Football Tables Project follows the following rules and error handling:

- The teams.csv file must contain the following columns: "Abbreviation", "Full Club Name", and "Special Ranking".
- The rounds CSV files must contain the following columns: "Home Team", "Away Team", "Score", "Home Team Goals", and "Away Team Goals".
- The "Score" column in the rounds CSV files must follow the format "X-X", where X is a positive integer.
- If the rounds CSV files contain incomplete match information (i.e., missing goals), the missing values will be assumed to be 0.
- If the teams.csv or rounds CSV files cannot be found or opened, the application will return a file not found error.
- If the input data in the CSV files is incorrect or invalid, the application will return a format exception error.
- If there are duplicate team abbreviations in the teams.csv file or duplicate round CSV files, the application will return a key not found error.

