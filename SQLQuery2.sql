SELECT *
	from Orders
	where id = 1;

SELECT sum(Orderlines.Quantity * Items.Price) 
	from Orderlines	
	join Items
		on Orderlines.ItemId = Items.Id
	where OrderId = 1;
