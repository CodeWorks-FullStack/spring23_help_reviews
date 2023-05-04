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
    -- "Ian's Public House",
    -- "https://cdn.vox-cdn.com/thumbor/5QNRiun1MJ5VqLlnUQDYfbkomyg=/0x0:1280x720/1200x800/filters:focal(541x266:745x470)/cdn.vox-cdn.com/uploads/chorus_image/image/55120575/13517551_1104527672919812_9041055288373038368_o.0.jpg",
    -- "Not open to everyone, must be waering a jacket, private drinkonly, must know the secret passcode",
    -- "644fe0271c41b32e5855df19" (
    "Big-B-Cheese",
    "https://s3-alpha-sig.figma.com/img/871b/04bd/64cb8adb72ca7fe0b48e23b831a96ec1?Expires=1684108800&Signature=Q4ZCYxsfoguet0kpdg8MRtp7-06sNDYhcKzt4ci6JqteBQXSBIYHcZ6uRV6Xsi5wCQW93Jgc6D~1am0pzIq6UHs1fsWLTvYt~ibFG5wYstaLfjb-M2412IKXfVPLHa8lGwiqvTRs5SzR8rbwAyI5256K936hSwM4SDLpDMZT0SkTmxaBUf2sH3IBNHG3aG9NimyiQULDdqPXuy8ru7f1RjaUvsXQlvcrtk6-z1PRPO8OIEihH8TcUHmIDZrhfQU8VyVCRfXDf0rRTjHU3UHZdMOF-xlPLMyGkjCGbe2rCcI0u55CnUpkCDVfHHPoCYsYGRNdxtzVdaAwaBIy6yrivw__&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4",
    "Benedict The Rat is here to make your day a little cheesier",
    "634844a08c9d1ba02348913d"
);

SELECT r.*, a.*
FROM restaurants r
    JOIN accounts a ON a.id = r.creatorId;

CREATE TABLE
    reports(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title VARCHAR(255) NOT NULL,
        body TEXT NOT NULL,
        rating INT NOT NULL DEFAULT 1,
        restaurantId INT NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (restaurantId) REFERENCES restaurants(id) ON DELETE CASCADE,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

INSERT INTO
    reports(
        title,
        body,
        rating,
        restaurantId,
        creatorId
    )
VALUES (
        "🐀🐀🐀",
        "I love Bendict Cumberbatch.",
        5,
        4,
        "634844a08c9d1ba02348913d"
    );

SELECT rep.*, acct.*
FROM reports rep
    JOIN accounts acct ON rep.creatorId = acct.id
WHERE restaurantId = 4;