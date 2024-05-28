# ReiCalc
A simple calculator server app with a web browser frontend that runs its calculations in the backend.

If you found this repository by accident you might be wondering "Why do it this way?". The short answer to that is: reasons. If you somehow find any part of this repository useful, awesome!

## Usage
Clone this repository and run `ReiCalcApp`.

## Issues
Please read the issues page for an overview of missing/broken things!

## Projects

### ReiCalcApp
ASP.NET server that serves the frontend and runs the calculator in the backend.

### ReiCalcLib
Reusable library that parses and solves mathematical strings and returns the result.

Uses the [shunting yard algorithm](https://en.wikipedia.org/wiki/Shunting_yard_algorithm) to solve the expressions.

#### Supported operations
- Addition `+`
- Subtraction `-`
- Division `/`
- Multiplication `*`
- Exponents `^`
- Parentheses `(` & `)`

#### Order of operations
1. Parentheses
2. Exponentiation and functions
3. Multiplication and division
4. Addition and subtraction

### ReiCalcConsole
Conosle application for quicker testing of the calculator without having to launch the server.