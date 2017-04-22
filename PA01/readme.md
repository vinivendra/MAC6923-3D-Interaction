
# Readme - MAC6923 - PA01

## Vinicius Vendramini - 7991103

This Program Assignment was created based on the Roll-a-Ball tutorial for Unity, available on YouTube. A few modifications were made, as instructed by the assignment's description. A few things should be noted:

### Assets

The models for the house, the fences and a few materials are freely available [here] (https://kenney.nl). They are part of the larger *Buildings* and *Racing* asset packages. The truck was made out of basic geometric objects on Unity itself, and is available in the _TruckScene_ scene, in the Scenes folder.

The sprite for the grass was taken out of a screenshot of a Pokémon video game. The rest of the models and images were created by me.

### Source files

The _CameraController_ file controls the camera movements to make it follow the car around.

The _CoinController_ script controls each coin individually, both as it spins around its own axis and as it spins around the house.

The _PlayerController_ file contains code for allowing the user to control the car and for updating the UI (both the score text and the victory/defeat text). It uses a few tricks and some physics calls to make the car controls a bit more natural and easier to use.

### Game

The game counts the score as the player collects (touches) the coins that spin around the center of the map, and ends in victory when all coins are collected or in defeat if the player touches the house (causing the car to be thrown away). Touching the fences is ok.

The player accelerates the car by pressing the up arrow and makes it go backwards by pressing the down arrow. The left and right arrows turn the car in the corresponding direction (based on its local coordinate system).

To test the game, it might be easier to just move the car slightly forwards and backwards and wait for the coins to circle around and touch the car by themselves.