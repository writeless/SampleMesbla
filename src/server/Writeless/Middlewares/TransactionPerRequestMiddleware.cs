using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Writeless.Middlewares
{
    //https://stackoverflow.com/questions/40611874/use-the-same-transaction-in-different-methods-with-entity-framework-core
    //public class TransactionPerRequestMiddleware
    //{
    //    private readonly RequestDelegate next_;

    //    public TransactionPerRequestMiddleware(RequestDelegate next)
    //    {
    //        next_ = next;
    //    }

    //    public async Task Invoke(HttpContext context, ApplicationDbContext dbContext)
    //    {
    //        var transaction = dbContext.Database.BeginTransaction(
    //            System.Data.IsolationLevel.ReadCommitted);

    //        await next_.Invoke(context);

    //        if (context.Response.StatusCode == 200)
    //        {
    //            transaction.Commit();
    //        }
    //        else
    //        {
    //            transaction.Rollback();
    //        }
    //    }
    //}
}
