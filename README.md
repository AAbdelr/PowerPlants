# Welcome !

## How to test the API ?

First, launch the "PowerPlants" solution.

In the solution you will find 2 projects "PowerPlants" and "PowerPlantsTests".

## "PowerPlants" Project 

The "PowerPlants" project contains the PowerPlantsController which contains 2 methods "Post" and "GeneratePower". It is in this controller that the algorithm is located.

In order to test the program, you must go to the "PowerPlantsTests" project which, as its name suggests, allows you to do the tests.

## "PowerPlantsTests" Project 

The "PowerPlantsTests" project contains the PowerPlantsControllerTests which contains the "PostTest" method. --> This method allows you to do the tests

Place the breakpoints where you want to analyze the behavior of the program and then right click on the "PostTest" method -> "Debug Test(s)"

After launching the method via the "Debug Test(s)" button, one of the test files contained in the "Mock" folder will be loaded and sent to the "Post" method of the "PowerPlantsController".

The program will stop where you have placed breakpoints so that you can analyze the algorithm.

Once the load is equal to the energy power supplied by the PowerPlants, the results will be returned to the "Result" variable of the "PostTest" method.

!!! So put at least one breakpoint on the "Result" variable to see the final result !!!

## SignalR

!!!

SignalR is well implemented and as requested the inputs as well as the result are sent to all clients connected to the hub. 
On the other hand the code allowing this is commented out because it throw a null exception because no client is connected to the server. (because the API is tested via unit tests without being executed)

!!!
