using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LINQLab.Models;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

namespace LINQLab
{
    class Problems
    {
        private EcommerceContext _context;

        public Problems()
        {
            _context = new EcommerceContext();
        }
        public void RunLINQQueries()
        {
            //// <><><><><><><><> R Actions (Read) <><><><><><><><><>
            //RDemoOne();
            //RProblemOne();
            //RDemoTwo();
            ////RProblemTwo();
            ////RProblemThree();
            ////RProblemFour();
            ///*RProblemFive*/();

            ////// <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>
            //RDemoThree();
            //RProblemSix();
            //RProblemSeven();
            //RProblemEight();

            //// <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

            //// <><> C Actions (Create) <><>
            //CDemoOne();
            //CProblemOne();
            //CDemoTwo();
            //CProblemTwo();

            //// <><> U Actions (Update) <><>
            //UDemoOne();
            //UProblemOne();
            //UProblemTwo();

            //// <><> D Actions (Delete) <><>
            //DDemoOne();
            //DProblemOne();
            DProblemTwo();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void RDemoOne()
        {
            // This LINQ query will return all the users from the User table.
            var users = _context.Users.ToList();

            Console.WriteLine("RDemoOne: Emails of All users");
            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void RProblemOne()
        {
            // Print the COUNT of all the users from the User table.
            var count = _context.Users.Count();
            Console.WriteLine($"RProblemOne: Total number of users: {count}");
        }

        /*
       Expected Result:
       User Count: 5
        */

        public void RDemoTwo()
        {
            // This LINQ query will get each product whose price is greater than $150.
            var productsOver150 = _context.Products.Where(p => p.Price > 150);
            Console.WriteLine("RDemoTwo: Products greater than $150");
            foreach (Product product in productsOver150)
            {
                Console.WriteLine($"{product.Name} - ${product.Price}");
            }
        }

        public void RProblemTwo()
        {
            // Write a LINQ query that gets each product whose price is less than or equal to $100.
            // Print the name and price of all products
            var productsUnder100 = _context.Products.Where(p => p.Price <= 100);
            Console.WriteLine("RProblemTwo: Products less than or equal to $100");
            foreach (Product product in productsUnder100)
            {
                Console.WriteLine($"{product.Name} - ${product.Price}");
            }

        }

        /*
            Expected Result:

            Name: Freedom from the Known - Jiddu Krishnamurti
            Price: $14.99

            Name: Ball Mason Jar-32 oz.
            Price: $8.85

            Name: Catan The Board Game
            Price: $43.67
         */

        public void RProblemThree()
        {
            // Write a LINQ query that gets each product whose name that CONTAINS an "s".
            var productsThatContainS = _context.Products.Where(Product => Product.Name.Contains("s", StringComparison.OrdinalIgnoreCase))
                .ToList();

            Console.WriteLine("RProblemThree: Users with Names Containing 's'");

            foreach (var product in productsThatContainS)

            {
                Console.WriteLine(product.Name);
            }

        }
        /*
            Expected Result:

            Name: Freedom from the Known - Jiddu Krishnamurti

            Name: Ball Mason Jar-32 oz.

            Name: Stellar Basic Flute Key of G - Native American Style Flute

            Name: Apple Watch Series 3

            Name: Nintendo Switch
         */

        public void RProblemFour()
        {
            // Write a LINQ query that gets all the users who registered BEFORE 2016.
            // Then print each user's email and registration date to the console.
            var before2016count = _context.Users
            .Where(user => user.RegistrationDate < new DateTime(2016, 1, 1))
            .ToList();

            Console.WriteLine("Users Registrated Before 2016");

            foreach (var user in before2016count)

            {

                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Registration Date: {user.RegistrationDate}");

            }

        }
        /*
            Expected Result:

            Email: janett@gmail.com
            Registration Date: 10/15/2015 12:00:00 AM
            Email: gary@gmail.com
            Registration Date: 10/15/2012 12:00:00 AM
         */

        public void RProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018.
            // Then print each user's email and registration date to the console.
            var usersRegisteredAfter2016Before2018 = _context.Users
                .Where(user => user.RegistrationDate > new DateTime(2016, 1, 1) && user.RegistrationDate < new DateTime(2018, 1, 1))
                .ToList();

            Console.WriteLine("Users Registrated After 2016 and Before 2018");

            foreach (var user in usersRegisteredAfter2016Before2018)

            {
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Registration Date: {user.RegistrationDate}");

            }

        }
        /*
            Expected Result:

            Email: bibi@gmail.com
            Registration Date: 4/6/2017 12:00:00 AM
         */

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void RDemoThree()
        {
            // This LINQ query will retreive all of the users who are assigned to the role of Customer.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            Console.WriteLine("RDemoThree: Customer Users");
            foreach (UserRole userrole in customerUsers)
            {
                Console.WriteLine($"Email: {userrole.User.Email} Role: {userrole.Role.RoleName}");
            }
        }
        public void RProblemSix()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var productsInUsersShoppingCart = _context.ShoppingCartItems
                .Where(item => item.User.Email == "afton@gmail.com")
                .Select(item => new 
                { ProductName = item.Product.Name, ProductPrice = item.Product.Price, ProductQuantity = item.Quantity})
                .ToList();

            Console.WriteLine("RProblemSix: Products In Users Shopping Cart");
            Console.WriteLine();
            foreach (var item in productsInUsersShoppingCart)

            {
                Console.WriteLine($"Name: {item.ProductName}");
                Console.WriteLine($"Price: {item.ProductPrice}");
                Console.WriteLine($"Quantity:{item.ProductQuantity}");
                Console.WriteLine();
            }

        }
        /*
            Expected Result:
            Name: Freedom from the Known - Jiddu Krishnamurti
            Price: $14.99
            Quantity: 1

            Name: Ball Mason Jar-32 oz.
            Price: $8.85
            Quantity: 10

            Name: Catan The Board Game
            Price: $43.67
            Quantity: 1

            Name: Nintendo Switch
            Price: $299.00
            Quantity: 1
        */

        public void RProblemSeven()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Print the total of the shopping cart to the console.
            // Remember to break the problem down and take it one step at a time!
            var sumOfShoppingCartItems = _context.ShoppingCartItems
                .Where(item => item.User.Email == "oda@gmail.com")
                .Select(sc => sc.Product.Price)
                .Sum();

            Console.WriteLine($"Total: ${sumOfShoppingCartItems}");


        }
        /*
         Total: $715.34
         */

        public void RProblemEight()
        {
            // Write a query that retrieves all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the product's name, price, and quantity to the console along with the email of the user that has it in their cart.
            var employeeCartItems = _context.ShoppingCartItems
                .Where(item => item.User.UserRoles.Any(role => role.Role.RoleName == "Employee"))
                .Select(item => new
                {
                    UserEmail = item.User.Email,
                    ProductName = item.Product.Name,
                    ProductPrice = item.Product.Price,
                    ProductQuantity = item.Quantity
                })
                .ToList();

            Console.WriteLine("Products in the Shopping Carts of Employees");
            Console.WriteLine();
            foreach (var item in employeeCartItems)
            {
                Console.WriteLine($"User Email: {item.UserEmail}");
                Console.WriteLine("-----------");
                Console.WriteLine($"Product Name: {item.ProductName}");
                Console.WriteLine($"Price: {item.ProductPrice}");
                Console.WriteLine($"Quantity: {item.ProductQuantity}");
                Console.WriteLine();
                Console.WriteLine();


            }


        }
        /*
            Expected Result

            User's email: bibi@gmail.com
            -----------
            Product name: Apple Watch Series 3
            Price: 169.00
            Quantity:5



            User's email: janett@gmail.com
            -----------
            Product name: Freedom from the Known - Jiddu Krishnamurti
            Price: 14.99
            Quantity:1

            Product name: Catan The Board Game
            Price: 43.67
            Quantity:1
         */

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        //IMPORTANT: The following methods will MODIFY your database. Even if you stop and restart the application, any changes made to the database will persist!
        //Calling a Create method more than once will result in duplicate data added to the table.
        //Calling an Update or Delete method more than once might cause an error. For instance, if you call a method that deletes the Nintendo Switch from the database, then try to call the method again, there will no longer be a Nintendo Switch to delete!
        //You may want to use Breakpoints or WriteLines to verify your LINQ Queries are finding the correct items before you add the .SaveChanges() to the method!

        // <><> C Actions (Create) <><>

        private void CDemoOne()
        {
            // This will create a new User object and add it to the Users table.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

        }

        private void CProblemOne()
        {
            // Create a new Product object and add that product to the Products table. Choose any name and product info you like.
            Product newProduct = new Product()
            {
               
                Name = "Playstation 4",
                Description = "Gaming Console",
                Price = 500
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        public void CDemoTwo()
        {
            // This will add the role of "Customer" to the user we created in CDemoOne by adding a new row to the Userroles junction table.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserrole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserrole);
            _context.SaveChanges();
            // If you encounter problems running this method, it likely means you ran CDemoOne multiple times and have created duplicate customers with the email "david@gmail.com"
        }

        public void CProblemTwo()
        {
            // Create a new ShoppingCartItem to represent the new product you created in CProblemOne being added to the shopping cart of the user created in CDemoOne.
            // This will add a new row to ShoppingCart junction table.
            var produtId = _context.Products.Where(p => p.Name == "Playstation 4").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            ShoppingCartItem newShoppingCartItem = new ShoppingCartItem()
            {
                UserId = userId,
                ProductId = produtId
            };
            _context.ShoppingCartItems.Add(newShoppingCartItem);
            _context.SaveChanges();

        }


        // <><> U Actions (Update) <><>

        private void UDemoOne()
        {
            // This will update the email of the user from CDemoOne to "dan@gmail.com"
            // Remember that after this update occurs, there should be no more "david@gmail.com" on the database!
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "dan@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void UProblemOne()
        {
            // Update the price of the product you created in CProblemOne to something different using LINQ.
            var product = _context.Products.Where(p => p.Price == 500).SingleOrDefault();
            product.Price = 450;
            _context.Products.Update(product);
            _context.SaveChanges();

        }

        private void UProblemTwo()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new Userrole object and add it to the Userroles table
            // See the DDemoOne below as an example of removing a role relationship
            var oldRole = _context.UserRoles.Where(u => u.Id == 6).SingleOrDefault();
            _context.UserRoles.Remove(oldRole);
            _context.SaveChanges();

            var roleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "dan@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();


        }

        // <><> D Actions (Delete) <><>

        private void DDemoOne()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userrole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userrole);

            _context.SaveChanges();

        }

        private void DProblemOne()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Use a Loop
            User userToDelete = _context.Users.FirstOrDefault(user => user.Email == "oda@gmail.com");

            var productsInCart = _context.ShoppingCartItems
                .Where(item => item.UserId == userToDelete.Id)
                .ToList();
            foreach (var productInCart in productsInCart)
            {
                _context.ShoppingCartItems.Remove(productInCart);
                 
            }
            _context.SaveChanges();
        }

        private void DProblemTwo()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();


        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".

            Console.WriteLine("Enter Email: ");

        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in, give them a menu where they can perform the following actions within the console:
            // -View the products in their shopping cart
            // -View all products in the Products table
            // -Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // -Remove a product from their shopping cart
            // 3. If the user does not successfully sign in
            // -Display "Invalid Email or Password"
            // -Re-prompt the user for credentials

        }

    }
}
