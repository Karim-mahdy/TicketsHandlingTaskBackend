Ticket Management Task
Overview
This .NET Core application is a Ticket Management Task with features for creating, listing, and managing tickets. It uses CQRS, Mediator pattern, Clean Architecture, and other modern development practices.

Features
Create a Ticket

Properties: Id, Creation Date, Phone Number, Governorate, City, District.
Dropdown Lists for Governorate, City, and District (static values).
List Tickets

Display tickets with pagination (5 records per page).
Handle Tickets

Handle Button changes ticket status to "Handled".
Automatic Handling

Tickets created within the last 60 minutes are automatically handled.
Color Coding

Yellow: Created 15 minutes ago.
Green: Created 30 minutes ago.
Blue: Created 45 minutes ago.
Red: Created 60 minutes ago.
Technologies Used
.NET Core: Framework for building the app.
CQRS & Mediator: Command and Query Responsibility Segregation.
Clean & Vertical Slice Architecture: Project structure.
AutoMapper: Mapping between DTOs and entities.
FluentValidation: Data validation.
Serilog: Logging.
Hangfire: Background job scheduling.
SignalR: Real-time updates.
Design Patterns: Strategy, Factory, Unit of Work.
SOLID Principles: Code quality and maintainability.
