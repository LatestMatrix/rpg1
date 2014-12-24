using UnityEngine;

using System;
using System.Collections;
using Mono.Data.Sqlite;


public class DB
{

    private SqliteConnection _dbConnection;
    private SqliteCommand _dbCommand;
    private SqliteDataReader _reader;
    private string _name;

    public DB(string name, string password = null)
    {
        OpenDB(name, password);
    }

    public DB()
    {

    }

    public void OpenDB(string name, string password = null)
    {
        _name = name;
        try
        {
            _dbConnection = new SqliteConnection(_name);
            if (password != null)
            {
                _dbConnection.SetPassword(password);
            }
            _dbConnection.Open();
            Log.Trace("Connected to db " + _name);
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
    }

    public void CloseSqlConnection()
    {
        if (_dbCommand != null)
        {
            _dbCommand.Dispose();
        }

        _dbCommand = null;
        if (_reader != null)
        {
            _reader.Dispose();
        }
        _reader = null;
        if (_dbConnection != null)
        {
            _dbConnection.Close();
        }
        _dbConnection = null;
        Log.Trace("Disconnected from db " + _name);
    }

    public void ChangePassword(string password)
    {
        try
        {
            _dbConnection.ChangePassword(password);
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
    }

    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        _dbCommand = _dbConnection.CreateCommand();
        _dbCommand.CommandText = sqlQuery;
        _reader = _dbCommand.ExecuteReader();
        return _reader;
    }

    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }

        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }
        query += " WHERE " + selectkey + " = " + selectvalue + " ";
        return ExecuteQuery(query);
    }

    public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + cols[0];

        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];

        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }

    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];

        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];

        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
        }
        return ExecuteQuery(query);
    }
}

