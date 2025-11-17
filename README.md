The Brewing Game is an educational game used by engineering students on their 2nd and 3rd year. The educational point of the game is conveying the usefulness of precision instrumentation while absolving repeated, precision tasks. The game uses a well-known environment, such as a brewery, to help the player practically learn about the effectiveness of utilising tools. The educational game loop should be short and concise:
1.	The player needs to produce beer to acquire points by interacting with a mash tun
2.	Their task is to fill the mash tun with the correct amount of water with no aid and brew the beer
3.	The quality of the product will determine how many points they will get for the batch
4.	After reaching the correct number of points they can upgrade the mash tun with correct instrumentation
5.	The player can appreciate the positive difference in utilising the correct tool for the action, being able to create a better-quality product with less effort
The game uses 3D assets and an isometric fixed view on the scene, the player can interact with the UI elements and game object using the mouse.


#### Functional Requirements
Functional requirements have been defined deriving from the user and task analysis document and client’s brief.
###### Game 
1.	The system should allow the player to start the game from main menu (title screen)
2.	The system should offer an input to allow the player to pick appropriate pronouns
3.	The system should display a guided tutorial 
4.	The system should provide a complete gameplay loop:
•	Receive brewing task
•	Fill mash tun with water
•	Brew batch
•	Collect beer produced
•	Receive points as product quality indicator
•	Unlock instrumentation upgrade
•	Brew new batch with instrumentation aid
5.	The system should allow the player to brew few times
6.	The system should allow the player to upgrade the mash tun with a water level sensor
7.	The system should allow the player to brew with upgraded tun to appreciate the difference
4.	Interface Requirements
The system will be interface-driven, all interactions should be possible using a mouse.
##### User Interface
8.	The system should include:
•	Main Menu
•	Pronoun selection screen
•	Pause Menu
•	Heads-Up Display (in game interface)
9.	The system should allow interactions with objects via mouse click
10.	The HUD should display:
•	Pause menu icon
•	Tutorial script icon
•	Selected object’s:
i.	Sprite
ii.	Valve button
iii.	Brew button
iv.	Collect button
v.	Upgrade button (when available)
vi.	Infill level (when upgraded)
5.	Performance Requirements
The game build will target different operating systems:
•	Windows (11)
•	MacOS
•	Linux (Ubuntu)
The game should perform well on lower end machines.
