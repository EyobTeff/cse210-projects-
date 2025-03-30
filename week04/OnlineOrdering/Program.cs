using System;

class Program
{
    static void Main()
    {
        // Create Customers
        Customer customer1 = new Customer("Eyob Teffera", new Address("477 s 3rd 4 apt5101", "Sugar City", "ID", "USA"));
        Customer customer2 = new Customer("Meseret Wolde", new Address("456 Maple Ave", "Rexburg", "ID", "USA"));

        // Create Orders
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "LAP123", 799.99, 1));
        order1.AddProduct(new Product("Wireless Mouse", "MOU456", 25.50, 2));

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Smartphone", "PHN789", 599.99, 1));
        order2.AddProduct(new Product("Headphones", "HDP101", 49.99, 1));
        order2.AddProduct(new Product("Charging Cable", "CHG202", 15.00, 2));

        // Display order details
        Console.WriteLine("Order 1:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalCost():F2}\n");

        Console.WriteLine("Order 2:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalCost():F2}");
    }
}
