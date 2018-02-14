# Multiple-Artificial-Intelligence-Characters-
Small run and gun game built via Unity that showcase multiple types of AI, including; Chasing, Evading, 
Finite State Machines, Flocking, Intercepting, Rule Based Systems, and NavMesh / A* 

Furthur Details:

Objective: The player most try to get from the start point to the end point on the other side of the map by accumulating kills and 
unlocking doors to progress further. Blue disc shaped save points are spread out through the map so that if you die you can 
immediately respawn there.  

Controls: Use the WASD keys to move the player around, SPACE BAR to “Dodge”, SHIFT to run, and the MOUSE and 
LEFT MOUSE button to aim and shoot the player’s gun.

Game Rules: Top Left corner of the HUD is the player health. Bottom left corner is players energy for running and dodging 
(Only replenishes when empty or after 3 seconds of letting go of shift or space bar). Top right corner is the kill count used to 
unlock yellow doors. Red doors can only be exited from one side. Green doors can only be opened when green enemies nearby are eliminated.  

Types of Artificial Intelligence (AI)  

Chase: All enemies have some sort of chasing code in their scripts. The red cylinder (NAME: Enemy Chaser) is slow to chase after the player 
and if player is not in range will wander aimlessly. The orange cylinder (NAME: Enemy Rocket) who owns a rocket launcher will behave 
similarly but will fire missiles when the player is in range. The yellow cylinder (Name: Enemy Evader) will not wander aimlessly, but will 
remain still until player is within a certain range then will start to chase.

Evade: The yellow cylinder (Name: Enemy Evader) will chase when player is in range and when its health is below 40% it will immediately 
begin to “run” away from the player indefinitely.

FSM: All enemies except for FlyEnemy and EnemyEvader have some implementation of FSM. They Wander, Chase and or Shoot. Big green cylinders 
(NAME: EnemySentry) have a little bit of different types of states. They patrol designated locations and continuously motion around these 
points. If they spot the player they will begin to chase them. If they lose sight of the player they will go to the last seen location and 
if the player is lost the will return to patrolling their programmed area. 

Flocking: Small red triangular prism shaped enemies (NAME: FlyEnemy) use a flocking algorithm and randomly fly around the map. If they 
collide with the player they will deal a certain amount of damage. If the player gets lucky they can kill these enemies, but it is best 
to avoid them by running or dodging. 

Intercept: The EnemyRocket has rocket launcher that fires at the player when they are in range. The rocket fires missiles that try to 
intercept the player by taking the players position and velocity.

Rule Based System:  The Biggest enemy, a big green cylinder (NAME: EnemyBigBossRule) uses a random wandering script, chase and attack 
when in range script (EnemyBigBossMovement.cs). The enemy will increase speed at which it chases if the player is in range and is 
holding the shift key. It will also lower the amount of damage it deals based on the health % of the player. The less the health, the 
less damage and time intervals of attacks the enemy will do. It also changes colors for each of these conditions. 

NavMesh / A*: All enemies, except for the FlyEnemy have a navmesh agent attached to them to allow them to avoid obstacles and find the 
player and chase the player when certain conditions are met. 
