Void Creature Invasion System
For info about the Void invasion see:
http://www.uoguide.com/Void_Creatures

This system spawns 2 to 4 Korpre in many random locations all over TerMur once per hour until it reaches the NumToSpawn. I avoided towns and the main housing areas.

The Korpre in this system evolve over time depending on what they do they can become:

Betballem by killing
which can become Ballem by killing
which can become UsagralemBallem by killing

Anlorzen by grouping
which can become Anlorlem by grouping
which can become Anlorvaglem by grouping

Anzuanord by surviving
which can become Relanord by surviving
which can become Vasanord by surviving

there is also a chance to spawn an Ortanord as a byproduct of evolution.

To install:
Updated to match stats/loot with the current repo's void creatures.

Install info:
Remove Scripts\Mobiles\Void Creatures folder
Remove Scripts\Mobiles\Normal\Ortanord.cs

comment out the following from BaseCreature.cs

#region SA
if (LastKiller is BaseBaseVoidCreature)
((BaseBaseVoidCreature)LastKiller).Mutate(VoidEvolution.Killing);
#endregion

comment out the following from PlayerMobile.cs

if (LastKiller is BaseBaseVoidCreature)
((BaseBaseVoidCreature)LastKiller).Mutate(VoidEvolution.Killing);

Drop the Void Invasion folder into your location of choice and restart

once the server is up and your logged in type:
[Add VoidCreatureInvasionSystemController
then target the location you want to place it.
Double click it and you will see:

Active False by default set to true to enable the system
NumToSpawn If there are less than this number of void creatures in the world it will spawn 2-4 more once an hour (default is 15)
RemoveAllVoids if set true deletes every BaseVoidCreature currently spawned then sets itself back to false

This system limits the number of void creatures in the world and closely mimics the mechanics for evolving on Broadsword.

WARNING: the evolution process causes normal and XML spawners to lose track of void creatures, so if they are placed on normal or XML spawners and are not killed often enough they would take over the map eventually. Basically just remove them from any spawners and you should be fine...

If anyone has any questions or suggestions feel free.