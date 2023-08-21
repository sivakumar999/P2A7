using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
    class Program
    {
        static string conStr = "server=DESKTOP-KQQHHP5\\SQLEXPRESS; database=LibraryDb; trusted_connection=true;";

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("1. Display Books");
                    Console.WriteLine("2. Add a Book");
                    Console.WriteLine("3. Update Book Quantity");
                    Console.WriteLine("4. Exit");
                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            DisplayBooks();
                            break;
                        case 2:
                            AddBook();
                            break;
                        case 3:
                            UpdateBookQuantity();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            Console.ReadKey();
        }

        static void DisplayBooks()
        {
            try
            {
                DataSet ds = RetrieveBooks();
                DataTable dt = ds.Tables[0];

                Console.WriteLine("List of Books in the Library:");
                Console.WriteLine("--------------------------------");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"Book ID: {row["BookId"]}");
                    Console.WriteLine($"Title: {row["Title"]}");
                    Console.WriteLine($"Author: {row["Author"]}");
                    Console.WriteLine($"Genre: {row["Genre"]}");
                    Console.WriteLine($"Quantity: {row["Quantity"]}");
                    Console.WriteLine("--------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while displaying books: " + ex.Message);
            }
        }

        static DataSet RetrieveBooks()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    string query = "SELECT * FROM Books";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving books: " + ex.Message);
                return null;
            }
        }

        static void AddBook()
        {
            try
            {
                Console.Write("Enter Title: ");
                string title = Console.ReadLine();
                Console.Write("Enter Author: ");
                string author = Console.ReadLine();
                Console.Write("Enter Genre: ");
                string genre = Console.ReadLine();
                Console.Write("Enter Quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                DataSet ds = RetrieveBooks();
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["Title"] = title;
                newRow["Author"] = author;
                newRow["Genre"] = genre;
                newRow["Quantity"] = quantity;
                dt.Rows.Add(newRow);
                UpdateDatabase(ds);
                Console.WriteLine("Book added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding a book: " + ex.Message);
            }
        }

        static void UpdateBookQuantity()
        {
            try
            {
                Console.Write("Enter Title of the Book to Update: ");
                string title = Console.ReadLine();
                Console.Write("Enter New Quantity: ");
                int newQuantity = Convert.ToInt32(Console.ReadLine());

                DataSet ds = RetrieveBooks();
                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                {
                    if (string.Equals(row["Title"].ToString(), title, StringComparison.OrdinalIgnoreCase))
                    {
                        row["Quantity"] = newQuantity;
                        UpdateDatabase(ds);
                        Console.WriteLine("Quantity updated successfully.");
                        return;
                    }
                }

                Console.WriteLine("Book not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating book quantity: " + ex.Message);
            }
        }

        static void UpdateDatabase(DataSet ds)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    string query = "SELECT * FROM Books";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(ds);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the database: " + ex.Message);
            }
        }
    }
}
