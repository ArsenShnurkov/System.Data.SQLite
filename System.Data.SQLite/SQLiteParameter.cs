﻿/********************************************************
 * ADO.NET 2.0 Data Provider for SQLite Version 3.X
 * Written by Robert Simpson (robert@blackcastlesoft.com)
 * 
 * Released to the public domain, use at your own risk!
 ********************************************************/

namespace System.Data.SQLite
{
  using System;
  using System.Data;
  using System.Data.Common;

  /// <summary>
  /// SQLite implementation of DbParameter.
  /// </summary>
  public sealed class SQLiteParameter : DbParameter
  {
    /// <summary>
    /// The data type of the parameter
    /// </summary>
    private int            _dbType;
    /// <summary>
    /// The version information for mapping the parameter
    /// </summary>
    private DataRowVersion _rowVersion;
    /// <summary>
    /// The value of the data in the parameter
    /// </summary>
    private Object         _objValue;
    /// <summary>
    /// The source column for the parameter
    /// </summary>
    private string         _sourceColumn;
    /// <summary>
    /// The column name
    /// </summary>
    private string         _columnName;
    /// <summary>
    /// The data size, unused by SQLite
    /// </summary>
    private int            _dataSize;

    /// <summary>
    /// Default constructor
    /// </summary>
    public SQLiteParameter()
    {
      Initialize(null, -1, 0, null, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs a named parameter given the specified parameter name
    /// </summary>
    /// <param name="parameterName">The parameter name</param>
    public SQLiteParameter(string parameterName)
    {
      Initialize(parameterName, -1, 0, null, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs a named parameter of the specified type
    /// </summary>
    /// <param name="parameterName">The parameter name</param>
    /// <param name="dbType">The datatype of the parameter</param>
    public SQLiteParameter(string parameterName, DbType dbType)
    {
      Initialize(parameterName, (int)dbType, 0, null, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs a named parameter of the specified type and source column reference
    /// </summary>
    /// <param name="parameterName">The parameter name</param>
    /// <param name="dbType">The data type</param>
    /// <param name="sourceColumn">The source column</param>
    public SQLiteParameter(string parameterName, DbType dbType, string sourceColumn)
    {
      Initialize(parameterName, (int)dbType, 0, sourceColumn, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs a named parameter of the specified type, source column and row version
    /// </summary>
    /// <param name="parameterName">The parameter name</param>
    /// <param name="dbType">The data type</param>
    /// <param name="sourceColumn">The source column</param>
    /// <param name="rowVersion">The row version information</param>
    public SQLiteParameter(string parameterName, DbType dbType, string sourceColumn, DataRowVersion rowVersion)
    {
      Initialize(parameterName, (int)dbType, 0, sourceColumn, rowVersion);
    }

    /// <summary>
    /// Constructs an unnamed parameter of the specified data type
    /// </summary>
    /// <param name="dbType">The datatype of the parameter</param>
    public SQLiteParameter(DbType dbType)
    {
      Initialize(null, (int)dbType, 0, null, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs an unnamed parameter of the specified data type and source column
    /// </summary>
    /// <param name="dbType">The datatype of the parameter</param>
    /// <param name="sourceColumn">The source column</param>
    public SQLiteParameter(DbType dbType, string sourceColumn)
    {
      Initialize(null, (int)dbType, 0, sourceColumn, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs an unnamed parameter of the specified data type, source column and row version
    /// </summary>
    /// <param name="dbType">The data type</param>
    /// <param name="sourceColumn">The source column</param>
    /// <param name="rowVersion">The row version information</param>
    public SQLiteParameter(DbType dbType, string sourceColumn, DataRowVersion rowVersion)
    {
      Initialize(null, (int)dbType, 0, sourceColumn, rowVersion);
    }

    /// <summary>
    /// Constructs a named parameter of the specified type and size
    /// </summary>
    /// <param name="parameterName">The parameter name</param>
    /// <param name="dbType">The data type</param>
    /// <param name="nSize">The size of the parameter</param>
    public SQLiteParameter(string parameterName, DbType dbType, int nSize)
    {
      Initialize(parameterName, (int)dbType, nSize, null, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs a named parameter of the specified type, size and source column
    /// </summary>
    /// <param name="parameterName">The name of the parameter</param>
    /// <param name="dbType">The data type</param>
    /// <param name="nSize">The size of the parameter</param>
    /// <param name="sourceColumn">The source column</param>
    public SQLiteParameter(string parameterName, DbType dbType, int nSize, string sourceColumn)
    {
      Initialize(parameterName, (int)dbType, nSize, sourceColumn, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs a named parameter of the specified type, size, source column and row version
    /// </summary>
    /// <param name="parameterName">The name of the parameter</param>
    /// <param name="dbType">The data type</param>
    /// <param name="nSize">The size of the parameter</param>
    /// <param name="sourceColumn">The source column</param>
    /// <param name="rowVersion">The row version information</param>
    public SQLiteParameter(string parameterName, DbType dbType, int nSize, string sourceColumn, DataRowVersion rowVersion)
    {
      Initialize(parameterName, (int)dbType, nSize, sourceColumn, rowVersion);
    }

    /// <summary>
    /// Constructs an unnamed parameter of the specified type and size
    /// </summary>
    /// <param name="dbType">The data type</param>
    /// <param name="nSize">The size of the parameter</param>
    public SQLiteParameter(DbType dbType, int nSize)
    {
      Initialize(null, (int)dbType, nSize, null, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs an unnamed parameter of the specified type, size, and source column
    /// </summary>
    /// <param name="dbType">The data type</param>
    /// <param name="nSize">The size of the parameter</param>
    /// <param name="sourceColumn">The source column</param>
    public SQLiteParameter(DbType dbType, int nSize, string sourceColumn)
    {
      Initialize(null, (int)dbType, nSize, sourceColumn, DataRowVersion.Current);
    }

    /// <summary>
    /// Constructs an unnamed parameter of the specified type, size, source column and row version
    /// </summary>
    /// <param name="dbType">The data type</param>
    /// <param name="nSize">The size of the parameter</param>
    /// <param name="sourceColumn">The source column</param>
    /// <param name="rowVersion">The row version information</param>
    public SQLiteParameter(DbType dbType, int nSize, string sourceColumn, DataRowVersion rowVersion)
    {
      Initialize(null, (int)dbType, nSize, sourceColumn, rowVersion);
    }

    /// <summary>
    /// Initializes the parameter member variables
    /// </summary>
    /// <param name="parameterName">The parameter name</param>
    /// <param name="dbType">The data type</param>
    /// <param name="nSize">The size</param>
    /// <param name="sourceColumn">The source column</param>
    /// <param name="rowVersion">The row version</param>
    private void Initialize(string parameterName, int dbType, int nSize, string sourceColumn, DataRowVersion rowVersion)
    {
      _columnName = parameterName;
      _dbType = dbType;
      _sourceColumn = sourceColumn;
      _rowVersion = rowVersion;
      _objValue = null;
      _dataSize = nSize;
    }

    /// <summary>
    /// Returns True.
    /// </summary>
    public override bool IsNullable
    {
      get
      {
        return true;
      }
      set 
      {
      }
    }

    /// <summary>
    /// Returns the datatype of the parameter
    /// </summary>
    public override DbType DbType
    {
      get
      {
        if (_dbType == -1) return DbType.String; // Unassigned default value is String
        return (DbType)_dbType;
      }
      set
      {
        _dbType = (int)value;
      }
    }

    /// <summary>
    /// Supports only input parameters
    /// </summary>
    public override ParameterDirection Direction
    {
      get
      {
        return ParameterDirection.Input;
      }
      set
      {
        if (value != ParameterDirection.Input)
          throw new NotImplementedException();
      }
    }

    /// <summary>
    /// Not implemented
    /// </summary>
    public override int Offset
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
      }
    }

    /// <summary>
    /// Returns the parameter name
    /// </summary>
    public override string ParameterName
    {
      get
      {
        return _columnName;
      }
      set
      {
        _columnName = value;
      }
    }

    /// <summary>
    /// Not implemented
    /// </summary>
    public override void ResetDbType()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the size of the parameter
    /// </summary>
    public override int Size
    {
      get
      {
        return _dataSize;
      }
      set
      {
        _dataSize = value;
      }
    }

    /// <summary>
    /// Gets/sets the source column
    /// </summary>
    public override string SourceColumn
    {
      get
      {
        return _sourceColumn;
      }
      set
      {
        _sourceColumn = value;
      }
    }

    /// <summary>
    /// Returns false, ignores any set value
    /// </summary>
    public override bool SourceColumnNullMapping
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets and sets the row version
    /// </summary>
    public override DataRowVersion SourceVersion
    {
      get
      {
        return _rowVersion;
      }
      set
      {
        _rowVersion = value;
      }
    }

    /// <summary>
    /// Gets and sets the parameter value.  If no datatype was specified, the datatype will assume the type from the value given.
    /// </summary>
    public override object Value
    {
      get
      {
        return _objValue;
      }
      set
      {
        _objValue = value;
        if (_dbType == -1 && _objValue != null && _objValue != DBNull.Value) // If the DbType has never been assigned, try to glean one from the value's datatype 
          _dbType = (int)SQLiteConvert.TypeToDbType(_objValue.GetType());
      }
    }    
  }
}
