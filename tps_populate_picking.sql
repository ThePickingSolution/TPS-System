
SET @userkey = '08d9552c-ebb7-4546-8979-599f0f4a0f16';

INSERT INTO tps.`picking.orderpicking`
(Id,Description,CreationDate) VALUES ('20210827_01','Picking Teste 1',NOW());

SET @pickingId = (SELECT Id FROM tps.`picking.orderpicking` ORDER BY CreationDate DESC limit 1);

INSERT INTO tps.`picking.orderpickingdetail`
(OrderPicking_Id, Name, Value) 
VALUES (@pickingId,'TEST','true');

INSERT INTO tps.`picking.orderpickingprocess` (OrderPicking_Id,Status_Id, Operator,Sector,Container,Date)
VALUES (@pickingId,1000,@userkey,NULL,NULL,NOW());

INSERT INTO tps.`picking.pickingitem` (Id,SKU, Description, OrderPicking_Id)
VALUES ('PI_01','TEST01','Item 01 Test',@pickingId);



INSERT INTO tps.`picking.orderpicking`
(Id,Description,CreationDate) VALUES ('20210827_02','Picking Teste 2',NOW());

SET @pickingId = (SELECT Id FROM tps.`picking.orderpicking` ORDER BY CreationDate DESC limit 1);

INSERT INTO tps.`picking.orderpickingdetail`
(OrderPicking_Id, Name, Value) 
VALUES (@pickingId,'SECTOR','A2');

INSERT INTO tps.`picking.orderpickingprocess` (OrderPicking_Id,Status_Id, Operator,Sector,Container,Date)
VALUES (@pickingId,1000,@userkey,NULL,NULL,NOW());

INSERT INTO tps.`picking.pickingitem` (Id,SKU, Description, OrderPicking_Id)
VALUES ('PI_02','TEST02','Item 02 Test',@pickingId);
