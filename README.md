# AStudyInTest

This is an exploration into TDD using Entity Framework Core's InMemory Database Provider. For fun.

The code may be a bit messy at times, I am deliberatly playing with the TDD development cycle and running a bit loose with the refactoring. Then when I need to refactor I will see how brittle my tests are.

## The problem

### Stage 1.
I have a couple of friends that sells food. One of them sells premade meals. 
* Orders are made over the phone, by text or by email.
* Orders must be in by miday on Wednessday and she delivers them on Friday. 
* The menu is changed every week but there are some standard menu items that stay in rotation.

On refelction this is basically a HelloFresh/Marley Spoon application with the addition of some set menu items.

### Stage 2
The other runs a small cookie business supplying local cafes
* They take orders over the phone. 
* They have multiple delivery days
* A lot of her customers have standard repeating weekly orders.
* They have standardised delivery runs that only go to a certain area on that day. If a specific customer wants some cookies they have to fit in to the days she is delivering in that area.
* If the order is large enough I am sure she would do a special delivery.
* Orders must be in by 12:00 the day before delivery, but if she has enough items in stock the order could be at a later date.



## The solution
Normally I would mock out a user interface, work through the logic with the client, make adjustments design the model and then start to implement the front end and backend in tandem.
This time I am going to attempt to develop the entire backend first just for fun.
