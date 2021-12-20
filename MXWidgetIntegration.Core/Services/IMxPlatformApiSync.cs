using MXWidgetIntegration.Core.Client;
using MXWidgetIntegration.Core.Model;
using System;

namespace MXWidgetIntegration.Core.Services
{
    public interface IMxPlatformApiSync : IApiAccessor
    {
        #region Synchronous Operations
        MemberResponseBody AggregateMember(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> AggregateMemberWithHttpInfo(string memberGuid, string userGuid);
        MemberResponseBody CheckBalances(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> CheckBalancesWithHttpInfo(string memberGuid, string userGuid);
        CategoryResponseBody CreateCategory(string userGuid, CategoryCreateRequestBody categoryCreateRequestBody);

        ApiResponse<CategoryResponseBody> CreateCategoryWithHttpInfo(string userGuid, CategoryCreateRequestBody categoryCreateRequestBody);
        AccountResponseBody CreateManagedAccount(string userGuid, string memberGuid, ManagedAccountCreateRequestBody managedAccountCreateRequestBody);

        ApiResponse<AccountResponseBody> CreateManagedAccountWithHttpInfo(string userGuid, string memberGuid, ManagedAccountCreateRequestBody managedAccountCreateRequestBody);
        MemberResponseBody CreateManagedMember(string userGuid, ManagedMemberCreateRequestBody managedMemberCreateRequestBody);

        ApiResponse<MemberResponseBody> CreateManagedMemberWithHttpInfo(string userGuid, ManagedMemberCreateRequestBody managedMemberCreateRequestBody);
        TransactionResponseBody CreateManagedTransaction(string userGuid, string memberGuid, ManagedTransactionCreateRequestBody managedTransactionCreateRequestBody);

        ApiResponse<TransactionResponseBody> CreateManagedTransactionWithHttpInfo(string userGuid, string memberGuid, ManagedTransactionCreateRequestBody managedTransactionCreateRequestBody);
        MemberResponseBody CreateMember(string userGuid, MemberCreateRequestBody memberCreateRequestBody);

        ApiResponse<MemberResponseBody> CreateMemberWithHttpInfo(string userGuid, MemberCreateRequestBody memberCreateRequestBody);
        TagResponseBody CreateTag(string userGuid, TagCreateRequestBody tagCreateRequestBody);

        ApiResponse<TagResponseBody> CreateTagWithHttpInfo(string userGuid, TagCreateRequestBody tagCreateRequestBody);
        TaggingResponseBody CreateTagging(string userGuid, TaggingCreateRequestBody taggingCreateRequestBody);

        ApiResponse<TaggingResponseBody> CreateTaggingWithHttpInfo(string userGuid, TaggingCreateRequestBody taggingCreateRequestBody);
        TransactionRuleResponseBody CreateTransactionRule(string userGuid, TransactionRuleCreateRequestBody transactionRuleCreateRequestBody);

        ApiResponse<TransactionRuleResponseBody> CreateTransactionRuleWithHttpInfo(string userGuid, TransactionRuleCreateRequestBody transactionRuleCreateRequestBody);
        UserResponseBody CreateUser(UserCreateRequestBody userCreateRequestBody);

        ApiResponse<UserResponseBody> CreateUserWithHttpInfo(UserCreateRequestBody userCreateRequestBody);
        void DeleteCategory(string categoryGuid, string userGuid);

        ApiResponse<Object> DeleteCategoryWithHttpInfo(string categoryGuid, string userGuid);
        void DeleteManagedAccount(string memberGuid, string userGuid, string accountGuid);

        ApiResponse<Object> DeleteManagedAccountWithHttpInfo(string memberGuid, string userGuid, string accountGuid);
        void DeleteManagedMember(string memberGuid, string userGuid);

        ApiResponse<Object> DeleteManagedMemberWithHttpInfo(string memberGuid, string userGuid);
        void DeleteManagedTransaction(string memberGuid, string userGuid, string transactionGuid);

        ApiResponse<Object> DeleteManagedTransactionWithHttpInfo(string memberGuid, string userGuid, string transactionGuid);
        void DeleteMember(string memberGuid, string userGuid);

        ApiResponse<Object> DeleteMemberWithHttpInfo(string memberGuid, string userGuid);
        void DeleteTag(string tagGuid, string userGuid);

        ApiResponse<Object> DeleteTagWithHttpInfo(string tagGuid, string userGuid);
        void DeleteTagging(string taggingGuid, string userGuid);

        ApiResponse<Object> DeleteTaggingWithHttpInfo(string taggingGuid, string userGuid);
        void DeleteTransactionRule(string transactionRuleGuid, string userGuid);

        ApiResponse<Object> DeleteTransactionRuleWithHttpInfo(string transactionRuleGuid, string userGuid);
        void DeleteUser(string userGuid);

        ApiResponse<Object> DeleteUserWithHttpInfo(string userGuid);
        System.IO.Stream DownloadStatementPDF(string memberGuid, string statementGuid, string userGuid);

        ApiResponse<System.IO.Stream> DownloadStatementPDFWithHttpInfo(string memberGuid, string statementGuid, string userGuid);
        EnhanceTransactionsResponseBody EnhanceTransactions(EnhanceTransactionsRequestBody enhanceTransactionsRequestBody);

        ApiResponse<EnhanceTransactionsResponseBody> EnhanceTransactionsWithHttpInfo(EnhanceTransactionsRequestBody enhanceTransactionsRequestBody);
        MemberResponseBody ExtendHistory(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> ExtendHistoryWithHttpInfo(string memberGuid, string userGuid);
        MemberResponseBody FetchStatements(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> FetchStatementsWithHttpInfo(string memberGuid, string userGuid);
        MemberResponseBody IdentifyMember(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> IdentifyMemberWithHttpInfo(string memberGuid, string userGuid);
        AccountNumbersResponseBody ListAccountNumbersByAccount(string accountGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<AccountNumbersResponseBody> ListAccountNumbersByAccountWithHttpInfo(string accountGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        AccountNumbersResponseBody ListAccountNumbersByMember(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<AccountNumbersResponseBody> ListAccountNumbersByMemberWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        AccountOwnersResponseBody ListAccountOwnersByMember(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<AccountOwnersResponseBody> ListAccountOwnersByMemberWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        CategoriesResponseBody ListCategories(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<CategoriesResponseBody> ListCategoriesWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        CategoriesResponseBody ListDefaultCategories(int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<CategoriesResponseBody> ListDefaultCategoriesWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?));
        CategoriesResponseBody ListDefaultCategoriesByUser(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<CategoriesResponseBody> ListDefaultCategoriesByUserWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        InstitutionsResponseBody ListFavoriteInstitutions(int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<InstitutionsResponseBody> ListFavoriteInstitutionsWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?));
        HoldingsResponseBody ListHoldings(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));

        ApiResponse<HoldingsResponseBody> ListHoldingsWithHttpInfo(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));
        HoldingsResponseBody ListHoldingsByMember(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));

        ApiResponse<HoldingsResponseBody> ListHoldingsByMemberWithHttpInfo(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));
        CredentialsResponseBody ListInstitutionCredentials(string institutionCode, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<CredentialsResponseBody> ListInstitutionCredentialsWithHttpInfo(string institutionCode, int? page = default(int?), int? recordsPerPage = default(int?));
        InstitutionsResponseBody ListInstitutions(string name = default(string), int? page = default(int?), int? recordsPerPage = default(int?), bool? supportsAccountIdentification = default(bool?), bool? supportsAccountStatement = default(bool?), bool? supportsAccountVerification = default(bool?), bool? supportsTransactionHistory = default(bool?));

        ApiResponse<InstitutionsResponseBody> ListInstitutionsWithHttpInfo(string name = default(string), int? page = default(int?), int? recordsPerPage = default(int?), bool? supportsAccountIdentification = default(bool?), bool? supportsAccountStatement = default(bool?), bool? supportsAccountVerification = default(bool?), bool? supportsTransactionHistory = default(bool?));
        AccountsResponseBody ListManagedAccounts(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<AccountsResponseBody> ListManagedAccountsWithHttpInfo(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        InstitutionsResponseBody ListManagedInstitutions(int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<InstitutionsResponseBody> ListManagedInstitutionsWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?));
        MembersResponseBody ListManagedMembers(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<MembersResponseBody> ListManagedMembersWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        TransactionsResponseBody ListManagedTransactions(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<TransactionsResponseBody> ListManagedTransactionsWithHttpInfo(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        ChallengesResponseBody ListMemberChallenges(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<ChallengesResponseBody> ListMemberChallengesWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        CredentialsResponseBody ListMemberCredentials(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<CredentialsResponseBody> ListMemberCredentialsWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        MembersResponseBody ListMembers(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<MembersResponseBody> ListMembersWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        MerchantsResponseBody ListMerchants(int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<MerchantsResponseBody> ListMerchantsWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?));
        StatementsResponseBody ListStatementsByMember(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<StatementsResponseBody> ListStatementsByMemberWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        TaggingsResponseBody ListTaggings(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<TaggingsResponseBody> ListTaggingsWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        TagsResponseBody ListTags(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<TagsResponseBody> ListTagsWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        TransactionRulesResponseBody ListTransactionRules(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<TransactionRulesResponseBody> ListTransactionRulesWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        TransactionsResponseBody ListTransactions(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));

        ApiResponse<TransactionsResponseBody> ListTransactionsWithHttpInfo(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));
        TransactionsResponseBody ListTransactionsByAccount(string accountGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));

        ApiResponse<TransactionsResponseBody> ListTransactionsByAccountWithHttpInfo(string accountGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));
        TransactionsResponseBody ListTransactionsByMember(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));

        ApiResponse<TransactionsResponseBody> ListTransactionsByMemberWithHttpInfo(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));
        TransactionsResponseBody ListTransactionsByTag(string tagGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));

        ApiResponse<TransactionsResponseBody> ListTransactionsByTagWithHttpInfo(string tagGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string));
        AccountsResponseBody ListUserAccounts(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<AccountsResponseBody> ListUserAccountsWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?));
        UsersResponseBody ListUsers(int? page = default(int?), int? recordsPerPage = default(int?));

        ApiResponse<UsersResponseBody> ListUsersWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?));
        AccountResponseBody ReadAccount(string accountGuid, string userGuid);

        ApiResponse<AccountResponseBody> ReadAccountWithHttpInfo(string accountGuid, string userGuid);
        CategoryResponseBody ReadCategory(string categoryGuid, string userGuid);

        ApiResponse<CategoryResponseBody> ReadCategoryWithHttpInfo(string categoryGuid, string userGuid);
        CategoryResponseBody ReadDefaultCategory(string categoryGuid, string userGuid);

        ApiResponse<CategoryResponseBody> ReadDefaultCategoryWithHttpInfo(string categoryGuid, string userGuid);
        HoldingResponseBody ReadHolding(string holdingGuid, string userGuid);

        ApiResponse<HoldingResponseBody> ReadHoldingWithHttpInfo(string holdingGuid, string userGuid);
        InstitutionResponseBody ReadInstitution(string institutionCode);

        ApiResponse<InstitutionResponseBody> ReadInstitutionWithHttpInfo(string institutionCode);
        AccountResponseBody ReadManagedAccount(string memberGuid, string userGuid, string accountGuid);

        ApiResponse<AccountResponseBody> ReadManagedAccountWithHttpInfo(string memberGuid, string userGuid, string accountGuid);
        MemberResponseBody ReadManagedMember(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> ReadManagedMemberWithHttpInfo(string memberGuid, string userGuid);
        TransactionResponseBody ReadManagedTransaction(string memberGuid, string userGuid, string transactionGuid);

        ApiResponse<TransactionResponseBody> ReadManagedTransactionWithHttpInfo(string memberGuid, string userGuid, string transactionGuid);
        MemberResponseBody ReadMember(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> ReadMemberWithHttpInfo(string memberGuid, string userGuid);
        MemberStatusResponseBody ReadMemberStatus(string memberGuid, string userGuid);

        ApiResponse<MemberStatusResponseBody> ReadMemberStatusWithHttpInfo(string memberGuid, string userGuid);
        MerchantResponseBody ReadMerchant(string merchantGuid);

        ApiResponse<MerchantResponseBody> ReadMerchantWithHttpInfo(string merchantGuid);
        MerchantLocationResponseBody ReadMerchantLocation(string merchantLocationGuid);

        ApiResponse<MerchantLocationResponseBody> ReadMerchantLocationWithHttpInfo(string merchantLocationGuid);
        StatementResponseBody ReadStatementByMember(string memberGuid, string statementGuid, string userGuid);

        ApiResponse<StatementResponseBody> ReadStatementByMemberWithHttpInfo(string memberGuid, string statementGuid, string userGuid);
        TagResponseBody ReadTag(string tagGuid, string userGuid);

        ApiResponse<TagResponseBody> ReadTagWithHttpInfo(string tagGuid, string userGuid);
        TaggingResponseBody ReadTagging(string taggingGuid, string userGuid);

        ApiResponse<TaggingResponseBody> ReadTaggingWithHttpInfo(string taggingGuid, string userGuid);
        TransactionResponseBody ReadTransaction(string transactionGuid, string userGuid);

        ApiResponse<TransactionResponseBody> ReadTransactionWithHttpInfo(string transactionGuid, string userGuid);
        TransactionRuleResponseBody ReadTransactionRule(string transactionRuleGuid, string userGuid);

        ApiResponse<TransactionRuleResponseBody> ReadTransactionRuleWithHttpInfo(string transactionRuleGuid, string userGuid);
        UserResponseBody ReadUser(string userGuid);

        ApiResponse<UserResponseBody> ReadUserWithHttpInfo(string userGuid);
        ConnectWidgetResponseBody RequestConnectWidgetURL(string userGuid, ConnectWidgetRequestBody connectWidgetRequestBody = default(ConnectWidgetRequestBody));

        ApiResponse<ConnectWidgetResponseBody> RequestConnectWidgetURLWithHttpInfo(string userGuid, ConnectWidgetRequestBody connectWidgetRequestBody = default(ConnectWidgetRequestBody));
        OAuthWindowResponseBody RequestOAuthWindowURI(string memberGuid, string userGuid, string referralSource = default(string), string uiMessageWebviewUrlScheme = default(string), bool? skipAggregation = default(bool?));

        ApiResponse<OAuthWindowResponseBody> RequestOAuthWindowURIWithHttpInfo(string memberGuid, string userGuid, string referralSource = default(string), string uiMessageWebviewUrlScheme = default(string), bool? skipAggregation = default(bool?));
        WidgetResponseBody RequestWidgetURL(string userGuid, WidgetRequestBody widgetRequestBody, string acceptLanguage = default(string));

        ApiResponse<WidgetResponseBody> RequestWidgetURLWithHttpInfo(string userGuid, WidgetRequestBody widgetRequestBody, string acceptLanguage = default(string));
        MemberResponseBody ResumeAggregation(string memberGuid, string userGuid, MemberResumeRequestBody memberResumeRequestBody);

        ApiResponse<MemberResponseBody> ResumeAggregationWithHttpInfo(string memberGuid, string userGuid, MemberResumeRequestBody memberResumeRequestBody);
        AccountResponseBody UpdateAccountByMember(string userGuid, string memberGuid, string accountGuid, AccountUpdateRequestBody accountUpdateRequestBody);

        ApiResponse<AccountResponseBody> UpdateAccountByMemberWithHttpInfo(string userGuid, string memberGuid, string accountGuid, AccountUpdateRequestBody accountUpdateRequestBody);
        CategoryResponseBody UpdateCategory(string categoryGuid, string userGuid, CategoryUpdateRequestBody categoryUpdateRequestBody);

        ApiResponse<CategoryResponseBody> UpdateCategoryWithHttpInfo(string categoryGuid, string userGuid, CategoryUpdateRequestBody categoryUpdateRequestBody);
        AccountResponseBody UpdateManagedAccount(string memberGuid, string userGuid, string accountGuid, ManagedAccountUpdateRequestBody managedAccountUpdateRequestBody);

        ApiResponse<AccountResponseBody> UpdateManagedAccountWithHttpInfo(string memberGuid, string userGuid, string accountGuid, ManagedAccountUpdateRequestBody managedAccountUpdateRequestBody);
        MemberResponseBody UpdateManagedMember(string memberGuid, string userGuid, ManagedMemberUpdateRequestBody managedMemberUpdateRequestBody);

        ApiResponse<MemberResponseBody> UpdateManagedMemberWithHttpInfo(string memberGuid, string userGuid, ManagedMemberUpdateRequestBody managedMemberUpdateRequestBody);
        TransactionResponseBody UpdateManagedTransaction(string memberGuid, string userGuid, string transactionGuid, ManagedTransactionUpdateRequestBody managedTransactionUpdateRequestBody);

        ApiResponse<TransactionResponseBody> UpdateManagedTransactionWithHttpInfo(string memberGuid, string userGuid, string transactionGuid, ManagedTransactionUpdateRequestBody managedTransactionUpdateRequestBody);
        MemberResponseBody UpdateMember(string memberGuid, string userGuid, MemberUpdateRequestBody memberUpdateRequestBody);

        ApiResponse<MemberResponseBody> UpdateMemberWithHttpInfo(string memberGuid, string userGuid, MemberUpdateRequestBody memberUpdateRequestBody);
        TagResponseBody UpdateTag(string tagGuid, string userGuid, TagUpdateRequestBody tagUpdateRequestBody);

        ApiResponse<TagResponseBody> UpdateTagWithHttpInfo(string tagGuid, string userGuid, TagUpdateRequestBody tagUpdateRequestBody);
        TaggingResponseBody UpdateTagging(string taggingGuid, string userGuid, TaggingUpdateRequestBody taggingUpdateRequestBody);

        ApiResponse<TaggingResponseBody> UpdateTaggingWithHttpInfo(string taggingGuid, string userGuid, TaggingUpdateRequestBody taggingUpdateRequestBody);
        TransactionResponseBody UpdateTransaction(string transactionGuid, string userGuid, TransactionUpdateRequestBody transactionUpdateRequestBody);

        ApiResponse<TransactionResponseBody> UpdateTransactionWithHttpInfo(string transactionGuid, string userGuid, TransactionUpdateRequestBody transactionUpdateRequestBody);
        TransactionRuleResponseBody UpdateTransactionRule(string transactionRuleGuid, string userGuid, TransactionRuleUpdateRequestBody transactionRuleUpdateRequestBody);

        ApiResponse<TransactionRuleResponseBody> UpdateTransactionRuleWithHttpInfo(string transactionRuleGuid, string userGuid, TransactionRuleUpdateRequestBody transactionRuleUpdateRequestBody);
        UserResponseBody UpdateUser(string userGuid, UserUpdateRequestBody userUpdateRequestBody);

        ApiResponse<UserResponseBody> UpdateUserWithHttpInfo(string userGuid, UserUpdateRequestBody userUpdateRequestBody);
        MemberResponseBody VerifyMember(string memberGuid, string userGuid);

        ApiResponse<MemberResponseBody> VerifyMemberWithHttpInfo(string memberGuid, string userGuid);
        #endregion Synchronous Operations
    }
}