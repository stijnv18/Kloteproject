CREATE DATABASE BlogDB;

USE BlogDB;

CREATE TABLE posts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Content TEXT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Likes INT NOT NULL DEFAULT 0,
    Dislikes INT NOT NULL DEFAULT 0
);