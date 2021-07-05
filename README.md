# Connect The Dots Clone
 This is a replication of the Connect the Dots game

# Unity Version : 2020.2.6f1

# Instructions to Play:
## After opening the project change the window aspect ratio to 16:9. Then hit Play to play the game.

# Game Flow
* Game starts with the Level Selection screen
* The user can select the level to start playing.
* The objective of the game is to connect to the same color node to each other

# Win Condition
* Connect all nodes with their respective colors and cover the entire board to solve each puzzle (the board should not be empty) after connecting all the respective nodes to each other.
* The connection will break if they cross or overlap some other colors.

# Game Structure

 # GameManager Class : 
 ## This class contains all the public references to other calsses
      
 # GameController Class:  
 ## This class contains the game logic
      
 # DotController Class: 
 ## This class contains the properties of the individual dots
      
 # LevelController Class: 
 ## This class is used to read the json data and then generate the level based on the data from the json
      # CreateLevel()     This method is used to read the json file from the Resources folder and then use the data from it to place the dots on the grids
      
 # LevelManager Class: 
 ## This class is used to carry the level number to the game scene so that the level generator can use this to generate the scene from the json file
 
 # LineController Class: 
 ## This class manages the line renderers
      # ResetLines()        This method checks whether the gird is a end grid. If so finishes the line or deletes the current drawn line
      # ResetLevel()        Resets the current level so that the user can start over
      # GetLinesCount()     This method is Used to get the count of drawn lines
      
 # LoadLevel Class: 
 ## This class is used to Load a level from the Level selector screen
 
