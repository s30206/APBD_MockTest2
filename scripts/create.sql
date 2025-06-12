-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2025-06-11 20:09:02.564

-- tables
-- Table: Car
CREATE TABLE Car (
                     ID int  NOT NULL IDENTITY(1, 1),
                     ModelName varchar(200)  NOT NULL,
                     CarManufacturer_ID int  NOT NULL,
                     Number int  NOT NULL,
                     CONSTRAINT Car_pk PRIMARY KEY  (ID)
);

-- Table: CarManufacturer
CREATE TABLE CarManufacturer (
                                 ID int  NOT NULL IDENTITY(1, 1),
                                 Name varchar(200)  NOT NULL,
                                 CONSTRAINT CarManufacturer_pk PRIMARY KEY  (ID)
);

-- Table: Competition
CREATE TABLE Competition (
                             ID int  NOT NULL IDENTITY(1, 1),
                             Name varchar(200)  NOT NULL,
                             CONSTRAINT Competition_pk PRIMARY KEY  (ID)
);

-- Table: Driver
CREATE TABLE Driver (
                        ID int  NOT NULL IDENTITY(1, 1),
                        FirstName varchar(200)  NOT NULL,
                        LastName varchar(200)  NOT NULL,
                        Birthday datetime2  NOT NULL,
                        Car_ID int  NOT NULL,
                        CONSTRAINT Driver_pk PRIMARY KEY  (ID)
);

-- Table: DriverCompetition
CREATE TABLE DriverCompetition (
                                   Driver_ID int  NOT NULL,
                                   Competition_ID int  NOT NULL,
                                   Date datetime2  NOT NULL,
                                   CONSTRAINT DriverCompetition_pk PRIMARY KEY  (Driver_ID,Competition_ID)
);

-- foreign keys
-- Reference: Car_CarManufacturer (table: Car)
ALTER TABLE Car ADD CONSTRAINT Car_CarManufacturer
    FOREIGN KEY (CarManufacturer_ID)
        REFERENCES CarManufacturer (ID);

-- Reference: Driver_Car (table: Driver)
ALTER TABLE Driver ADD CONSTRAINT Driver_Car
    FOREIGN KEY (Car_ID)
        REFERENCES Car (ID);

-- Reference: Table_6_Competition (table: DriverCompetition)
ALTER TABLE DriverCompetition ADD CONSTRAINT Table_6_Competition
    FOREIGN KEY (Competition_ID)
        REFERENCES Competition (ID);

-- Reference: Table_6_Driver (table: DriverCompetition)
ALTER TABLE DriverCompetition ADD CONSTRAINT Table_6_Driver
    FOREIGN KEY (Driver_ID)
        REFERENCES Driver (ID);

-- End of file.

