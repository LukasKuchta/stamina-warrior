# Purpose of this project
Show the DDD driven modeling and what are the best practices and benefits of this approach. Strong OOP and SOLID guiddience in practice. How to fight with complexities and what is a change locallity.

## Modeling mindset
- What is a model?
- What are the differences between a real-life model and a software model?
- What does the model mean?
- What is its place within the system?
- Does the model have behavior?
- Is primitive obsession always bad idea?
- Where are boundaries ?
- <img width="470" height="351" alt="image" src="https://github.com/user-attachments/assets/b9872644-f316-4d94-92e2-64ebc129e2bd" />

- ...

--------------------
# The duel (conceptual model)
<img width="407" height="254" alt="image" src="https://github.com/user-attachments/assets/7a896e98-23f7-4772-88e8-51c11b013ba1" />


The game supports two-player duels. Players can fight across different spheras (each sphere defines the duel’s complexity and difficulty). Every player starts with Health and Damage Power.

To make fights more exciting, players can bring a deck of magical cards. Each player’s deck can be empty or filled with cards of different abilities:

## Magic cards:
- Health card — heals its owner.
- Fighting card — boosts the owner’s potential attack.
- Thorn card — damages its owner.
- Cursed card — places a curse on the opponent. The curse lasts for the entire fight (unless removed by a special card).
- Stealing card — allows a player to steal any card from the opponent.

The entire combat and turn system is driven by randomness and probability.

# Possible battle outcomes:
- Double knock-out — both players die.
- One player wins — the other is defeated.
- Tie — the duel ends in a draw.

# Bounded context
For simplicity just one at the start of the project but ...

# Ubiquitous language

# Project structure readability

# The domain

# Big chapter of testing
## Unit testing
- Testing public API, not internal behavior (overspecification)
- Looks like production code
- Treated as production code

## Enforce architectural rules in unit tests.
https://github.com/BenMorris/NetArchTest

## God Loves, Man Kills
<img width="676" height="667" alt="image" src="https://github.com/user-attachments/assets/155ec70e-660c-442f-bcfc-be09a3ba8c1e" />
https://stryker-mutator.io/docs/stryker-net/introduction/

## Who is William Stryker
https://en.wikipedia.org/wiki/William_Stryker


