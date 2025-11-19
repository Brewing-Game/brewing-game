  

# 1.  Introduction

This should serve as a living document for the development team of The Brewing Game, as well as a reference for clients.

The document will contain details of the software requirements and design, and should help readers acknowledge the product’s requirements, development environment, user stories, design choices, as well as constraints. The project will be developed with no costs, utilising free tools and resources. The game should be ready for playtesting on 30/03/2026.

# 2.  General description

The Brewing Game is an educational game used by engineering students on their 2nd and 3rd year. The educational point of the game is conveying the usefulness of precision instrumentation while absolving repeated, precision tasks. The game uses a well-known environment, such as a brewery, to help the player practically learn about the effectiveness of utilising tools. The educational game loop should be short and concise:

1.      The player needs to produce beer to acquire points by interacting with a mash tun

2.      Their task is to fill the mash tun with the correct amount of water with no aid and brew the beer

3.      The quality of the product will determine how many points they will get for the batch

4.      After reaching the correct number of points they can upgrade the mash tun with correct instrumentation

5.      The player can appreciate the positive difference in utilising the correct tool for the action, being able to create a better-quality product with less effort

The game uses 3D assets and an isometric fixed view on the scene, the player can interact with the UI elements and game object using the mouse.

# 3.  Functional Requirements

Functional requirements have been defined deriving from the user and task analysis document and client’s brief.

## 3.1 Game

1.      The system should allow the player to start the game from main menu (title screen)

2.      The system should display a guided tutorial

3.      The system should provide a complete gameplay loop:

·         Receive brewing task

·         Fill mash tun with water

·         Brew batch

·         Collect beer produced

·         Receive points as product quality indicator

·         Unlock instrumentation upgrade

·         Brew new batch with instrumentation aid

4.      The system should allow the player to brew few times

5.      The system should allow the player to upgrade the mash tun with a water level sensor

6.      The system should allow the player to brew with upgraded tun to appreciate the difference

# 4.  Interface Requirements

The system will be interface-driven, all interactions should be possible using a mouse.

## 4.1  User Interface

7.      The system should include:

·         Main Menu

·         Pause Menu

·         Heads-Up Display (in game interface)

8.      The system should allow interactions with objects via mouse click

9.  The HUD should display:

·         Pause menu icon

·         Tutorial script icon

·         Selected object’s:

                                                              i.      Sprite

                                                             ii.      Valve button

                                                           iii.      Brew button

                                                           iv.      Collect button

                                                             v.      Upgrade button (when available)

                                                           vi.      Infill level (when upgraded)

# 5.  Performance Requirements

The game build will target different operating systems:

·         Windows (11)

·         MacOS

·         Linux (Ubuntu)

The game should perform well on lower end machines.

# 6.  Design Constraints

The game is used by the University of the Highlands and Islands; it needs to meet accessibility and inclusivity requirements.

The product needs to be developed in a small amount of time; hence the scope should be small and only scalable where possible.

The team does not have budget for tailored art and assets creation.

# 7.  Non-Functional Attributes

Accessibility compliance:

Must have:

10.  Text to Speech or narrated text.

11.  Font and text colour/size personalisation.

# 8.  Preliminary schedule and budget

Final build preferably for 30/03/2026, ready for playtests.

No budget.