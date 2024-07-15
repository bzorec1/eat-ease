
# DOMAIN MODEL

```json
{
  "Groups": [
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "GroupManagerId": "00000000-0000-0000-0000-000000000000",
      "Name": "Sample Group Name",
      "Description": "Sample group description",
      "LocationPreferences": ["Location1", "Location2"],
      "Events": [
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000001"
      ],
      "Members": [
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000001"
      ]
    }
  ],
  "Events": [
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "EventType": "Social",
      "EventDateTime": "2024-03-15T06:29:36.207215",
      "Location": "Sample Event Location",
      "Status": "Planned",
      "Expenses": [
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000001"
      ],
      "Participants": [
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000001"
      ]
    }
  ],
  "Expenses": [
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "Amount": 100.0,
      "Description": "Sample expense description",
      "Category": "Food",
      "PaidByUserId": "00000000-0000-0000-0000-000000000000",
      "EventId": "00000000-0000-0000-0000-000000000000"
    }
  ],
  "Users": [
    {
      "Id": "00000000-0000-0000-0000-000000000000",
      "FirstName": "Sample Name",
      "LastName": "Sample Lastname"
    },
    {
      "Id": "00000000-0000-0000-0000-000000000001",
      "FirstName": "Another Name",
      "LastName": "Another Lastname"
    }
  ]
}
```

```sql
-- Group Table
CREATE TABLE Groups (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    GroupManagerId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    LocationPreferences NVARCHAR(MAX) NULL -- Assuming JSON format or delimited list,
    FOREIGN KEY (GroupManagerId) REFERENCES Members(Id)
);

-- Members Table
CREATE TABLE Members (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL
);

-- GroupMembers Table (to handle many-to-many relationship between Groups and Members)
CREATE TABLE GroupMembers (
    GroupId UNIQUEIDENTIFIER NOT NULL,
    MemberId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (GroupId, MemberId),
    FOREIGN KEY (GroupId) REFERENCES Groups(Id) ON DELETE CASCADE,
    FOREIGN KEY (MemberId) REFERENCES Members(Id) ON DELETE CASCADE
);

-- Events Table
CREATE TABLE Events (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EventType NVARCHAR(50) NOT NULL,
    EventDateTime DATETIME2 NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    GroupId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (GroupId) REFERENCES Groups(Id) ON DELETE CASCADE
);

-- EventParticipants Table
CREATE TABLE EventParticipants (
    EventId UNIQUEIDENTIFIER NOT NULL,
    ParticipantId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (EventId, ParticipantId),
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE,
    FOREIGN KEY (ParticipantId) REFERENCES Members(Id) ON DELETE CASCADE
);

-- Expenses Table
CREATE TABLE Expenses (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Amount DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Category NVARCHAR(50) NOT NULL,
    PaidByUserId UNIQUEIDENTIFIER NOT NULL,
    EventId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (PaidByUserId) REFERENCES Members(Id),
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE
);

```
