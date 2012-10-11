select SUM(it.TotalDue) as TotalDue,it.AcctNum
from 
	(select bill.TotalDue,bill.TransactionMonth,bill.Cycle,bill.Comp,bill.AcctNum,bill.TransactionDate 
		from ViewBillHistory as bill) as it
where it.TransactionDate between '2011-8-1' and '2011-8-29'
group by it.TransactionMonth,it.AcctNum
order by it.AcctNum



select bill.TotalDue,bill.TransactionMonth,bill.Cycle,bill.Comp,bill.AcctNum from ViewBillHistory as bill
order by bill.TransactionMonth

select * from ViewBillHistory where AcctNum='1'


select sum(it.TotalDue) from ViewBillHistory as it  
where it.TransactionDate between '2011/8/1' and '2011/8/29' and it.Comp='elec' 
group by it.TransactionMonth

use MBDemo
go
select CONVERT(varchar(7),GETDATE(),120) 