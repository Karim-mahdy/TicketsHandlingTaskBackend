Ticket Management Task
Overview
This .NET Core application is a Ticket Management Task designed to create, list, and manage tickets. It utilizes modern development practices including CQRS, the Mediator pattern, and Clean Architecture.

Features
1. Create a Ticket
Properties: Id, Creation Date, Phone Number, Governorate, City, District
Dropdown Lists for Governorate, City, and District with static values (no database required).
2. List Tickets
Pagination: Display tickets with 5 records per page.
3. Handle Tickets
Handle Button: Changes ticket status to "Handled".
4. Automatic Handling
Tickets created within the last 60 minutes are automatically marked as handled.
5. Color Coding
Yellow: Ticket created 15 minutes ago.
Green: Ticket created 30 minutes ago.
Blue: Ticket created 45 minutes ago.
Red: Ticket created 60 minutes ago.
Technologies Used
.NET Core: Framework for building the app.
CQRS & Mediator: Command and Query Responsibility Segregation.
Clean & Vertical Slice Architecture: Project structure for better organization and maintainability.
AutoMapper: Mapping between DTOs and entities.
FluentValidation: Data validation for incoming requests.
Serilog: Logging framework for capturing errors and exceptions.
Hangfire: Background job scheduling for automatic ticket handling and status color updates.
SignalR: Real-time updates for status color changes.
Design Patterns: Strategy, Factory, and Unit of Work for better design and implementation.
SOLID Principles: Ensuring high-quality, maintainable, and scalable code.
