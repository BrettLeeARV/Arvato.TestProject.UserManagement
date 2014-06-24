ALTER TABLE Asset
ADD CreatedBy int, CreatedDateTime datetime,ModifiedBy int, ModifiedDateTime datetime

ALTER TABLE Room
ADD CreatedBy int, CreatedDateTime datetime,ModifiedBy int, ModifiedDateTime datetime

ALTER TABLE Booking
ADD ModifiedBy int, ModifiedDateTime datetime

GO

CREATE TRIGGER Asset_onInsert ON Asset FOR INSERT AS 
SET NOCOUNT ON
UPDATE Asset 
SET CreatedDateTime = getdate()
WHERE ID IN (SELECT ID FROM inserted)

GO

CREATE TRIGGER Asset_onUpdate ON Asset FOR UPDATE AS 
SET NOCOUNT ON
UPDATE Asset 
SET ModifiedDateTime = getdate()
WHERE ID IN (SELECT ID FROM inserted)

GO

CREATE TRIGGER Room_onInsert ON Room FOR INSERT AS 
SET NOCOUNT ON
UPDATE Room 
SET CreatedDateTime = getdate()
WHERE ID IN (SELECT ID FROM inserted)

GO

CREATE TRIGGER Room_onUpdate ON Room FOR UPDATE AS 
SET NOCOUNT ON
UPDATE Room 
SET ModifiedDateTime = getdate()
WHERE ID IN (SELECT ID FROM inserted)

GO




