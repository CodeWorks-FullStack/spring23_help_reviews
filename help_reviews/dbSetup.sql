CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    restaurants(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(100) NOT NULL,
        imgUrl VARCHAR(500) NOT NULL,
        description TEXT,
        isShutdown BOOLEAN NOT NULL DEFAULT FALSE,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

INSERT INTO
    restaurants(
        name,
        imgUrl,
        description,
        creatorId
    )
VALUES
    -- ("Joe's crab shack", "https://images.unsplash.com/photo-1528826134410-fd8d3f21789d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=715&q=80", "We serve corndogs because we thought it would be funny if we served corndogs when someone expected delicious crab and we can't source crab here so it's corndogs, baby", "644fe0271c41b32e5855df19")
    -- ("Payton's (❌🌾) Pastries", "https://images.unsplash.com/photo-1595450269117-a4c68af150d2?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80", "Just like bread, but worse 😉", "644fe0271c41b32e5855df19") (
    "Ian's Public House",
    "https://cdn.vox-cdn.com/thumbor/5QNRiun1MJ5VqLlnUQDYfbkomyg=/0x0:1280x720/1200x800/filters:focal(541x266:745x470)/cdn.vox-cdn.com/uploads/chorus_image/image/55120575/13517551_1104527672919812_9041055288373038368_o.0.jpg",
    "Not open to everyone, must be waering a jacket, private drinkonly, must know the secret passcode",
    "644fe0271c41b32e5855df19"
);

SELECT r.*, a.*
FROM restaurants r
    JOIN accounts a ON a.id = r.creatorId;