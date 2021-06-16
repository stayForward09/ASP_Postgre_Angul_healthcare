using System;
using System.Linq;
using System.Collections.Generic;


namespace Advantage.API
{
    public class DataSeed
    {
        private readonly ApiContext _ctx;


        //constructor
        public DataSeed(ApiContext ctx)
        {
            this._ctx = ctx;
        }


        //save data
        public void SeedData(int nCustomers, int nOrders)
        {
            if (!_ctx.Customers.Any())
            {
                SeedCustomers(nCustomers); 
                _ctx.SaveChanges();
            }

            if (!_ctx.Orders.Any())
            {
                SeedOrders(nOrders); 
                _ctx.SaveChanges();
            }

            if (!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }
            
        }//end SeedData method



        // load customers 
        private void SeedCustomers(int n){
            List<Customer> customers = BuildCustomerList(n);

            foreach(var customer in customers)
            {
                _ctx.Customers.Add(customer);
            }//end for

        }//end SeedCustomers



        //load orders
        private void SeedOrders(int n)
        {
            List<Order> orders = BuildOrderList(n);

            foreach(var order in orders)
            {
                _ctx.Orders.Add(order);
            }//end for

        }//end SeedOrders



        // load servers
        public void SeedServers()
        {
            List<Server> servers = BuildServerList();

            foreach(var server in servers)
            {
                _ctx.Servers.Add(server);
            }//end for

        }//end SeedServers



        // Creates alist of customers
        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customers = new List<Customer>();
            var names = new List<string>();

            for (var i = 1; i <= nCustomers; i++)
            {
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);

                customers.Add(new Customer {
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState()

                });
            }//end for

            return customers;
        } //end BuildCustomersList



        // creates the list of orders
        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();
            var rand = new Random();

            for(var i = 1; i <= nOrders; i++)
            {
                var randCustomerId = rand.Next(1,_ctx.Customers.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                var customers = _ctx.Customers.ToList();

                orders.Add(new Order{
                    Id = i,
                    Customer = customers.First(c => c.Id == randCustomerId),
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
                
            }//end for 

            return orders;
        }//end BuildOrderList




        // Creates the list of servers
        private List<Server> BuildServerList()
        {
            return new List<Server>()
        
            {
                new Server{
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                
                new Server{
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false
                },
    
                new Server{
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                new Server{
                    Id = 4,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server{
                    Id = 5,
                    Name = "Prod-Mail",
                    IsOnline = true
                },
                new Server{
                    Id = 6,
                    Name = "Prod-Services",
                    IsOnline = true
                },
                new Server{
                    Id = 7,
                    Name = "QA-Web",
                    IsOnline = true
                },
                new Server{
                    Id = 8,
                    Name = "QA-Mail",
                    IsOnline = true
                },
                new Server{
                    Id = 9,
                    Name = "QA-Services",
                    IsOnline = true
                }
                 
            };
        } //end BuildServer List



    }//end class
    
}//end namespace