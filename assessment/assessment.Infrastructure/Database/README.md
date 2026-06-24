# How to run migrations and seed the database
To run migrations and seed the database, follow these steps:

1. Open a terminal and navigate to the root directory of the project.
2. Run the following command to apply migrations and seed the database:
```bash
dotnet ef database update --project assessment.Infrastructure/Database --startup-project assessment.Presentation
```
3. The command will apply any pending migrations and seed the database with initial data.