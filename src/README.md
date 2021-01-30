# WeepingSnake Solution

The macro structure of the solution is a kind of MVC, so it consists out of three projects.

## [`Game`](WeepingSnake.Game) (the Model)

The [`model`](WeepingSnake.Game) is the game itself. It manages games and players, applies actions and generates output. It can also be used as a standalone library, so that [`view`](WeepingSnake.ConsoleClient) and [`controller`](WeepingSnake.WebService) can be implemented differently than in this solution.

## [`ConsoleClient`](WeepingSnake.ConsoleClient) (the View)

The [`view`](WeepingSnake.ConsoleClient) is a sample client that accesses the [`WebService`](WeepingSnake.WebService) via http. For platform independency reasons, this sample client does not provide a graphical user interface.

## [`WebService`](WeepingSnake.WebService) (the Controller)

The [`controller`](WeepingSnake.WebService) provides multiple http endpoints to access the [`game`](WeepingSnake.Game) itself. The game is accessed through the business logic of the model. Since the controller maps the provided methods of the model to http, the model can be considered as a standalone library for this reason.
