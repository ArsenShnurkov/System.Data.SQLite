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
  /// SQLite implementation of DbCommandBuilder.
  /// </summary>
  public sealed class SQLiteCommandBuilder : DbCommandBuilder
  {
    private SQLiteRowUpdatingEventHandler _handler;

    /// <summary>
    /// Default constructor
    /// </summary>
    public SQLiteCommandBuilder()
    {
    }

    /// <summary>
    /// Initializes the command builder and associates it with the specified data adapter.
    /// </summary>
    /// <param name="adp"></param>
    public SQLiteCommandBuilder(SQLiteDataAdapter adp)
    {
      DataAdapter = adp;
    }

    /// <summary>
    /// Not implemented, this function does nothing.
    /// </summary>
    /// <param name="parameter">The parameter to use in applying custom behaviors to a row</param>
    /// <param name="row">The row to apply the parameter to</param>
    /// <param name="statementType">The type of statement</param>
    /// <param name="whereClause">Whether the application of the parameter is part of a WHERE clause</param>
    protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
    {
      SQLiteParameter param = (SQLiteParameter)parameter;
      param.DbType = (DbType)row[SchemaTableColumn.ProviderType];
    }

    /// <overloads>
    /// Not implemented.  Throws a NotImplementedException() if called.
    /// </overloads>
    /// <summary>
    /// Not implemented.
    /// </summary>
    /// <param name="parameterName">The name of the parameter</param>
    /// <returns>Error</returns>
    protected override string GetParameterName(string parameterName)
    {
      return String.Format(System.Globalization.CultureInfo.InvariantCulture, "${0}", parameterName);
    }

    /// <summary>
    /// Not implemented.
    /// </summary>
    /// <param name="parameterOrdinal">The i of the parameter</param>
    /// <returns>Error</returns>
    protected override string GetParameterName(int parameterOrdinal)
    {
      return String.Format(System.Globalization.CultureInfo.InvariantCulture, "$param{0}", parameterOrdinal);
    }

    /// <summary>
    /// Returns a placeholder character for the specified parameter i.
    /// </summary>
    /// <param name="parameterOrdinal">The index of the parameter to provide a placeholder for</param>
    /// <returns>Returns a "?" character, used for all placeholders.</returns>
    protected override string GetParameterPlaceholder(int parameterOrdinal)
    {
      return GetParameterName(parameterOrdinal);
    }

    /// <summary>
    /// Not implemented.
    /// </summary>
    /// <param name="adapter">A data adapter to receive events on.</param>
    protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
    {
      SQLiteDataAdapter adp = (SQLiteDataAdapter)adapter;

      _handler = new SQLiteRowUpdatingEventHandler(RowUpdatingEventHandler);
      adp.RowUpdating += _handler;
    }

    private void RowUpdatingEventHandler(object sender, RowUpdatingEventArgs e)
    {
      base.RowUpdatingHandler(e);
    }

  }
}
