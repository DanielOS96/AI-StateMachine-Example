<i>
Disclaimer: This is a showcase of the Delegate State Machine Code. Most other assets were taken from the asset store i.e. Enemy Spider model, Standard assets and First Person Controller. <b>Created 2019</b>.
</i>

<h1>Enemy State Machine Example.</h1>
Example Video: https://www.youtube.com/watch?v=A5KSO2FJVWY </br>
The code architecture was based the Pluggable AI Unity example (https://www.youtube.com/watch?v=cHUXh5biQMg).  The code has been designed in such a way to allow for any form of AI (follower AI, NPC AI, etc) this example project comes with one fully working Enemy AI to demonstrate its potential. 
</br></br></br>

The included enemy AI has behaviours for the following:
* Attack- The enemy can attack the player causing damage with a verity of different attack animations and randomised sound. 
* Jump Attack- There is a small percent chance this special attack will be triggered. The enemy will lunge at the player doing damage.
* Hit- Enemy can be hit in various areas with randomised sound and animations. Some areas do more damage than others. A body shot will cause 1 point of damage whereas a shot to the crystal will cause an instant kill and the crystal will be destroyed.
* Patrol- When the enemy cannot find the player they will run from waypoint to waypoint.
* Idle- If the enemy cannot find the player and there are no waypoints, they will just idle in their current spot.
* Alert- If the enemy loses sight of the player it stop moving forward will go into alert state where is will scan its surrounding for a few seconds then either chase the player again if it sees them or go into patrol mode if it does not.
</br></br>

Other neat features:
* The enemy will dynamically align itself to the angle of the surface it is on. 
* The enemy’s animations will dynamically shift between walk and run based on its speed. 
* The audio for walk and run will also change based on enemy speed. 
* When the enemy jumps at you shooting it and killing it mid air is really satisfying (especially in VR). 
</br>
From these behaviours alone it is very easy to mix and match to create new forms of AI with no code at all. For example we could take out all the attacking behaviour and just have an AI that patrols waypoints then goes into alert state when It sees the player. This would make for a good quest giver AI where they walk around from point to point then approach the player if they are close. 
</br>


<h2>How to Run</h2>
Unzip the 'Builds.zip' folder and run the .exe inside.

</br></br></br>

![](https://github.com/DanielOS96/AI-StateMachine-Example/blob/master/Screenshots/1.png)
![](https://github.com/DanielOS96/AI-StateMachine-Example/blob/master/Screenshots/2.png)
