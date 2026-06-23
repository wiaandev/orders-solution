# Assessment Wiaan Duvenhage

## My approach to abstraction: CLEAN architecture and DDD principles
DDD and clean architecture:

Reasoning behind this is that I want to make sure that the application is scalable and maintainable. I want to make sure that the application is testable and that the application is easy to understand. I want to make sure that the application is easy to change and that the application is easy to extend.
This may be overengineering for something this simple but I believe as each application grows (as every application does) it will make it a bit more maintainable over the long run

> I started a brand new project just to make sure I add things in the right place and that I kind of rambled in the live assessment and I placed things not where I usually should

# My thought process

### Dependency injection
Instead of bloating the `program.cs` in the presentation layer. I just abstratced the dependency injection into a static class called `ServiceCollectionExtensions` for each project and then I just call that in the `program.cs` file. This makes it easier to read and understand what is being injected.

# The layers
## Application:
Here I added all the business logic and the use cases (services, repository interfaces). I split up the functionality of the application into a features folder structure and then added everything pertaining to that "feature" in there.
For example all interfaces, repositories and everyting to do with and order I added into that folder. My reasoning behind this is because I believe putting everything into repository folder or services folder gives the developer a hard time to look at each folder to implement a feature.

> After taking a look again at the DDD principle docs this is definitely not where the db context should be.

### `ProcessOrderResult` class:
I created a `ProcessOrderResult` class to encapsulate the result of processing an order.
This class contains a `Success` property to indicate whether the order was processed successfully, and an `ErrorMessage` property to provide any error messages that may have occurred during processing. This is a pattern I really like as you can see exactly what is being returned. I could have gone even further and created a `ErrorResult` object that will tell me exactly what went wrong to make it better for tracing and debugging but I didn't have time to implement that.

I implemented also a validator class that will validate the order before processing it.

### Why split up payments logic for an order?
Quite simply because I wanted to make sure that the order processing logic is not bloated with payment logic. I wanted to make sure that the order processing logic is only concerned with processing the order and not with payment logic.

> I could have gone a step deeper and implemented [Aspire](https://aspire.dev/) to seed the database for me as well as spin up any necessary dependency, making the need for a `docker-compose.yaml` obsolete.

## Domain:
In this layer I just wanted the business rules of the application here, so things like the entities, enums used for the entities, etc.

### Why are we not using the AppDbContext here?
Because EF Core is more of an external dependency (database) and something that should and can be swapped out at any time. Coupling it with the entities will introduce a bigger refactor, slowing down development just for a db provider change and we may switch to another ORM like Dapper.

#### Enums
I moved from defining the enum directly inside the entity to placing it in a dedicated `Enums/` folder. The main goal was to avoid cluttered references like Entity.Enum when accessing it from the application or infrastructure layers, and instead have a cleaner, more direct Enum usage.

That said, this is largely a matter of preference. In some cases, it still makes more sense to keep the enum within the entity itself, especially when it’s tightly coupled to the domain model. However, since this particular enum is shared across multiple projects, placing it in a separate enums directory felt like a cleaner and more practical approach.

## Infrastructure:
In this layer is where we would put anything to do with the database registration, where we send email, process payments or even push some things to a queue or (if I wanted to) add orleans integration here.

Here is where I actually implemented the repositories for the customer, prodyct and order entities defined in `Domain`. As well as implemented the services for the mailing and payments for the different providers.

### Seed Service:
I placed the seed service in the infrastructure layer because the data persistence.

#### Why do we need a SeedService?
This was mainly added for developer convenience, allowing anyone working on the project to quickly get a working dataset without having to manually write scripts or populate the database themselves.

### Strategy pattern:
To handle payments for different providers, I implemented the Strategy Pattern. The main idea is to avoid letting the application layer explicitly decide which payment provider to use. Instead, the correct implementation is resolved at runtime based on the incoming request.

This is achieved through a keyed service registration (Keyed/KeyScoped), where each payment provider is registered with a specific key. When an order request comes in, it includes a payment method, which is then used to resolve the appropriate strategy via a resolver function. That resolved service is responsible for processing the payment.

The benefit of this approach is that adding new payment methods is straightforward. A new provider only requires implementing the strategy in the infrastructure layer and registering it in the DI container. No changes are needed in the application layer, and there’s no need to introduce additional conditional logic like `if` or `switch` statements.

Additionally I added the keys as constant strings instead of hardcoding them in the service registration, making it a bit easier to add to the list and have a centralised place to find them.


## Presentation:
The fourth and final layer will be used as the layer that the UI interacts with. Here we will define our endpoints


### Minimal API:
I went the minimal api route just for the sake of simplicity and that creating controllers was a bit too much for the endpoints.

I split up each "feature" into its own endpoint file.

### Web application extensions
I added this here so that the presentation layer because we need this to seed the database. I have abstracted all the code that calls the dbcontext to migrate drop and seed in the `Infrastructure` layer. This is because the presentation layer should not be concerned with how the database is seeded or migrated, it should only be concerned with calling the service that does that.


## Future improvements:
- Implement a more robust error handling mechanism, possibly using middleware to catch exceptions and return standardized error
- Implement logging to track application behavior and errors
- Add `Directory.Packages.props` to centralize package versions and reduce duplication across projects
- Implement integration and unit tests to validate that for example the correct strategy is being called or tha the DB is returning the correct data from the endpoint