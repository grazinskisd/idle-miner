#Idle Miner Tycoon clone#

Couple of notes on code architecture:  

* Zenject plugin is used for dependency injection  
* Following on some things learned from "Clean Code" (https://g.co/kgs/B8Rprn):  
	* Comments are kept to a minimum, compensated by descriptive class, function and variable names  
	* Functions are kept small and restricted to relatively constrained tasks  
* A kind of MVC pattern is used for this project:  
	* MonoBehaviours act as Views and give general acess to a game object  
	* Pure C# classes as controllers, factories and other bits to controll the views  
* Due to time constraints and minimal asset requirements, Resources and AssetBundles are not used (AssetBundles would be advisable otherwise)  
  
Features:  

1. Multiple mines, that are configurable with ScriptableObject settings  
2. Lift, warehouse and mineshaft facilities in each mine. These are also configured using ScriptableObject settings  
3. Leveling up of Lifts, warehouses and mineshafts  
4. Each mine can support 35 mineshafts, however there is no feature to add them in game, just in settings

Can be downloaded here: https://dalius.itch.io/idle-miner

![image](https://github.com/grazinskisd/idle-miner/assets/7268374/0e86398a-ec33-4178-acd7-2cd043348476)
