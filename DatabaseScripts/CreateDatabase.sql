-- Create the users table
CREATE TABLE users (
    Id TEXT PRIMARY KEY,
    Username TEXT NOT NULL,
    Email TEXT NOT NULL,
    HashedPassword TEXT NOT NULL
);
