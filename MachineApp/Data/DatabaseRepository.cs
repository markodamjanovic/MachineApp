using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace MachineApp.Data
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public DatabaseRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<T> Get<T>(int id) where T: class
        {
            using (var connection = _databaseConnection.GetDbConnection())
            {
                return await connection.GetAsync<T>(id);
            }
        }
        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            using (var connection = _databaseConnection.GetDbConnection())
            {
                return await connection.GetAllAsync<T>();
            }
        }
        public async Task<int> Insert<T>(T model) where T: class
        {
            using (var connection = _databaseConnection.GetDbConnection())
            {
                return await connection.InsertAsync<T>(model);
            }
        }
        public async Task<T> Update<T>(T model) where T: class
        {
            using (var connection = _databaseConnection.GetDbConnection())
            {
                var result = await connection.UpdateAsync<T>(model);

                if(result)
                {
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<T> Delete<T>(T model) where T: class
        {
            using (var connection = _databaseConnection.GetDbConnection())
            {   
                var result = await connection.DeleteAsync(model);

                if(result)
                {
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<IEnumerable<T>> DeleteList<T>(IEnumerable<T> models) where T: class
        {
            using (var connection = _databaseConnection.GetDbConnection())
            {   
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        bool result = true;
                        foreach (T model in models)
                        {
                            result &= await connection.DeleteAsync(model);
                        }
                        
                        if(result)
                        {
                            transaction.Commit();
                            return models;
                        }
                        else
                        {   
                            transaction.Rollback();
                            return null;
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            }
        }
    }
}