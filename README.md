# Solution folder structure

**db** folder: put .sql files here. File names should start with 3 digits (000, 001, 002...), followed by an underscore (_) and a short description of what the script does, e.g. `001_add_rooms_table.sql`. To let colleagues easily update their database, create a .sql file to ALTER the database whenever you modify the database schema.

The `Seed.sql` script will insert dummy data into a freshly created database if you dropped your old one before running the database create script.
