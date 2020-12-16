using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Contact_Number_Diary.Classes
{
    class ContactKeeper
    {
        //Static Connection String
        private static string connectionString = "server=localhost;user id=root;database=contactnumbers;pwd=password";
        //Properties(get & set)
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        //Methods

        //To retreive records from database
        public DataTable Select()
        {
            //Instantiating data table
            DataTable dt = new DataTable();
            //MySQL Connection
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                //SQL Query
                string SQLQuery = "SELECT Contact_ID AS CID,First_Name AS 'First Name',Last_Name as 'Last Name',Contact_Number AS 'Contact NO',Address,Gender FROM Users";
                //Instantiating SQL Commant
                MySqlCommand cmd = new MySqlCommand(SQLQuery, conn);
                //Open connection
                conn.Open();
                //Load datatable
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close connection
                conn.Close();
            }
            //Return datatable
            return dt;
        }
        //To insert records in Database
        public bool Insert(ContactKeeper c)
        {
            //declaring a default bool variable and initializing false
            bool isSucess = false;
            //MySQL Connection
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                //SQL Query
                string SQLQuery = "INSERT INTO Users(First_Name,Last_Name,Contact_Number,Address,Gender) VALUES(@First_Name,@Last_Name,@Contact_Number,@Address,@Gender)";
                //MYSQL Command
                MySqlCommand cmd = new MySqlCommand(SQLQuery, conn);
                //Creating parameters to add data
                cmd.Parameters.AddWithValue("@First_Name", c.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", c.LastName);
                cmd.Parameters.AddWithValue("@Contact_Number", c.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                //Connection Open
                conn.Open();
                //Execute Query
                //Here ExecuteNonQuery() returns the number of rows affected 
                int row = cmd.ExecuteNonQuery();
                isSucess = row > 0 ? true : false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close connection
                conn.Close();
            }

            return isSucess;
        }
        //To delete records of Database
        public bool Delete(ContactKeeper c)
        {
            //Declaring default boolean variable and initializing it false
            bool isSucess = false;
            //MySQL Connection
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                //SQL Query
                string SQLQuery = "DELETE FROM Users WHERE Contact_ID=@Contact_ID";
                //MySQL Command
                MySqlCommand cmd = new MySqlCommand(SQLQuery,conn);
                //Creating parameters to add data
                cmd.Parameters.AddWithValue("@Contact_ID", c.ContactID);
                //Open Connection
                conn.Open();
                //Execute Query
                //and ExecuteNonQuery() returns the number of rows affected
                int row = cmd.ExecuteNonQuery();
                isSucess = row > 0 ? true : false;

            }
            catch(Exception ex)
            {
               MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close Connection
                conn.Close();
            }
            return isSucess;
        }
        //To update records of Database
        public bool Update(ContactKeeper c)
        {
            //Declaring default boolean variable and initializing false
            bool isSucess = false;
            //MySQL Connection
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                //SQLQuery
                string SQLQuery = "UPDATE Users SET First_Name=@First_Name,Last_Name=@Last_Name,Contact_Number=@Contact_Number,Address=@Address,Gender=@Gender WHERE Contact_ID=@Contact_ID";
                //SQL Command
                MySqlCommand cmd = new MySqlCommand(SQLQuery, conn);
                //Creating parameters to add Data
                cmd.Parameters.AddWithValue("@First_Name", c.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", c.LastName);
                cmd.Parameters.AddWithValue("@Contact_Number", c.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@Contact_ID", c.ContactID);
                //Connection Open
                conn.Open();
                //Execute Query
                //and ExecuteNonQuery() returns the number of rows affected
                int row = cmd.ExecuteNonQuery();
                isSucess = row > 0 ? true : false;


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close connection
                conn.Close();
            }
            return isSucess;

        }
        //To Search record from database
        public DataTable Search(string searchString)
        {
            //Instanatiating DataTable
            DataTable dt = new DataTable();
            //MySQL Connction
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                //SQL Query
                string SQLQuery = "SELECT Contact_ID AS CID,First_Name AS `First Name`,Last_Name as `Last Name`,Contact_Number AS `Contact NO`,Address,Gender FROM Users Where Contact_ID LIKE '%" + searchString+"%' OR First_Name LIKE '%"+searchString+"%' OR Last_Name LIKE '%"+searchString+"%' OR Contact_Number LIKE '%" + searchString +"%' OR Address LIKE '%"+searchString +"%' OR Gender LIKE '%" + searchString + "%'";
                //SQL Command
                MySqlCommand cmd = new MySqlCommand(SQLQuery,conn);
                //Create parameters to add data
                cmd.Parameters.AddWithValue("@searchString", searchString);
                //Connection Open
                conn.Open();
                //Execute Query
                dt.Load(cmd.ExecuteReader());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                //Close Connection
                conn.Close();
            }
            //Return Datatable
            return dt;

        }
    }
}
//Pramesh Karki