# Eatease Project Domain Model

The domain model for the Eatease project outlines the core entities and their relationships, focusing on organizing group lunches, decision-making, expense tracking, and attendance management.

## Models

### Group

Represents a collection of users coming together for lunches or events. Groups can have preferences for cuisines or locations and manage expenses collectively.

- **Attributes:**
  - Name: The name of the group.
  - Description: A brief description of the group's purpose or interests.
  - LocationPreferences: Preferred locations or types of cuisine.
  - GroupManagerId: A reference to the `User` who manages the group (Group manager).

- **Relationships:**
  - Has many `Events`: The events or lunches organized within the group.
  - Has many `Users`: Users who are members of the group.

### Event

An occurrence within a group, such as a lunch or meeting. Events are decided collaboratively, and expenses are tracked.

- **Attributes:**
  - EventType: The type of event (e.g., lunch, meeting).
  - EventDateTime: The scheduled date and time for the event.
  - Location: The location or venue for the event.
  - Status: The current status of the event (Upcoming, Ongoing, Completed).
  - Expenses: A collection of expenses related to the event.

- **Relationships:**
  - Belongs to a `Group`.
  - Has many `Users` through attendance (Participation).

### User

Individuals who can join groups, participate in events, and manage their preferences and expenses.

- **Attributes:**
  - Name: The user's first name.
  - Surname: The user's last name.
  - Preferences: Individual preferences for cuisine, location, etc.

- **Relationships:**
  - Belongs to many `Groups` through participation in events.
  - Has many `Events` through attendance.

### Expense (Additional Model)

Tracks the expenses related to group activities or events, facilitating bill splitting and contribution management.

- **Attributes:**
  - Amount: The total amount of the expense.
  - Description: A description of the expense.
  - Category: The category of the expense (Food, Drink, Other).
  - PaidByUserId: The user who paid the expense.
  - EventId: The event to which the expense is linked.

- **Relationships:**
  - Belongs to an `Event`.
  - Associated with a `User` who paid for the expense.
