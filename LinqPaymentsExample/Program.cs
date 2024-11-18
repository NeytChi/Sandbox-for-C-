using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPaymentsExample
{
    /* 
    С помощью LINQ произвести :	
	
    Поиск Инвойсов и Платежей, с совпадающей датой	
    Расчет: покрыли ли платежи все инвойсы по каждой дате, если есть отклонение - то найти его	
	
    По результатам сформировать два списка:	
    Вывести ID инвойсов, за дни, в которые не были покрыты платежами в полной мере в этот день	
    Вывести ID инвойсов, за дни, в которые платежей было больше, чем необходимо по общей сумме в этот день	
	
    Для лаконичности - сделать это одной цепочкой методов без явного использования циклов
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            var tests = new InvoicePaymentTests();
            tests.SearchByDeadLine_WhenIsMatch_Result();
            tests.CalculationByDeadLine_WhenCoveredByPayment_ResultInvoices();
            tests.CalculationByDeadLine_WhenNotCoveredByPayment_ResultInvoices();
            Console.Read();
        }
    }
    public class InvoicePaymentTests
    {
        public List<Invoice> CreateTestInvoices()
        {
            return new List<Invoice>
            {
                new Invoice
                {
                    InvoiceId = 1,
                    InvoiceBody = "First invoice.",
                    InvoiceDeadline = new DateTime(2024, 4, 5),
                    InvoiceCreated = new DateTime(2024, 3, 5),
                    Amount = 500
                },
                new Invoice
                {
                    InvoiceId = 2,
                    InvoiceBody = "Second invoice.",
                    InvoiceDeadline = new DateTime(2024, 12, 5),
                    InvoiceCreated = new DateTime(2024, 3, 5),
                    Amount = 500
                }
            };
        }
        public List<Payment> CreateTestPayments()
        {
            return new List<Payment>
            {
                new Payment
                {
                    PaymentId = 1,
                    Amount = 500,
                    PaymentDate = new DateTime(2024, 4, 5)
                },
                new Payment
                {
                    PaymentId = 2,
                    Amount = 1000,
                    PaymentDate = new DateTime(2024, 8, 1)
                }
            };
        }
        public void SearchByDeadLine_WhenIsMatch_Result()
        {
            var invoices = CreateTestInvoices();
            var payments = CreateTestPayments();

            Console.WriteLine("General count of invoices -> " + invoices.Count);
            Console.WriteLine("General count of payments -> " + payments.Count);

            var matchedInvoices = invoices.Where(invoice =>
                payments.Where(payment =>
                    payment.PaymentDate == invoice.InvoiceDeadline)
                        .FirstOrDefault() != null).ToList();
            Console.WriteLine("Search invoices by deadline match with payment date. Count -> " + matchedInvoices.Count);
        }
        public void CalculationByDeadLine_WhenCoveredByPayment_ResultInvoices()
        {
            CalculationByDeadLine(true);
        }
        public void CalculationByDeadLine_WhenNotCoveredByPayment_ResultInvoices()
        {
            CalculationByDeadLine(false);
        }
        public void CalculationByDeadLine(bool covered = true)
        {
            var invoices = CreateTestInvoices();
            var payments = CreateTestPayments();

            Console.WriteLine("General count of invoices -> " + invoices.Count);
            Console.WriteLine("General count of payments -> " + payments.Count);

            var matchedInvoices = from i in invoices
                                  join p in payments on i.InvoiceDeadline equals p.PaymentDate
                                  where (i.Amount <= p.Amount) == covered
                                  select i
                                  ;
            Console.WriteLine($"Calculate invoices that covered({covered}) by payments by their deadline dates. Count -> {matchedInvoices.Count()}");

            var exceptedInvoices = invoices.Except(matchedInvoices).ToList();

            Console.WriteLine($"Invoices that is excepted from group of matches. Count ->{exceptedInvoices.Count()}");
        }
    }
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceBody { get; set; }
        public DateTime InvoiceDeadline {get;set;}
        public DateTime InvoiceCreated {get;set;}
        public decimal Amount {get; set;}	
    }
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }	
}
