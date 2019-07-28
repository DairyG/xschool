using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace XSchool.Repositories.Extensions
{
    public static class DbContextExtensions
    {
        public static RelationalDataReader ExcuteReader(this DatabaseFacade databaseFacade, string cmdText, params DbParameter[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();
            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade.GetService<IRawSqlCommandBuilder>().Build(cmdText, parameters);
                return rawSqlCommand.RelationalCommand.ExecuteReader(
                    databaseFacade.GetService<IRelationalConnection>(),
                    parameterValues: rawSqlCommand.ParameterValues);
            }
        }

        public static RelationalDataReader ExcuteReader(this DatabaseFacade databaseFacade, string cmdText, CommandType cmdType, params DbParameter[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();
            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade.GetService<IRawSqlCommandBuilder>().Build(cmdText, parameters);
                return rawSqlCommand.RelationalCommand.ExecuteReader(
                    databaseFacade.GetService<IRelationalConnection>(),
                    parameterValues: rawSqlCommand.ParameterValues);
            }

        }

        public static object ExecuteScalar(this DatabaseFacade databaseFacade, string cmdText, CommandType cmdType=CommandType.Text, params DbParameter[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();
            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade.GetService<IRawSqlCommandBuilder>().Build(cmdText, parameters);
                return rawSqlCommand.RelationalCommand.ExecuteScalar(
                    databaseFacade.GetService<IRelationalConnection>(),
                    parameterValues: rawSqlCommand.ParameterValues);
            }

        }

    }
}
