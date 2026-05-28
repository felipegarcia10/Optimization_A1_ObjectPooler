# Optimization A1 - Object Pooler

# 

Collaborators: PG29 Felipe, PG29 Julian Rosero







HOW TO USE OUR OBJECT POOLER:



1. In the scene you Will see a "Spawner" game object with an "Object Pooler" component.
2. the child game object of "Spawner" contain the preinstantiated object to be pooled (Turrets and Bullets).
3. The "Object Pooler" component in "Spawner" holds the items to pool (types of items) and the list of items to pool.
4. the items to pool have the following properties:

   1. Amout to Pool: the amout of objects of this specific type to be pooled
   2. Object to pool: a prefab of the object to pool
   3. should expand: can "Amount to Pool" increase.
   4. Should start active: determines if the pooled object starts active in the scene or not
   5. Should randomize position: determines if the position of the pooled object is randomized on Start().
5. the Pooles Objects list starts with the preinitialized objects to pool (bullets and turrets)
6. new game object instances are added to the pool when the "Amout to pool" is bigger that the amount of preinstantiated objects.

&#x09;



WHAT TO EXPECT



when playing, the corresponding "Amount to pool" of turrets is randomly spawned in the scene, and they shoot bullets that deactivate on trigger (Hit bounds or turrets). 

The Pooler adds new instances or removes the preinstantiated objects based on the "Amount to Pool" 

&#x09;

