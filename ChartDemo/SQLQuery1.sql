select * from ViewBillHistory as it where it.TransactionDate between '2010-3-1' and '2010-3-28'
select * from ViewUsageHistory

select sum(it.TotalDue) from ViewBillHistory as it
where it.TransactionDate between '2010-3-1' and '2010-3-28'
group by CONVERT(varchar(7), it.TransactionDate, 120)