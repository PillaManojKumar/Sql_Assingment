using System.Data.SqlClient;
namespace Ass1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Data source=IN-DQ3K9S3; Initial Catalog=Expense_Tracker; Integrated Security=true");
            conn.Open();

            while (true)
            {
                Console.WriteLine("Welcome to Expenses Tracker application");
                Console.WriteLine("1. AddTransaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Income");
                Console.WriteLine("4. Check Available Balance");


                Console.Write("Enter your choice (1-4): ");
                int choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        {
                            string title = "";
                            string description = "";
                            decimal amount = 0;
                            try
                            {
                                Console.Write("Enter Title: ");
                                title = Console.ReadLine();

                                Console.Write("Enter Description: ");
                                description = Console.ReadLine();

                                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
                                {
                                    throw new EmptyException();
                                }
                            }
                            catch (EmptyException)
                            {
                                Console.WriteLine("Fields should not be empty");
                                return;
                            }

                            try
                            {
                                Console.Write("Enter Amount: ");
                                amount = Convert.ToDecimal(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Enter only integer value");
                                return;
                            }

                            Console.Write("Enter Date (dd-MM-yyyy): ");
                            DateTime date = Convert.ToDateTime(Console.ReadLine());

                            string query = "INSERT INTO Transactions (Title, Description, Amount, Date) VALUES (@Title, @Description, @Amount, @Date)";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@Title", title);
                                cmd.Parameters.AddWithValue("@Description", description);
                                cmd.Parameters.AddWithValue("@Amount", amount);
                                cmd.Parameters.AddWithValue("@Date", date);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Transaction Added Successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Transaction Not Added!");
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            string query = "SELECT * FROM Transactions WHERE Amount < 0";
                            using (SqlCommand command = new SqlCommand(query, conn))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        Console.WriteLine("Expenses Transactions:");
                                        Console.WriteLine("--------------------");
                                        Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-10}", "Title", "Description", "Amount", "Date");
                                        while (reader.Read())
                                        {
                                            Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-10}", reader["Title"], reader["Description"], reader["Amount"], reader["Date"]);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No Expenses Transactions Found!");
                                    }
                                }
                            }
                            break;
                        }
                        Console.WriteLine();

                    case 3:
                        {
                            string query = "SELECT * FROM Transactions WHERE Amount > 0";
                            using (SqlCommand command = new SqlCommand(query, conn))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        Console.WriteLine("Income Transactions:");
                                        Console.WriteLine("--------------------");
                                        Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-10}", "Title", "Description", "Amount", "Date");
                                        while (reader.Read())
                                        {
                                            Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-10}", reader["Title"], reader["Description"], reader["Amount"], reader["Date"]);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No Income Transactions Found!");
                                    }
                                }
                            }
                            break;
                        }

                    case 4:
                        {

                            decimal totalIncome = 0;
                            decimal totalExpense = 0;

                            // Calculate total income
                            SqlCommand command = new SqlCommand("SELECT SUM(Amount) FROM Transactions WHERE Amount > 0", conn);
                            var result = command.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                totalIncome = Convert.ToDecimal(result);
                            }

                            // Calculate total expense
                            command.CommandText = "SELECT SUM(Amount) FROM Transactions WHERE Amount < 0";
                            result = command.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                totalExpense = Convert.ToDecimal(result);
                            }


                            decimal balance = totalIncome + totalExpense;
                            Console.WriteLine($"Available Balance: {balance}");
                            break;
                        }

                    default:
                        Console.WriteLine("Wrong Choice Entered!");
                        break;
                }

                Console.WriteLine();
            }
            conn.Close();
        }
    }
}