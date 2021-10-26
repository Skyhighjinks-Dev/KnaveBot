CREATE TABLE [AdminAction] (
	[ActionID] int IDENTITY(1, 1) PRIMARY KEY,
	[GuildID] varchar(500) NOT NULL,
	[User] varchar(100) NOT NULL,
	[UserID] bigint NOT NULL,
	[Sender] varchar(100) NOT NULL,
	[SenderID] bigint NOT NULL,
	[Action] varchar(50) NOT NULL,
	[Date] DATE NOT NULL,
	[InvalidDate] DATE NULL,
	[LastStatusUpdate] DATE NULL
);

