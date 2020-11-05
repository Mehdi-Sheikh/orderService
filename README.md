# OrderService

Thanks for taking the time to do this exercise!

We want to see how you solve tasks in two key areas:
* Architecting and designing new solutions
* Refactoring and improving existing code

The two tasks are completely separate.

## Architecture and design

We would like you to use a visualization tool of your choice (like [draw.io](https://app.diagrams.net), MS paint, etc) to illustrate how you would organize various components to achieve the needs of a system.

The following are the needs of a pizza ordering service:

1. The system needs to allow creation of an order both from a frontend and via integrations with external entities/partners
2. Admin level users can set prices and administrate available discounts
3. When an order is placed, a PDF receipt should be generated and emailed to the customer.

Note: The PDF receipt service can only generate a single receipt at a time and might spend 10-60 seconds on it depending on its mood.

## Code refactoring

The accompanying OrderService solution contains some classes used by an imaginary service to generate order receipts and some unit tests to prove that everything works.  

We want you to make the system more extensible.  
The company wants to be able to add a LOT of new products, discounts of various types and receipt formats in the future.  

Refactor the code so that it can better tackle these changes, and prove your work by implementing: 
* a JSON receipt
* a 50% discount when buying 10 or more of a product
* a flat amount discount (say kr 100)
* a new disability insurance product that costs 1000

You are free to break any existing dependencies in order to make the changes you want.

### Tip
* Your code should be [SOLID](https://en.wikipedia.org/wiki/SOLID).
* You should be confident that it works.
* You should be comfortable the next developer will easily understand how to work with it.

## Summary
There are no right or wrong solution(s) for these tasks.  
We don't want you to spend an excessive amount of time on this so if in doubt, ask.

In the interview, be prepared to present your work (by sharing your screen) and take us through what you've done and the choices you've made.

Good luck and, most importantly, have fun :)