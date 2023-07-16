using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Serialization;
using System.IO;

/*************************************************************************/
/* Program Name:     xml2db.cs                                         	 */
/* Date:             05/01/2022                                          */
/* Programmer:       Miranda Morris                                      */
/* Class:            CSCI 234                               	         */
/*                                                     				     */
/* Program Description: The purpose of this program is to create and copy*/
/* an customer.xml file and database file to the debug file.             */
/*                                                                       */
/* Input: data from a database			                                 */
/*                                                                       */
/* Output: a customer.xml file and database file				         */
/*                                                                       */
/* Givens: database data                         					     */
/*                                                                       */
/* Testing Data:                                                         */
/*                                                                       */
/* List the Testing Input Data: I ran the program then checked the debug */
/* folder and both the customer.xml and database files were there        */
/*************************************************************************/

public class XML2DB
{
    static string connectString;
    static string commandString;
    static SQLiteConnection connection = null;
    static SQLiteCommand sqlCmd;

    public class Customer
    {
        public string CustID;
        public string FirstName;
        public string LastName;
        public string Address;
        public string City;
        public string State;
        public string Zip;
        public string ZipExt;
        public string AreaCode;
        public string Phone;
        public string CellAreaCode;
        public string CellPhone;
        public string Email;
    }

    static void SerializeObject(string filename)
    {
        SQLiteDataReader reader = null;
      
        XmlSerializer serializer = new XmlSerializer(typeof(Customer));

       
        TextReader Reader = new StreamReader(filename);

        
        Customer i = new Customer();

        i = (Customer)serializer.Deserialize(Reader);

        connectString = "Data Source=Simple.db";



        commandString = "insert into Customer values (";
        commandString += "'";
        commandString += i.CustID;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.FirstName;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.LastName;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.Address;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.City;
        commandString += "'";
        commandString += ",";
        commandString += "'";
        commandString += i.State;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.Zip;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.ZipExt;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.AreaCode;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.Phone;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.CellAreaCode;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.CellPhone;
        commandString += "'";
        commandString += ", ";
        commandString += "'";
        commandString += i.Email;
        commandString += "'";
        commandString += ")";

        try
        {
            connection = new SQLiteConnection(connectString);
            connection.Open();

            sqlCmd = new SQLiteCommand();
            sqlCmd.CommandText = commandString;
            sqlCmd.Connection = connection;

            sqlCmd.ExecuteNonQuery();
        }


        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        finally
        {
            if (reader != null) reader.Close();
            if (connection != null) connection.Close();
        }

        Reader.Close();
    }
    static void Main(string[] args)
    {
        SerializeObject("Customer.xml");
    }
}

