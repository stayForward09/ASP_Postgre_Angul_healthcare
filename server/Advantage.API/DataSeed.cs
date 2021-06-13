using System;
using System.Linq;
using Advantage.API;
using System.Collections.Generic;


namespace Advantage.API
{
    public class DataSeed
    {
        private readonly ApiContext _ctx;

        public DataSeed(ApiContext ctx)
        {
            this._ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders)
        {
            if (!_ctx.Customers.Any())
            {
                SeedCustomers(nCustomers); 
                _ctx.SaveChanges();
            }

            if (!_ctx.Orders.Any())
            {
                SeedOrders(nCustomers); 
                _ctx.SaveChanges();
            }

            if (!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }
            
        }


        private void SeedCustomers(int n){
            List<Customer> customers = BuildCustomerList(n);

            foreach(var customer in customers)
            {
                _ctx.Customers.Add(customer);
            }
        }


        private void SeedOrders(int n)
        {
            List<Order> orders = BuildOrderList(n);

            foreach(var order in orders)
            {
                _ctx.Orders.Add(order);
            }
        }


        public void SeedServers()
        {
            List<Server> servers = BuildServerList();

            foreach(var server in servers)
            {
                _ctx.Servers.Add(server);
            }
        }

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
            }
            return customers;
        }   


         


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
        } 



        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();
            var rand = new Random();

            for(var i = 1; i <= nOrders; i++)
            {
                var randCustomerId = rand.Next(_ctx.Customers.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);

                orders.Add(new Order{
                    Id = i,
                    Customer = _ctx.Customers.First(c => c.Id == randCustomerId),
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
                
            }
            return orders;
        }




    }
    
}