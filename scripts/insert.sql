insert into CarManufacturer (Name) values ('Honda');
insert into Car (ModelName, CarManufacturer_ID, Number) values ('HondaSuper', 1, 1);
-- Insert drivers with a small delay to check sorting by birthday
insert into Driver (FirstName, LastName, Birthday, Car_ID) values ('Oleg', 'Popovich', SYSDATETIME(), 1);
insert into Driver (FirstName, LastName, Birthday, Car_ID) values ('Aleksandr', 'Zetkovich', SYSDATETIME(), 1);
