using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contact_Number_Diary.Classes;
using MySql.Data.MySqlClient;

namespace Contact_Number_Diary
{
    public partial class mainWindow : Form
    {
        //Create object of ContactKeeper class Object
        ContactKeeper c = new ContactKeeper();
        //Constructor
        public mainWindow()
        {
            InitializeComponent();
        }
        //Handling click events on Add button
        private void addButton_Click(object sender, EventArgs e)
        {
            //Instantiating Properties
            c.FirstName = firstName.Text.Trim();
            c.LastName = lastName.Text.Trim();
            c.ContactNumber = contactNumber.Text.Trim();
            c.Address = address.Text.Trim();
            c.Gender = gender.Text;
            //Input validation
            if (c.FirstName == "" || c.LastName == "" || c.ContactNumber == "" || c.Address == "" || c.Gender == "")
            {
                //MessageBox with custome message
                MessageBox.Show("Fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (contactID.Text != "")
            {
                MessageBox.Show("Contact with same Contact ID already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Inserting records
                bool isSucess=c.Insert(c);
                if (isSucess)
                {
                    MessageBox.Show("New contact has been inserted sucessfully.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   //Restting all the input fields
                    ResetFields();
                }
                else
                {
                    MessageBox.Show("Error Occured.Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Updating grid data view
                LoadGridView();
            }

        }

        //Handling click events on linklabel
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Link to github profile
            System.Diagnostics.Process.Start("https://github.com/PrameshKarki");
        }
        //Method to reset input fields
        private void ResetFields()
        {
            contactID.Text = "";
            firstName.Text = "";
            lastName.Text = "";
            contactNumber.Text = "";
            gender.Text = "";
            address.Text = "";
        }
        //Method to update grid data view
        private void LoadGridView()
        {
            
            DataTable dt = c.Select();
            gridView.DataSource = dt;
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            LoadGridView();
            //Styling gridView
            gridView.ColumnHeadersHeight = 24;
            gridView.Columns[0].Width = 45;
            gridView.Columns[1].Width = 110;
            gridView.Columns[2].Width = 110;
        }
        //Method to handle click events on Clear button
        private void clearButton_Click(object sender, EventArgs e)
        {
            ResetFields();
        }
        //Method to handle click events on Update button
        private void updateButton_Click(object sender, EventArgs e)
        {
            c.FirstName = firstName.Text.Trim();
            c.LastName = lastName.Text.Trim();
            c.ContactNumber = contactNumber.Text.Trim();
            c.Address = address.Text.Trim();
            c.Gender = gender.Text;
            string temp = contactID.Text;
            //Validating inputs
            if (c.FirstName == "" || c.LastName == "" || c.ContactNumber == "" || c.Address == "" || c.Gender == "" || temp=="")
            {
                MessageBox.Show("Fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                c.ContactID =int.Parse(temp);
                bool isSucess = c.Update(c);
                if (isSucess)
                {
                    MessageBox.Show("Contact has been updated sucessfully.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFields();
                }
                else
                {
                    MessageBox.Show("Error Occured.Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadGridView();
            }


        }
        //Method to check current highlighted row
        private int GetRowIndex()
        {
            int index=gridView.CurrentCell.RowIndex;
            return index;
        }
        //Method to fetch data from grid data view to input fields
        private void FetchDataToTextField()
        {
            int index = GetRowIndex();
            contactID.Text = gridView.Rows[index].Cells[0].Value.ToString();
            firstName.Text = gridView.Rows[index].Cells[1].Value.ToString();
            lastName.Text = gridView.Rows[index].Cells[2].Value.ToString();
            contactNumber.Text = gridView.Rows[index].Cells[3].Value.ToString();
            address.Text = gridView.Rows[index].Cells[4].Value.ToString();
            gender.Text = gridView.Rows[index].Cells[5].Value.ToString();

        }
       //Handling click events on cell to determine which row to update/delete
        private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FetchDataToTextField();
        }
        //Handling click events on Delete button

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //Temprorarily hold contact number to check it is empty or not
            string temp = contactID.Text;
            if (temp == "")
            {
                MessageBox.Show("Select exitsing contact first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                c.ContactID = int.Parse(temp);
                bool isSucess = c.Delete(c);
                if (isSucess)
                {
                    MessageBox.Show("Contact has been deleted sucessfully.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFields();
                }
                else
                {
                    MessageBox.Show("Error Occured.Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadGridView();

            }
        }
        //Handling to textchange event to fetch searched results
        private void searchText_TextChanged(object sender, EventArgs e)
        {
            string searchString = searchText.Text;
            DataTable searchedTable = c.Search(searchString);
            gridView.DataSource = searchedTable;
        }
    }
}
//Pramesh Karki
//Vist:https://github.com/PrameshKarki
