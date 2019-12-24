# Rocket League Map Copier
This application copies a map file into the Rocket League mods directory, as the name provided.  
It will also copy any packages that names are provided for which exist alongside the map file.

## Reason for creation
When modding the game RocketLeague, we copy map files into the game's package directory and overwrite an existing map to spoof our package as a legitimate entry. Sometimes, we also have additional packages containing assets that are used in our map but contained within separate files. This application streamlines that process to a single button press.

## Method
1. The user enters their game directory and their custom map directory. 
2. The user enters their package name and any other package names which should come along with it.
3. `Copy map` is pressed.
4. The map and it's dependent files are copied into the required directory.
5. The user starts the game and loads into the default map, but it has been overwritten with their own map instead.

## Tech used
- C#
- Visual Studio

## Definition of done
This project will be defined as completed when it:
- [x] Copies map files into the required folder.
- [x] Copies additional dependencies alongside the map.
- [x] Saves settings on exit
