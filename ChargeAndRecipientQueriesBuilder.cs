using System;
using System.Collections.Generic;

namespace TesteQueriesRecipeintAndCharge
{
    internal class ChargeAndRecipientQueriesBuilder
    {
        public string GetChargeRecipientWithSplitQuery(string[] ids)
        {
            var parameters = CreateParameters(ids);

            var query = @"SELECT c.[ExternalId] as 'ChargeId', r.ExternalId as 'RecipientId', c.PGID as 'PgId'
                FROM [Charge] C
                INNER JOIN [Transaction] t ON t.Id = C.LastTransactionId
                LEFT JOIN [Split] s ON s.TransactionId = T.Id
                LEFT JOIN [Recipient] r ON r.Id = S.RecipientId
                WHERE c.pgid IN(" + string.Join(",", parameters) + ") AND c.CreatedAt > @CreatedAt AND c.AccountId = @AccountId";

            return query;
        }

        public string GetChargeRecipientWithSplitQueryWithoutParameters(string[] ids)
        {
            var query = @"SELECT c.[ExternalId] as 'ChargeId', r.ExternalId as 'RecipientId', c.PGID as 'PgId'
                FROM [Charge] C
                INNER JOIN [Transaction] t ON t.Id = C.LastTransactionId
                LEFT JOIN [Split] s ON s.TransactionId = T.Id
                LEFT JOIN [Recipient] r ON r.Id = S.RecipientId
                WHERE c.pgid IN(" + string.Join(",", ids) + ") AND c.CreatedAt > @CreatedAt AND c.AccountId = @AccountId";

            return query;
        }

        public string GetChargeRecipientWithSplitQueryUsingDapper(string[] ids)
        {
            var parameters = CreateParameters(ids);

            var query = @"SELECT c.[ExternalId] as 'ChargeId', r.ExternalId as 'RecipientId', c.PGID as 'PgId'
                FROM [Charge] C
                INNER JOIN [Transaction] t ON t.Id = C.LastTransactionId
                LEFT JOIN [Split] s ON s.TransactionId = T.Id
                LEFT JOIN [Recipient] r ON r.Id = S.RecipientId
                WHERE c.pgid IN(" + string.Join(",", parameters) + ") AND c.CreatedAt > @CreatedAt AND c.AccountId = @AccountId";

            return query;
        }

        public string GetChargeRecipientWithSplitQueryUsingDapperWithoutParameters(string[] ids)
        {
            var query = @"SELECT c.[ExternalId] as 'ChargeId', r.ExternalId as 'RecipientId', c.PGID as 'PgId'
                FROM [Charge] C
                INNER JOIN [Transaction] t ON t.Id = C.LastTransactionId
                LEFT JOIN [Split] s ON s.TransactionId = T.Id
                LEFT JOIN [Recipient] r ON r.Id = S.RecipientId
                WHERE c.pgid IN(" + string.Join(",", ids) + ") AND c.CreatedAt > @CreatedAt AND c.AccountId = @AccountId";

            return query;
        }

        public string GetChargeRecipientWithOpenJson(string[] ids)
        {
            return @"SELECT c.[ExternalId] as 'ChargeId', r.ExternalId as 'RecipientId', c.PGID as 'PgId'
            FROM [Charge] C
            INNER JOIN [Transaction] t ON t.Id = C.LastTransactionId
            LEFT JOIN [Split] s ON s.TransactionId = T.Id
            LEFT JOIN [Recipient] r ON r.Id = S.RecipientId
            WHERE c.PGID IN (SELECT value FROM OPENJSON('[" +  string.Join(",", ids) + "]')) AND c.CreatedAt > @CreatedAt AND c.AccountId = @AccountId";
        }

        public string GetQueryWithOpenJson()
        {
            return @"SELECT Distinct c.[ExternalId] as ChargeId, r.ExternalId as RecipientId, c.PGID as PgId
            FROM [Charge] C
            INNER JOIN [Transaction] t ON t.Id = C.LastTransactionId
            LEFT JOIN [Split] s ON s.TransactionId = T.Id
            LEFT JOIN [Recipient] r ON r.Id = S.RecipientId
            WHERE c.PGID IN (SELECT value FROM OPENJSON(@Ids) with([value] varchar(36) '$')) AND c.CreatedAt > @CreatedAt AND c.AccountId = @AccountId";
        }

        public string GetQueryWithOpenJson2()
        {
            return @"SELECT Distinct r.ExternalId as RecipientId, gr.PGID as PgId
            FROM [GatewayRecipient] gr
            INNER JOIN [Recipient] R on R.Id = gr.RecipientId
            WHERE gr.PGID IN(SELECT value FROM OPENJSON(@Ids) with([value] varchar(38) '$')) AND r.AccountId = @AccountId";
        }

        private IEnumerable<string> CreateParameters(string[] ids)
        {
            var parameters = new List<string>();
            for (int i = 0; i < ids.Length; i++)
            {
                string paramName = "@pgid" + i;
                parameters.Add(paramName);
            }

            return parameters;
        }
    }
}
