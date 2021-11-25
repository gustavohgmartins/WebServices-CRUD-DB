using System;
using System.Data;
using System.Data.SqlClient;
using wstest.Models;
using System.Collections.Generic;


namespace webServices.DAO
{
    public class UserDAO{
        //DB connection parameters
        string strConnection = "Server=localhost; Database=webServices; Trusted_Connection=True;";
        SqlConnection connection = null;
        

        //Returns the users list
        public List<User> Users(){
            try{
                connection = new SqlConnection(strConnection);
                connection.Open();
                string queryString = "select * from Users";
                SqlCommand command = new SqlCommand (queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read()){
                    List<User> usersList = new List<User>();
                    User user = new User();
                    //Creates an object "user" for each registered user and adds it into the users list
                    do{
                        user = new User(); 
                        user.name = (string)reader["name"];
                        user.age = (int)reader["age"];
                        user.cpf = (string)reader["cpf"];
                        usersList.Add(user);
                    }while(reader.Read());
                    return usersList;
                }
                return null;
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return null;
            }
            finally{
                if(connection != null){
                connection.Close();
                }
            }
        }

        //Add a user into the DB
        public bool addUser(string cpf, string name, string age){
            try{
                connection = new SqlConnection(strConnection);
                connection.Open();
                string queryString = "insert into Users (cpf, name, age) values(@cpf, @name, @age)";
                SqlCommand command = new SqlCommand (queryString, connection);
                //Creates parameters so the method can be dinamic and secure (prevents injections)
                command.Parameters.Add( new SqlParameter("@cpf",cpf));
                command.Parameters.Add( new SqlParameter("@name",name));
                command.Parameters.Add( new SqlParameter("@age",age));
                command.ExecuteNonQuery();
                return true;
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return false;    
            }
            finally{
                if(connection != null){
                connection.Close();
                }
            }
        }

        public bool removeUser(string cpf){
            try{
                connection = new SqlConnection(strConnection);
                connection.Open();
                string queryString = "delete from Users where cpf = @cpf";
                SqlCommand command = new SqlCommand (queryString, connection);
                command.Parameters.Add( new SqlParameter("@cpf",cpf));
                int remove = command.ExecuteNonQuery();
                if(remove > 0){
                return true;
                }
                return false;
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
            finally{
                if(connection != null){
                connection.Close();
                }
            }
        }

        //Returns an user selected by cpf
        public User getUser(string cpf){
            try{
                connection = new SqlConnection(strConnection);
                connection.Open();
                string queryString = "select * from Users where cpf = @cpf";
                SqlCommand command = new SqlCommand (queryString, connection);
                command.Parameters.Add( new SqlParameter("@cpf",cpf));
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read()){
                    //Gets date from the DB and creates a new object user
                    User user = new User();
                    user.name = (string)reader["name"];
                    user.age = (int)reader["age"];
                    user.cpf = (string)reader["cpf"];

                    reader.Close();
                    return user;
                }
                return null;
            }
        
            catch(Exception e){
                Console.WriteLine(e.Message);
                return null;
            }
            finally{
                if(connection != null){
                connection.Close();
                }
            }
        }

        //Updates an user's data (except the cpf, which is the Primary key)
        public bool updateUser(string cpf, string name, string age){
            try{
                connection = new SqlConnection(strConnection);
                connection.Open();
                string queryString = "update Users set name = @name, age = @age where cpf = @cpf";
                SqlCommand command = new SqlCommand (queryString, connection);
                command.Parameters.Add( new SqlParameter("@cpf",cpf));
                command.Parameters.Add( new SqlParameter("@name",name));
                command.Parameters.Add( new SqlParameter("@age",age));
                command.ExecuteNonQuery();
                return true;
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
            finally{
                if(connection != null){
                connection.Close();
                }
            }
        }
    }
}