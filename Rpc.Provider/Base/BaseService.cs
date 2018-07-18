using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Rpc.Provider.Base
{
    public class BaseService : IDisposable
    {
        private IDbConnection _db = null;
        internal IDbConnection db
        {
            get
            {
                if (_db == null || _db.State == ConnectionState.Closed)
                {
                    _db = DbConnFactory.GetDbConnection("dbcontext"); //new一个新的连接对象
                }
                return _db;
            }
        }

        public void Dispose()
        {
            if (_db != null)
            {
                if (_db.State != ConnectionState.Closed)
                {
                    _db.Dispose();
                }
                _db = null;
            }
        }

       
    }
}
