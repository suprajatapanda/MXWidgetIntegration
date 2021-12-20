﻿using MXWidgetIntegration.Core.Client;
using MXWidgetIntegration.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXWidgetIntegration.Core.Services
{
    public partial class MxPlatformApi : IMxPlatformApi
    {
        private ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MxPlatformApi"/> class.
        /// </summary>
        /// <returns></returns>
        public MxPlatformApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MxPlatformApi"/> class.
        /// </summary>
        /// <returns></returns>
        public MxPlatformApi(string basePath)
        {
            this.Configuration = Core.Client.Configuration.MergeConfigurations(
                GlobalConfiguration.Instance,
                new Configuration { BasePath = basePath }
            );
            this.Client = new ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Core.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MxPlatformApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public MxPlatformApi(Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Core.Client.Configuration.MergeConfigurations(
                GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Core.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MxPlatformApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public MxPlatformApi(ISynchronousClient client, IAsynchronousClient asyncClient, IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Core.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Aggregate member Calling this endpoint initiates an aggregation event for the member. This brings in the latest account and transaction data from the connected institution. If this data has recently been updated, MX may not initiate an aggregation event.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody AggregateMember(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = AggregateMemberWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Aggregate member Calling this endpoint initiates an aggregation event for the member. This brings in the latest account and transaction data from the connected institution. If this data has recently been updated, MX may not initiate an aggregation event.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> AggregateMemberWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->AggregateMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->AggregateMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/aggregate", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AggregateMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Aggregate member Calling this endpoint initiates an aggregation event for the member. This brings in the latest account and transaction data from the connected institution. If this data has recently been updated, MX may not initiate an aggregation event.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> AggregateMemberAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await AggregateMemberWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Aggregate member Calling this endpoint initiates an aggregation event for the member. This brings in the latest account and transaction data from the connected institution. If this data has recently been updated, MX may not initiate an aggregation event.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> AggregateMemberWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->AggregateMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->AggregateMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/aggregate", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AggregateMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Check balances This endpoint operates much like the aggregate member endpoint except that it gathers only account balance information; it does not gather any transaction data.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody CheckBalances(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = CheckBalancesWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Check balances This endpoint operates much like the aggregate member endpoint except that it gathers only account balance information; it does not gather any transaction data.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> CheckBalancesWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->CheckBalances");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CheckBalances");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/check_balance", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CheckBalances", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Check balances This endpoint operates much like the aggregate member endpoint except that it gathers only account balance information; it does not gather any transaction data.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> CheckBalancesAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await CheckBalancesWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Check balances This endpoint operates much like the aggregate member endpoint except that it gathers only account balance information; it does not gather any transaction data.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> CheckBalancesWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->CheckBalances");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CheckBalances");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/check_balance", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CheckBalances", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create category Use this endpoint to create a new custom category for a specific &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryCreateRequestBody">Custom category object to be created</param>
        /// <returns>CategoryResponseBody</returns>
        public CategoryResponseBody CreateCategory(string userGuid, CategoryCreateRequestBody categoryCreateRequestBody)
        {
            ApiResponse<CategoryResponseBody> localVarResponse = CreateCategoryWithHttpInfo(userGuid, categoryCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create category Use this endpoint to create a new custom category for a specific &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryCreateRequestBody">Custom category object to be created</param>
        /// <returns>ApiResponse of CategoryResponseBody</returns>
        public ApiResponse<CategoryResponseBody> CreateCategoryWithHttpInfo(string userGuid, CategoryCreateRequestBody categoryCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateCategory");

            // verify the required parameter 'categoryCreateRequestBody' is set
            if (categoryCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'categoryCreateRequestBody' when calling MxPlatformApi->CreateCategory");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = categoryCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CategoryResponseBody>("/users/{user_guid}/categories", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create category Use this endpoint to create a new custom category for a specific &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryCreateRequestBody">Custom category object to be created</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CategoryResponseBody</returns>
        public async System.Threading.Tasks.Task<CategoryResponseBody> CreateCategoryAsync(string userGuid, CategoryCreateRequestBody categoryCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CategoryResponseBody> localVarResponse = await CreateCategoryWithHttpInfoAsync(userGuid, categoryCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create category Use this endpoint to create a new custom category for a specific &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryCreateRequestBody">Custom category object to be created</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CategoryResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CategoryResponseBody>> CreateCategoryWithHttpInfoAsync(string userGuid, CategoryCreateRequestBody categoryCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateCategory");

            // verify the required parameter 'categoryCreateRequestBody' is set
            if (categoryCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'categoryCreateRequestBody' when calling MxPlatformApi->CreateCategory");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = categoryCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<CategoryResponseBody>("/users/{user_guid}/categories", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create managed account Use this endpoint to create a partner-managed account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedAccountCreateRequestBody">Managed account to be created.</param>
        /// <returns>AccountResponseBody</returns>
        public AccountResponseBody CreateManagedAccount(string userGuid, string memberGuid, ManagedAccountCreateRequestBody managedAccountCreateRequestBody)
        {
            ApiResponse<AccountResponseBody> localVarResponse = CreateManagedAccountWithHttpInfo(userGuid, memberGuid, managedAccountCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create managed account Use this endpoint to create a partner-managed account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedAccountCreateRequestBody">Managed account to be created.</param>
        /// <returns>ApiResponse of AccountResponseBody</returns>
        public ApiResponse<AccountResponseBody> CreateManagedAccountWithHttpInfo(string userGuid, string memberGuid, ManagedAccountCreateRequestBody managedAccountCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateManagedAccount");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->CreateManagedAccount");

            // verify the required parameter 'managedAccountCreateRequestBody' is set
            if (managedAccountCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedAccountCreateRequestBody' when calling MxPlatformApi->CreateManagedAccount");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.Data = managedAccountCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<AccountResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create managed account Use this endpoint to create a partner-managed account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedAccountCreateRequestBody">Managed account to be created.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountResponseBody> CreateManagedAccountAsync(string userGuid, string memberGuid, ManagedAccountCreateRequestBody managedAccountCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountResponseBody> localVarResponse = await CreateManagedAccountWithHttpInfoAsync(userGuid, memberGuid, managedAccountCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create managed account Use this endpoint to create a partner-managed account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedAccountCreateRequestBody">Managed account to be created.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountResponseBody>> CreateManagedAccountWithHttpInfoAsync(string userGuid, string memberGuid, ManagedAccountCreateRequestBody managedAccountCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateManagedAccount");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->CreateManagedAccount");

            // verify the required parameter 'managedAccountCreateRequestBody' is set
            if (managedAccountCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedAccountCreateRequestBody' when calling MxPlatformApi->CreateManagedAccount");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.Data = managedAccountCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<AccountResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create managed member Use this endpoint to create a new partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberCreateRequestBody">Managed member to be created.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody CreateManagedMember(string userGuid, ManagedMemberCreateRequestBody managedMemberCreateRequestBody)
        {
            ApiResponse<MemberResponseBody> localVarResponse = CreateManagedMemberWithHttpInfo(userGuid, managedMemberCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create managed member Use this endpoint to create a new partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberCreateRequestBody">Managed member to be created.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> CreateManagedMemberWithHttpInfo(string userGuid, ManagedMemberCreateRequestBody managedMemberCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateManagedMember");

            // verify the required parameter 'managedMemberCreateRequestBody' is set
            if (managedMemberCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedMemberCreateRequestBody' when calling MxPlatformApi->CreateManagedMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = managedMemberCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/managed_members", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create managed member Use this endpoint to create a new partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberCreateRequestBody">Managed member to be created.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> CreateManagedMemberAsync(string userGuid, ManagedMemberCreateRequestBody managedMemberCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await CreateManagedMemberWithHttpInfoAsync(userGuid, managedMemberCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create managed member Use this endpoint to create a new partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberCreateRequestBody">Managed member to be created.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> CreateManagedMemberWithHttpInfoAsync(string userGuid, ManagedMemberCreateRequestBody managedMemberCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateManagedMember");

            // verify the required parameter 'managedMemberCreateRequestBody' is set
            if (managedMemberCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedMemberCreateRequestBody' when calling MxPlatformApi->CreateManagedMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = managedMemberCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/managed_members", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create managed transaction Use this endpoint to create a new partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedTransactionCreateRequestBody">Managed transaction to be created.</param>
        /// <returns>TransactionResponseBody</returns>
        public TransactionResponseBody CreateManagedTransaction(string userGuid, string memberGuid, ManagedTransactionCreateRequestBody managedTransactionCreateRequestBody)
        {
            ApiResponse<TransactionResponseBody> localVarResponse = CreateManagedTransactionWithHttpInfo(userGuid, memberGuid, managedTransactionCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create managed transaction Use this endpoint to create a new partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedTransactionCreateRequestBody">Managed transaction to be created.</param>
        /// <returns>ApiResponse of TransactionResponseBody</returns>
        public ApiResponse<TransactionResponseBody> CreateManagedTransactionWithHttpInfo(string userGuid, string memberGuid, ManagedTransactionCreateRequestBody managedTransactionCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateManagedTransaction");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->CreateManagedTransaction");

            // verify the required parameter 'managedTransactionCreateRequestBody' is set
            if (managedTransactionCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedTransactionCreateRequestBody' when calling MxPlatformApi->CreateManagedTransaction");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.Data = managedTransactionCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<TransactionResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create managed transaction Use this endpoint to create a new partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedTransactionCreateRequestBody">Managed transaction to be created.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionResponseBody> CreateManagedTransactionAsync(string userGuid, string memberGuid, ManagedTransactionCreateRequestBody managedTransactionCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionResponseBody> localVarResponse = await CreateManagedTransactionWithHttpInfoAsync(userGuid, memberGuid, managedTransactionCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create managed transaction Use this endpoint to create a new partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="managedTransactionCreateRequestBody">Managed transaction to be created.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionResponseBody>> CreateManagedTransactionWithHttpInfoAsync(string userGuid, string memberGuid, ManagedTransactionCreateRequestBody managedTransactionCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateManagedTransaction");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->CreateManagedTransaction");

            // verify the required parameter 'managedTransactionCreateRequestBody' is set
            if (managedTransactionCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedTransactionCreateRequestBody' when calling MxPlatformApi->CreateManagedTransaction");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.Data = managedTransactionCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<TransactionResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create member This endpoint allows you to create a new member. Members are created with the required parameters credentials and institution_code, and the optional parameters id and metadata. When creating a member, youll need to include the correct type of credential required by the financial institution and provided by the user. You can find out which credential type is required with the &#x60;/institutions/{institution_code}/credentials&#x60; endpoint. If successful, the MX Platform API will respond with the newly-created member object. Once you successfully create a member, MX will immediately validate the provided credentials and attempt to aggregate data for accounts and transactions.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberCreateRequestBody">Member object to be created with optional parameters (id and metadata) and required parameters (credentials and institution_code)</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody CreateMember(string userGuid, MemberCreateRequestBody memberCreateRequestBody)
        {
            ApiResponse<MemberResponseBody> localVarResponse = CreateMemberWithHttpInfo(userGuid, memberCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create member This endpoint allows you to create a new member. Members are created with the required parameters credentials and institution_code, and the optional parameters id and metadata. When creating a member, youll need to include the correct type of credential required by the financial institution and provided by the user. You can find out which credential type is required with the &#x60;/institutions/{institution_code}/credentials&#x60; endpoint. If successful, the MX Platform API will respond with the newly-created member object. Once you successfully create a member, MX will immediately validate the provided credentials and attempt to aggregate data for accounts and transactions.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberCreateRequestBody">Member object to be created with optional parameters (id and metadata) and required parameters (credentials and institution_code)</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> CreateMemberWithHttpInfo(string userGuid, MemberCreateRequestBody memberCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateMember");

            // verify the required parameter 'memberCreateRequestBody' is set
            if (memberCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'memberCreateRequestBody' when calling MxPlatformApi->CreateMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = memberCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/members", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create member This endpoint allows you to create a new member. Members are created with the required parameters credentials and institution_code, and the optional parameters id and metadata. When creating a member, youll need to include the correct type of credential required by the financial institution and provided by the user. You can find out which credential type is required with the &#x60;/institutions/{institution_code}/credentials&#x60; endpoint. If successful, the MX Platform API will respond with the newly-created member object. Once you successfully create a member, MX will immediately validate the provided credentials and attempt to aggregate data for accounts and transactions.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberCreateRequestBody">Member object to be created with optional parameters (id and metadata) and required parameters (credentials and institution_code)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> CreateMemberAsync(string userGuid, MemberCreateRequestBody memberCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await CreateMemberWithHttpInfoAsync(userGuid, memberCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create member This endpoint allows you to create a new member. Members are created with the required parameters credentials and institution_code, and the optional parameters id and metadata. When creating a member, youll need to include the correct type of credential required by the financial institution and provided by the user. You can find out which credential type is required with the &#x60;/institutions/{institution_code}/credentials&#x60; endpoint. If successful, the MX Platform API will respond with the newly-created member object. Once you successfully create a member, MX will immediately validate the provided credentials and attempt to aggregate data for accounts and transactions.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberCreateRequestBody">Member object to be created with optional parameters (id and metadata) and required parameters (credentials and institution_code)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> CreateMemberWithHttpInfoAsync(string userGuid, MemberCreateRequestBody memberCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateMember");

            // verify the required parameter 'memberCreateRequestBody' is set
            if (memberCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'memberCreateRequestBody' when calling MxPlatformApi->CreateMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = memberCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/members", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create tag Use this endpoint to create a new custom tag.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagCreateRequestBody">Tag object to be created with required parameters (tag_guid)</param>
        /// <returns>TagResponseBody</returns>
        public TagResponseBody CreateTag(string userGuid, TagCreateRequestBody tagCreateRequestBody)
        {
            ApiResponse<TagResponseBody> localVarResponse = CreateTagWithHttpInfo(userGuid, tagCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create tag Use this endpoint to create a new custom tag.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagCreateRequestBody">Tag object to be created with required parameters (tag_guid)</param>
        /// <returns>ApiResponse of TagResponseBody</returns>
        public ApiResponse<TagResponseBody> CreateTagWithHttpInfo(string userGuid, TagCreateRequestBody tagCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateTag");

            // verify the required parameter 'tagCreateRequestBody' is set
            if (tagCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'tagCreateRequestBody' when calling MxPlatformApi->CreateTag");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = tagCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<TagResponseBody>("/users/{user_guid}/tags", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create tag Use this endpoint to create a new custom tag.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagCreateRequestBody">Tag object to be created with required parameters (tag_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TagResponseBody</returns>
        public async System.Threading.Tasks.Task<TagResponseBody> CreateTagAsync(string userGuid, TagCreateRequestBody tagCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TagResponseBody> localVarResponse = await CreateTagWithHttpInfoAsync(userGuid, tagCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create tag Use this endpoint to create a new custom tag.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagCreateRequestBody">Tag object to be created with required parameters (tag_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TagResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TagResponseBody>> CreateTagWithHttpInfoAsync(string userGuid, TagCreateRequestBody tagCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateTag");

            // verify the required parameter 'tagCreateRequestBody' is set
            if (tagCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'tagCreateRequestBody' when calling MxPlatformApi->CreateTag");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = tagCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<TagResponseBody>("/users/{user_guid}/tags", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create tagging Use this endpoint to create a new association between a tag and a particular transaction, according to their unique GUIDs.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingCreateRequestBody">Tagging object to be created with required parameters (tag_guid and transaction_guid)</param>
        /// <returns>TaggingResponseBody</returns>
        public TaggingResponseBody CreateTagging(string userGuid, TaggingCreateRequestBody taggingCreateRequestBody)
        {
            ApiResponse<TaggingResponseBody> localVarResponse = CreateTaggingWithHttpInfo(userGuid, taggingCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create tagging Use this endpoint to create a new association between a tag and a particular transaction, according to their unique GUIDs.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingCreateRequestBody">Tagging object to be created with required parameters (tag_guid and transaction_guid)</param>
        /// <returns>ApiResponse of TaggingResponseBody</returns>
        public ApiResponse<TaggingResponseBody> CreateTaggingWithHttpInfo(string userGuid, TaggingCreateRequestBody taggingCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateTagging");

            // verify the required parameter 'taggingCreateRequestBody' is set
            if (taggingCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'taggingCreateRequestBody' when calling MxPlatformApi->CreateTagging");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = taggingCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<TaggingResponseBody>("/users/{user_guid}/taggings", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create tagging Use this endpoint to create a new association between a tag and a particular transaction, according to their unique GUIDs.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingCreateRequestBody">Tagging object to be created with required parameters (tag_guid and transaction_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TaggingResponseBody</returns>
        public async System.Threading.Tasks.Task<TaggingResponseBody> CreateTaggingAsync(string userGuid, TaggingCreateRequestBody taggingCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TaggingResponseBody> localVarResponse = await CreateTaggingWithHttpInfoAsync(userGuid, taggingCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create tagging Use this endpoint to create a new association between a tag and a particular transaction, according to their unique GUIDs.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingCreateRequestBody">Tagging object to be created with required parameters (tag_guid and transaction_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TaggingResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TaggingResponseBody>> CreateTaggingWithHttpInfoAsync(string userGuid, TaggingCreateRequestBody taggingCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateTagging");

            // verify the required parameter 'taggingCreateRequestBody' is set
            if (taggingCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'taggingCreateRequestBody' when calling MxPlatformApi->CreateTagging");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = taggingCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<TaggingResponseBody>("/users/{user_guid}/taggings", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create transaction rule Use this endpoint to create a new transaction rule. The newly-created &#x60;transaction_rule&#x60; object will be returned if successful.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleCreateRequestBody">TransactionRule object to be created with optional parameters (description) and required parameters (category_guid and match_description)</param>
        /// <returns>TransactionRuleResponseBody</returns>
        public TransactionRuleResponseBody CreateTransactionRule(string userGuid, TransactionRuleCreateRequestBody transactionRuleCreateRequestBody)
        {
            ApiResponse<TransactionRuleResponseBody> localVarResponse = CreateTransactionRuleWithHttpInfo(userGuid, transactionRuleCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create transaction rule Use this endpoint to create a new transaction rule. The newly-created &#x60;transaction_rule&#x60; object will be returned if successful.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleCreateRequestBody">TransactionRule object to be created with optional parameters (description) and required parameters (category_guid and match_description)</param>
        /// <returns>ApiResponse of TransactionRuleResponseBody</returns>
        public ApiResponse<TransactionRuleResponseBody> CreateTransactionRuleWithHttpInfo(string userGuid, TransactionRuleCreateRequestBody transactionRuleCreateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateTransactionRule");

            // verify the required parameter 'transactionRuleCreateRequestBody' is set
            if (transactionRuleCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleCreateRequestBody' when calling MxPlatformApi->CreateTransactionRule");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = transactionRuleCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<TransactionRuleResponseBody>("/users/{user_guid}/transaction_rules", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create transaction rule Use this endpoint to create a new transaction rule. The newly-created &#x60;transaction_rule&#x60; object will be returned if successful.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleCreateRequestBody">TransactionRule object to be created with optional parameters (description) and required parameters (category_guid and match_description)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionRuleResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionRuleResponseBody> CreateTransactionRuleAsync(string userGuid, TransactionRuleCreateRequestBody transactionRuleCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionRuleResponseBody> localVarResponse = await CreateTransactionRuleWithHttpInfoAsync(userGuid, transactionRuleCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create transaction rule Use this endpoint to create a new transaction rule. The newly-created &#x60;transaction_rule&#x60; object will be returned if successful.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleCreateRequestBody">TransactionRule object to be created with optional parameters (description) and required parameters (category_guid and match_description)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionRuleResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionRuleResponseBody>> CreateTransactionRuleWithHttpInfoAsync(string userGuid, TransactionRuleCreateRequestBody transactionRuleCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->CreateTransactionRule");

            // verify the required parameter 'transactionRuleCreateRequestBody' is set
            if (transactionRuleCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleCreateRequestBody' when calling MxPlatformApi->CreateTransactionRule");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = transactionRuleCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<TransactionRuleResponseBody>("/users/{user_guid}/transaction_rules", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create user Call this endpoint to create a new user. The MX Platform API will respond with the newly-created user object if successful. This endpoint accepts several parameters - id, metadata, and is_disabled. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userCreateRequestBody">User object to be created. (None of these parameters are required, but the user object cannot be empty)</param>
        /// <returns>UserResponseBody</returns>
        public UserResponseBody CreateUser(UserCreateRequestBody userCreateRequestBody)
        {
            ApiResponse<UserResponseBody> localVarResponse = CreateUserWithHttpInfo(userCreateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create user Call this endpoint to create a new user. The MX Platform API will respond with the newly-created user object if successful. This endpoint accepts several parameters - id, metadata, and is_disabled. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userCreateRequestBody">User object to be created. (None of these parameters are required, but the user object cannot be empty)</param>
        /// <returns>ApiResponse of UserResponseBody</returns>
        public ApiResponse<UserResponseBody> CreateUserWithHttpInfo(UserCreateRequestBody userCreateRequestBody)
        {
            // verify the required parameter 'userCreateRequestBody' is set
            if (userCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'userCreateRequestBody' when calling MxPlatformApi->CreateUser");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = userCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<UserResponseBody>("/users", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create user Call this endpoint to create a new user. The MX Platform API will respond with the newly-created user object if successful. This endpoint accepts several parameters - id, metadata, and is_disabled. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userCreateRequestBody">User object to be created. (None of these parameters are required, but the user object cannot be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of UserResponseBody</returns>
        public async System.Threading.Tasks.Task<UserResponseBody> CreateUserAsync(UserCreateRequestBody userCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<UserResponseBody> localVarResponse = await CreateUserWithHttpInfoAsync(userCreateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create user Call this endpoint to create a new user. The MX Platform API will respond with the newly-created user object if successful. This endpoint accepts several parameters - id, metadata, and is_disabled. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userCreateRequestBody">User object to be created. (None of these parameters are required, but the user object cannot be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (UserResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<UserResponseBody>> CreateUserWithHttpInfoAsync(UserCreateRequestBody userCreateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userCreateRequestBody' is set
            if (userCreateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'userCreateRequestBody' when calling MxPlatformApi->CreateUser");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = userCreateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<UserResponseBody>("/users", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete category Use this endpoint to delete a specific custom category according to its unique GUID. The API will respond with an empty object and a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns></returns>
        public void DeleteCategory(string categoryGuid, string userGuid)
        {
            DeleteCategoryWithHttpInfo(categoryGuid, userGuid);
        }

        /// <summary>
        /// Delete category Use this endpoint to delete a specific custom category according to its unique GUID. The API will respond with an empty object and a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteCategoryWithHttpInfo(string categoryGuid, string userGuid)
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->DeleteCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteCategory");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/categories/{category_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete category Use this endpoint to delete a specific custom category according to its unique GUID. The API will respond with an empty object and a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteCategoryAsync(string categoryGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteCategoryWithHttpInfoAsync(categoryGuid, userGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete category Use this endpoint to delete a specific custom category according to its unique GUID. The API will respond with an empty object and a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteCategoryWithHttpInfoAsync(string categoryGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->DeleteCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteCategory");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/categories/{category_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete managed account Use this endpoint to delete a partner-managed account according to its unique GUID. If successful, the API will respond with a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <returns></returns>
        public void DeleteManagedAccount(string memberGuid, string userGuid, string accountGuid)
        {
            DeleteManagedAccountWithHttpInfo(memberGuid, userGuid, accountGuid);
        }

        /// <summary>
        /// Delete managed account Use this endpoint to delete a partner-managed account according to its unique GUID. If successful, the API will respond with a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteManagedAccountWithHttpInfo(string memberGuid, string userGuid, string accountGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteManagedAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteManagedAccount");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->DeleteManagedAccount");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/managed_members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete managed account Use this endpoint to delete a partner-managed account according to its unique GUID. If successful, the API will respond with a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteManagedAccountAsync(string memberGuid, string userGuid, string accountGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteManagedAccountWithHttpInfoAsync(memberGuid, userGuid, accountGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete managed account Use this endpoint to delete a partner-managed account according to its unique GUID. If successful, the API will respond with a status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteManagedAccountWithHttpInfoAsync(string memberGuid, string userGuid, string accountGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteManagedAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteManagedAccount");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->DeleteManagedAccount");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/managed_members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete managed member Use this endpoint to delete the specified partner-managed &#x60;member&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns></returns>
        public void DeleteManagedMember(string memberGuid, string userGuid)
        {
            DeleteManagedMemberWithHttpInfo(memberGuid, userGuid);
        }

        /// <summary>
        /// Delete managed member Use this endpoint to delete the specified partner-managed &#x60;member&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteManagedMemberWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteManagedMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteManagedMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/managed_members/{member_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete managed member Use this endpoint to delete the specified partner-managed &#x60;member&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteManagedMemberAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteManagedMemberWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete managed member Use this endpoint to delete the specified partner-managed &#x60;member&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteManagedMemberWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteManagedMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteManagedMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/managed_members/{member_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete managed transaction Use this endpoint to delete the specified partner-managed &#x60;transaction&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <returns></returns>
        public void DeleteManagedTransaction(string memberGuid, string userGuid, string transactionGuid)
        {
            DeleteManagedTransactionWithHttpInfo(memberGuid, userGuid, transactionGuid);
        }

        /// <summary>
        /// Delete managed transaction Use this endpoint to delete the specified partner-managed &#x60;transaction&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteManagedTransactionWithHttpInfo(string memberGuid, string userGuid, string transactionGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteManagedTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteManagedTransaction");

            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->DeleteManagedTransaction");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/managed_members/{member_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete managed transaction Use this endpoint to delete the specified partner-managed &#x60;transaction&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteManagedTransactionAsync(string memberGuid, string userGuid, string transactionGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteManagedTransactionWithHttpInfoAsync(memberGuid, userGuid, transactionGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete managed transaction Use this endpoint to delete the specified partner-managed &#x60;transaction&#x60;. The endpoint will respond with a status of &#x60;204 No Content&#x60; without a resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteManagedTransactionWithHttpInfoAsync(string memberGuid, string userGuid, string transactionGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteManagedTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteManagedTransaction");

            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->DeleteManagedTransaction");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/managed_members/{member_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete member Accessing this endpoint will permanently delete a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns></returns>
        public void DeleteMember(string memberGuid, string userGuid)
        {
            DeleteMemberWithHttpInfo(memberGuid, userGuid);
        }

        /// <summary>
        /// Delete member Accessing this endpoint will permanently delete a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteMemberWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/members/{member_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete member Accessing this endpoint will permanently delete a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteMemberAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteMemberWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete member Accessing this endpoint will permanently delete a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteMemberWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DeleteMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/members/{member_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete tag Use this endpoint to permanently delete a specific tag based on its unique GUID. If successful, the API will respond with status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns></returns>
        public void DeleteTag(string tagGuid, string userGuid)
        {
            DeleteTagWithHttpInfo(tagGuid, userGuid);
        }

        /// <summary>
        /// Delete tag Use this endpoint to permanently delete a specific tag based on its unique GUID. If successful, the API will respond with status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteTagWithHttpInfo(string tagGuid, string userGuid)
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->DeleteTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteTag");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/tags/{tag_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete tag Use this endpoint to permanently delete a specific tag based on its unique GUID. If successful, the API will respond with status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteTagAsync(string tagGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteTagWithHttpInfoAsync(tagGuid, userGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete tag Use this endpoint to permanently delete a specific tag based on its unique GUID. If successful, the API will respond with status of &#x60;204 No Content&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteTagWithHttpInfoAsync(string tagGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->DeleteTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteTag");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/tags/{tag_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete tagging Use this endpoint to delete a tagging according to its unique GUID. If successful, the API will respond with an empty body and a status of 204 NO Content.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns></returns>
        public void DeleteTagging(string taggingGuid, string userGuid)
        {
            DeleteTaggingWithHttpInfo(taggingGuid, userGuid);
        }

        /// <summary>
        /// Delete tagging Use this endpoint to delete a tagging according to its unique GUID. If successful, the API will respond with an empty body and a status of 204 NO Content.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteTaggingWithHttpInfo(string taggingGuid, string userGuid)
        {
            // verify the required parameter 'taggingGuid' is set
            if (taggingGuid == null)
                throw new ApiException(400, "Missing required parameter 'taggingGuid' when calling MxPlatformApi->DeleteTagging");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteTagging");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tagging_guid", ClientUtils.ParameterToString(taggingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/taggings/{tagging_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete tagging Use this endpoint to delete a tagging according to its unique GUID. If successful, the API will respond with an empty body and a status of 204 NO Content.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteTaggingAsync(string taggingGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteTaggingWithHttpInfoAsync(taggingGuid, userGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete tagging Use this endpoint to delete a tagging according to its unique GUID. If successful, the API will respond with an empty body and a status of 204 NO Content.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteTaggingWithHttpInfoAsync(string taggingGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'taggingGuid' is set
            if (taggingGuid == null)
                throw new ApiException(400, "Missing required parameter 'taggingGuid' when calling MxPlatformApi->DeleteTagging");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteTagging");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tagging_guid", ClientUtils.ParameterToString(taggingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/taggings/{tagging_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete transaction rule Use this endpoint to permanently delete a transaction rule based on its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns></returns>
        public void DeleteTransactionRule(string transactionRuleGuid, string userGuid)
        {
            DeleteTransactionRuleWithHttpInfo(transactionRuleGuid, userGuid);
        }

        /// <summary>
        /// Delete transaction rule Use this endpoint to permanently delete a transaction rule based on its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteTransactionRuleWithHttpInfo(string transactionRuleGuid, string userGuid)
        {
            // verify the required parameter 'transactionRuleGuid' is set
            if (transactionRuleGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleGuid' when calling MxPlatformApi->DeleteTransactionRule");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteTransactionRule");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_rule_guid", ClientUtils.ParameterToString(transactionRuleGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}/transaction_rules/{transaction_rule_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete transaction rule Use this endpoint to permanently delete a transaction rule based on its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteTransactionRuleAsync(string transactionRuleGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteTransactionRuleWithHttpInfoAsync(transactionRuleGuid, userGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete transaction rule Use this endpoint to permanently delete a transaction rule based on its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteTransactionRuleWithHttpInfoAsync(string transactionRuleGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'transactionRuleGuid' is set
            if (transactionRuleGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleGuid' when calling MxPlatformApi->DeleteTransactionRule");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteTransactionRule");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_rule_guid", ClientUtils.ParameterToString(transactionRuleGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}/transaction_rules/{transaction_rule_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete user Use this endpoint to delete the specified &#x60;user&#x60;. The response will have a status of &#x60;204 No Content&#x60; without an object.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns></returns>
        public void DeleteUser(string userGuid)
        {
            DeleteUserWithHttpInfo(userGuid);
        }

        /// <summary>
        /// Delete user Use this endpoint to delete the specified &#x60;user&#x60;. The response will have a status of &#x60;204 No Content&#x60; without an object.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteUserWithHttpInfo(string userGuid)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteUser");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/users/{user_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete user Use this endpoint to delete the specified &#x60;user&#x60;. The response will have a status of &#x60;204 No Content&#x60; without an object.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteUserAsync(string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteUserWithHttpInfoAsync(userGuid, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete user Use this endpoint to delete the specified &#x60;user&#x60;. The response will have a status of &#x60;204 No Content&#x60; without an object.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteUserWithHttpInfoAsync(string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DeleteUser");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/users/{user_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Download statement pdf Use this endpoint to download a specified statement PDF.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>System.IO.Stream</returns>
        public System.IO.Stream DownloadStatementPDF(string memberGuid, string statementGuid, string userGuid)
        {
            ApiResponse<System.IO.Stream> localVarResponse = DownloadStatementPDFWithHttpInfo(memberGuid, statementGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Download statement pdf Use this endpoint to download a specified statement PDF.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of System.IO.Stream</returns>
        public ApiResponse<System.IO.Stream> DownloadStatementPDFWithHttpInfo(string memberGuid, string statementGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DownloadStatementPDF");

            // verify the required parameter 'statementGuid' is set
            if (statementGuid == null)
                throw new ApiException(400, "Missing required parameter 'statementGuid' when calling MxPlatformApi->DownloadStatementPDF");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DownloadStatementPDF");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+pdf"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("statement_guid", ClientUtils.ParameterToString(statementGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<System.IO.Stream>("/users/{user_guid}/members/{member_guid}/statements/{statement_guid}.pdf", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DownloadStatementPDF", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Download statement pdf Use this endpoint to download a specified statement PDF.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of System.IO.Stream</returns>
        public async System.Threading.Tasks.Task<System.IO.Stream> DownloadStatementPDFAsync(string memberGuid, string statementGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<System.IO.Stream> localVarResponse = await DownloadStatementPDFWithHttpInfoAsync(memberGuid, statementGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Download statement pdf Use this endpoint to download a specified statement PDF.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (System.IO.Stream)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<System.IO.Stream>> DownloadStatementPDFWithHttpInfoAsync(string memberGuid, string statementGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->DownloadStatementPDF");

            // verify the required parameter 'statementGuid' is set
            if (statementGuid == null)
                throw new ApiException(400, "Missing required parameter 'statementGuid' when calling MxPlatformApi->DownloadStatementPDF");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->DownloadStatementPDF");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+pdf"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("statement_guid", ClientUtils.ParameterToString(statementGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<System.IO.Stream>("/users/{user_guid}/members/{member_guid}/statements/{statement_guid}.pdf", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DownloadStatementPDF", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Enhance transactions Use this endpoint to categorize, cleanse, and classify transactions. These transactions are not persisted or stored on the MX platform.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="enhanceTransactionsRequestBody">Transaction object to be enhanced</param>
        /// <returns>EnhanceTransactionsResponseBody</returns>
        public EnhanceTransactionsResponseBody EnhanceTransactions(EnhanceTransactionsRequestBody enhanceTransactionsRequestBody)
        {
            ApiResponse<EnhanceTransactionsResponseBody> localVarResponse = EnhanceTransactionsWithHttpInfo(enhanceTransactionsRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Enhance transactions Use this endpoint to categorize, cleanse, and classify transactions. These transactions are not persisted or stored on the MX platform.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="enhanceTransactionsRequestBody">Transaction object to be enhanced</param>
        /// <returns>ApiResponse of EnhanceTransactionsResponseBody</returns>
        public ApiResponse<EnhanceTransactionsResponseBody> EnhanceTransactionsWithHttpInfo(EnhanceTransactionsRequestBody enhanceTransactionsRequestBody)
        {
            // verify the required parameter 'enhanceTransactionsRequestBody' is set
            if (enhanceTransactionsRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'enhanceTransactionsRequestBody' when calling MxPlatformApi->EnhanceTransactions");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = enhanceTransactionsRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<EnhanceTransactionsResponseBody>("/transactions/enhance", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("EnhanceTransactions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Enhance transactions Use this endpoint to categorize, cleanse, and classify transactions. These transactions are not persisted or stored on the MX platform.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="enhanceTransactionsRequestBody">Transaction object to be enhanced</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of EnhanceTransactionsResponseBody</returns>
        public async System.Threading.Tasks.Task<EnhanceTransactionsResponseBody> EnhanceTransactionsAsync(EnhanceTransactionsRequestBody enhanceTransactionsRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<EnhanceTransactionsResponseBody> localVarResponse = await EnhanceTransactionsWithHttpInfoAsync(enhanceTransactionsRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Enhance transactions Use this endpoint to categorize, cleanse, and classify transactions. These transactions are not persisted or stored on the MX platform.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="enhanceTransactionsRequestBody">Transaction object to be enhanced</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (EnhanceTransactionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<EnhanceTransactionsResponseBody>> EnhanceTransactionsWithHttpInfoAsync(EnhanceTransactionsRequestBody enhanceTransactionsRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'enhanceTransactionsRequestBody' is set
            if (enhanceTransactionsRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'enhanceTransactionsRequestBody' when calling MxPlatformApi->EnhanceTransactions");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = enhanceTransactionsRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<EnhanceTransactionsResponseBody>("/transactions/enhance", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("EnhanceTransactions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Extend history Some institutions allow developers to access an extended transaction history with up to 24 months of data associated with a particular member. The process for fetching and then reading this extended transaction history is much like standard aggregation, and it may trigger multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique identifier for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique identifier for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody ExtendHistory(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = ExtendHistoryWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Extend history Some institutions allow developers to access an extended transaction history with up to 24 months of data associated with a particular member. The process for fetching and then reading this extended transaction history is much like standard aggregation, and it may trigger multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique identifier for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique identifier for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> ExtendHistoryWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ExtendHistory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ExtendHistory");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/extend_history", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ExtendHistory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Extend history Some institutions allow developers to access an extended transaction history with up to 24 months of data associated with a particular member. The process for fetching and then reading this extended transaction history is much like standard aggregation, and it may trigger multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique identifier for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique identifier for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> ExtendHistoryAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await ExtendHistoryWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Extend history Some institutions allow developers to access an extended transaction history with up to 24 months of data associated with a particular member. The process for fetching and then reading this extended transaction history is much like standard aggregation, and it may trigger multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique identifier for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique identifier for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> ExtendHistoryWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ExtendHistory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ExtendHistory");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/extend_history", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ExtendHistory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Fetch statements Use this endpoint to fetch the statements associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody FetchStatements(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = FetchStatementsWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Fetch statements Use this endpoint to fetch the statements associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> FetchStatementsWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->FetchStatements");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->FetchStatements");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/fetch_statements", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("FetchStatements", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Fetch statements Use this endpoint to fetch the statements associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> FetchStatementsAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await FetchStatementsWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Fetch statements Use this endpoint to fetch the statements associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> FetchStatementsWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->FetchStatements");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->FetchStatements");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/fetch_statements", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("FetchStatements", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Identify member The identify endpoint begins an identification process for an already-existing member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody IdentifyMember(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = IdentifyMemberWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Identify member The identify endpoint begins an identification process for an already-existing member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> IdentifyMemberWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->IdentifyMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->IdentifyMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/identify", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("IdentifyMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Identify member The identify endpoint begins an identification process for an already-existing member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> IdentifyMemberAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await IdentifyMemberWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Identify member The identify endpoint begins an identification process for an already-existing member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> IdentifyMemberWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->IdentifyMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->IdentifyMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/identify", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("IdentifyMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List account numbers by account This endpoint returns a list of account numbers associated with the specified &#x60;account&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>AccountNumbersResponseBody</returns>
        public AccountNumbersResponseBody ListAccountNumbersByAccount(string accountGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<AccountNumbersResponseBody> localVarResponse = ListAccountNumbersByAccountWithHttpInfo(accountGuid, userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List account numbers by account This endpoint returns a list of account numbers associated with the specified &#x60;account&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of AccountNumbersResponseBody</returns>
        public ApiResponse<AccountNumbersResponseBody> ListAccountNumbersByAccountWithHttpInfo(string accountGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ListAccountNumbersByAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListAccountNumbersByAccount");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AccountNumbersResponseBody>("/users/{user_guid}/accounts/{account_guid}/account_numbers", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAccountNumbersByAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List account numbers by account This endpoint returns a list of account numbers associated with the specified &#x60;account&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountNumbersResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountNumbersResponseBody> ListAccountNumbersByAccountAsync(string accountGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountNumbersResponseBody> localVarResponse = await ListAccountNumbersByAccountWithHttpInfoAsync(accountGuid, userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List account numbers by account This endpoint returns a list of account numbers associated with the specified &#x60;account&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountNumbersResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountNumbersResponseBody>> ListAccountNumbersByAccountWithHttpInfoAsync(string accountGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ListAccountNumbersByAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListAccountNumbersByAccount");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<AccountNumbersResponseBody>("/users/{user_guid}/accounts/{account_guid}/account_numbers", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAccountNumbersByAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List account numbers by member This endpoint returns a list of account numbers associated with the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>AccountNumbersResponseBody</returns>
        public AccountNumbersResponseBody ListAccountNumbersByMember(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<AccountNumbersResponseBody> localVarResponse = ListAccountNumbersByMemberWithHttpInfo(memberGuid, userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List account numbers by member This endpoint returns a list of account numbers associated with the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of AccountNumbersResponseBody</returns>
        public ApiResponse<AccountNumbersResponseBody> ListAccountNumbersByMemberWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListAccountNumbersByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListAccountNumbersByMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AccountNumbersResponseBody>("/users/{user_guid}/members/{member_guid}/account_numbers", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAccountNumbersByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List account numbers by member This endpoint returns a list of account numbers associated with the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountNumbersResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountNumbersResponseBody> ListAccountNumbersByMemberAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountNumbersResponseBody> localVarResponse = await ListAccountNumbersByMemberWithHttpInfoAsync(memberGuid, userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List account numbers by member This endpoint returns a list of account numbers associated with the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountNumbersResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountNumbersResponseBody>> ListAccountNumbersByMemberWithHttpInfoAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListAccountNumbersByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListAccountNumbersByMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<AccountNumbersResponseBody>("/users/{user_guid}/members/{member_guid}/account_numbers", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAccountNumbersByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List account owners by member This endpoint returns an array with information about every account associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>AccountOwnersResponseBody</returns>
        public AccountOwnersResponseBody ListAccountOwnersByMember(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<AccountOwnersResponseBody> localVarResponse = ListAccountOwnersByMemberWithHttpInfo(memberGuid, userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List account owners by member This endpoint returns an array with information about every account associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of AccountOwnersResponseBody</returns>
        public ApiResponse<AccountOwnersResponseBody> ListAccountOwnersByMemberWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListAccountOwnersByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListAccountOwnersByMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AccountOwnersResponseBody>("/users/{user_guid}/members/{member_guid}/account_owners", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAccountOwnersByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List account owners by member This endpoint returns an array with information about every account associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountOwnersResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountOwnersResponseBody> ListAccountOwnersByMemberAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountOwnersResponseBody> localVarResponse = await ListAccountOwnersByMemberWithHttpInfoAsync(memberGuid, userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List account owners by member This endpoint returns an array with information about every account associated with a particular member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountOwnersResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountOwnersResponseBody>> ListAccountOwnersByMemberWithHttpInfoAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListAccountOwnersByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListAccountOwnersByMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<AccountOwnersResponseBody>("/users/{user_guid}/members/{member_guid}/account_owners", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAccountOwnersByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List categories Use this endpoint to list all categories associated with a &#x60;user&#x60;, including both default and custom categories.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>CategoriesResponseBody</returns>
        public CategoriesResponseBody ListCategories(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<CategoriesResponseBody> localVarResponse = ListCategoriesWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List categories Use this endpoint to list all categories associated with a &#x60;user&#x60;, including both default and custom categories.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of CategoriesResponseBody</returns>
        public ApiResponse<CategoriesResponseBody> ListCategoriesWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListCategories");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CategoriesResponseBody>("/users/{user_guid}/categories", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCategories", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List categories Use this endpoint to list all categories associated with a &#x60;user&#x60;, including both default and custom categories.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CategoriesResponseBody</returns>
        public async System.Threading.Tasks.Task<CategoriesResponseBody> ListCategoriesAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CategoriesResponseBody> localVarResponse = await ListCategoriesWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List categories Use this endpoint to list all categories associated with a &#x60;user&#x60;, including both default and custom categories.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CategoriesResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CategoriesResponseBody>> ListCategoriesWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListCategories");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<CategoriesResponseBody>("/users/{user_guid}/categories", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCategories", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List default categories Use this endpoint to retrieve a list of all the default categories and subcategories offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>CategoriesResponseBody</returns>
        public CategoriesResponseBody ListDefaultCategories(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<CategoriesResponseBody> localVarResponse = ListDefaultCategoriesWithHttpInfo(page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List default categories Use this endpoint to retrieve a list of all the default categories and subcategories offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of CategoriesResponseBody</returns>
        public ApiResponse<CategoriesResponseBody> ListDefaultCategoriesWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CategoriesResponseBody>("/categories/default", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDefaultCategories", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List default categories Use this endpoint to retrieve a list of all the default categories and subcategories offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CategoriesResponseBody</returns>
        public async System.Threading.Tasks.Task<CategoriesResponseBody> ListDefaultCategoriesAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CategoriesResponseBody> localVarResponse = await ListDefaultCategoriesWithHttpInfoAsync(page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List default categories Use this endpoint to retrieve a list of all the default categories and subcategories offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CategoriesResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CategoriesResponseBody>> ListDefaultCategoriesWithHttpInfoAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<CategoriesResponseBody>("/categories/default", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDefaultCategories", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List default categories by user Use this endpoint to retrieve a list of all the default categories and subcategories, scoped by user, offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>CategoriesResponseBody</returns>
        public CategoriesResponseBody ListDefaultCategoriesByUser(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<CategoriesResponseBody> localVarResponse = ListDefaultCategoriesByUserWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List default categories by user Use this endpoint to retrieve a list of all the default categories and subcategories, scoped by user, offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of CategoriesResponseBody</returns>
        public ApiResponse<CategoriesResponseBody> ListDefaultCategoriesByUserWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListDefaultCategoriesByUser");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CategoriesResponseBody>("/users/{user_guid}/categories/default", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDefaultCategoriesByUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List default categories by user Use this endpoint to retrieve a list of all the default categories and subcategories, scoped by user, offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CategoriesResponseBody</returns>
        public async System.Threading.Tasks.Task<CategoriesResponseBody> ListDefaultCategoriesByUserAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CategoriesResponseBody> localVarResponse = await ListDefaultCategoriesByUserWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List default categories by user Use this endpoint to retrieve a list of all the default categories and subcategories, scoped by user, offered within the MX Platform API. In other words, each item in the returned list will have its &#x60;is_default&#x60; field set to &#x60;true&#x60;. There are currently 119 default categories and subcategories. Both the _list default categories_ and _list default categories by user_ endpoints return the same results. The different routes are provided for convenience.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CategoriesResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CategoriesResponseBody>> ListDefaultCategoriesByUserWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListDefaultCategoriesByUser");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<CategoriesResponseBody>("/users/{user_guid}/categories/default", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDefaultCategoriesByUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List favorite institutions This endpoint returns a paginated list containing institutions that have been set as the partner’s favorites, sorted by popularity. Please contact MX to set a list of favorites.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>InstitutionsResponseBody</returns>
        public InstitutionsResponseBody ListFavoriteInstitutions(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<InstitutionsResponseBody> localVarResponse = ListFavoriteInstitutionsWithHttpInfo(page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List favorite institutions This endpoint returns a paginated list containing institutions that have been set as the partner’s favorites, sorted by popularity. Please contact MX to set a list of favorites.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of InstitutionsResponseBody</returns>
        public ApiResponse<InstitutionsResponseBody> ListFavoriteInstitutionsWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<InstitutionsResponseBody>("/institutions/favorites", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListFavoriteInstitutions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List favorite institutions This endpoint returns a paginated list containing institutions that have been set as the partner’s favorites, sorted by popularity. Please contact MX to set a list of favorites.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of InstitutionsResponseBody</returns>
        public async System.Threading.Tasks.Task<InstitutionsResponseBody> ListFavoriteInstitutionsAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<InstitutionsResponseBody> localVarResponse = await ListFavoriteInstitutionsWithHttpInfoAsync(page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List favorite institutions This endpoint returns a paginated list containing institutions that have been set as the partner’s favorites, sorted by popularity. Please contact MX to set a list of favorites.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (InstitutionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<InstitutionsResponseBody>> ListFavoriteInstitutionsWithHttpInfoAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<InstitutionsResponseBody>("/institutions/favorites", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListFavoriteInstitutions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List holdings This endpoint returns all holdings associated with the specified &#x60;user&#x60; across all accounts and members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <returns>HoldingsResponseBody</returns>
        public HoldingsResponseBody ListHoldings(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            ApiResponse<HoldingsResponseBody> localVarResponse = ListHoldingsWithHttpInfo(userGuid, fromDate, page, recordsPerPage, toDate);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List holdings This endpoint returns all holdings associated with the specified &#x60;user&#x60; across all accounts and members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <returns>ApiResponse of HoldingsResponseBody</returns>
        public ApiResponse<HoldingsResponseBody> ListHoldingsWithHttpInfo(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListHoldings");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<HoldingsResponseBody>("/users/{user_guid}/holdings", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListHoldings", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List holdings This endpoint returns all holdings associated with the specified &#x60;user&#x60; across all accounts and members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of HoldingsResponseBody</returns>
        public async System.Threading.Tasks.Task<HoldingsResponseBody> ListHoldingsAsync(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<HoldingsResponseBody> localVarResponse = await ListHoldingsWithHttpInfoAsync(userGuid, fromDate, page, recordsPerPage, toDate, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List holdings This endpoint returns all holdings associated with the specified &#x60;user&#x60; across all accounts and members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (HoldingsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<HoldingsResponseBody>> ListHoldingsWithHttpInfoAsync(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListHoldings");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<HoldingsResponseBody>("/users/{user_guid}/holdings", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListHoldings", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List holdings by member This endpoint returns all holdings associated with the specified &#x60;member&#x60; across all accounts.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <returns>HoldingsResponseBody</returns>
        public HoldingsResponseBody ListHoldingsByMember(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            ApiResponse<HoldingsResponseBody> localVarResponse = ListHoldingsByMemberWithHttpInfo(memberGuid, userGuid, fromDate, page, recordsPerPage, toDate);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List holdings by member This endpoint returns all holdings associated with the specified &#x60;member&#x60; across all accounts.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <returns>ApiResponse of HoldingsResponseBody</returns>
        public ApiResponse<HoldingsResponseBody> ListHoldingsByMemberWithHttpInfo(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListHoldingsByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListHoldingsByMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<HoldingsResponseBody>("/users/{user_guid}/members/{member_guid}/holdings", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListHoldingsByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List holdings by member This endpoint returns all holdings associated with the specified &#x60;member&#x60; across all accounts.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of HoldingsResponseBody</returns>
        public async System.Threading.Tasks.Task<HoldingsResponseBody> ListHoldingsByMemberAsync(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<HoldingsResponseBody> localVarResponse = await ListHoldingsByMemberWithHttpInfoAsync(memberGuid, userGuid, fromDate, page, recordsPerPage, toDate, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List holdings by member This endpoint returns all holdings associated with the specified &#x60;member&#x60; across all accounts.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter holdings from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter holdings to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (HoldingsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<HoldingsResponseBody>> ListHoldingsByMemberWithHttpInfoAsync(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListHoldingsByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListHoldingsByMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<HoldingsResponseBody>("/users/{user_guid}/members/{member_guid}/holdings", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListHoldingsByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List institution credentials Use this endpoint to see which credentials will be needed to create a member for a specific institution.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>CredentialsResponseBody</returns>
        public CredentialsResponseBody ListInstitutionCredentials(string institutionCode, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<CredentialsResponseBody> localVarResponse = ListInstitutionCredentialsWithHttpInfo(institutionCode, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List institution credentials Use this endpoint to see which credentials will be needed to create a member for a specific institution.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of CredentialsResponseBody</returns>
        public ApiResponse<CredentialsResponseBody> ListInstitutionCredentialsWithHttpInfo(string institutionCode, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'institutionCode' is set
            if (institutionCode == null)
                throw new ApiException(400, "Missing required parameter 'institutionCode' when calling MxPlatformApi->ListInstitutionCredentials");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("institution_code", ClientUtils.ParameterToString(institutionCode)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CredentialsResponseBody>("/institutions/{institution_code}/credentials", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstitutionCredentials", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List institution credentials Use this endpoint to see which credentials will be needed to create a member for a specific institution.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CredentialsResponseBody</returns>
        public async System.Threading.Tasks.Task<CredentialsResponseBody> ListInstitutionCredentialsAsync(string institutionCode, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CredentialsResponseBody> localVarResponse = await ListInstitutionCredentialsWithHttpInfoAsync(institutionCode, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List institution credentials Use this endpoint to see which credentials will be needed to create a member for a specific institution.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CredentialsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CredentialsResponseBody>> ListInstitutionCredentialsWithHttpInfoAsync(string institutionCode, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'institutionCode' is set
            if (institutionCode == null)
                throw new ApiException(400, "Missing required parameter 'institutionCode' when calling MxPlatformApi->ListInstitutionCredentials");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("institution_code", ClientUtils.ParameterToString(institutionCode)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<CredentialsResponseBody>("/institutions/{institution_code}/credentials", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstitutionCredentials", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List institutions This endpoint returns a list of institutions based on the specified search term or parameter.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="name">This will list only institutions in which the appended string appears. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="supportsAccountIdentification">Filter only institutions which support account identification. (optional)</param>
        /// <param name="supportsAccountStatement">Filter only institutions which support account statements. (optional)</param>
        /// <param name="supportsAccountVerification">Filter only institutions which support account verification. (optional)</param>
        /// <param name="supportsTransactionHistory">Filter only institutions which support extended transaction history. (optional)</param>
        /// <returns>InstitutionsResponseBody</returns>
        public InstitutionsResponseBody ListInstitutions(string name = default(string), int? page = default(int?), int? recordsPerPage = default(int?), bool? supportsAccountIdentification = default(bool?), bool? supportsAccountStatement = default(bool?), bool? supportsAccountVerification = default(bool?), bool? supportsTransactionHistory = default(bool?))
        {
            ApiResponse<InstitutionsResponseBody> localVarResponse = ListInstitutionsWithHttpInfo(name, page, recordsPerPage, supportsAccountIdentification, supportsAccountStatement, supportsAccountVerification, supportsTransactionHistory);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List institutions This endpoint returns a list of institutions based on the specified search term or parameter.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="name">This will list only institutions in which the appended string appears. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="supportsAccountIdentification">Filter only institutions which support account identification. (optional)</param>
        /// <param name="supportsAccountStatement">Filter only institutions which support account statements. (optional)</param>
        /// <param name="supportsAccountVerification">Filter only institutions which support account verification. (optional)</param>
        /// <param name="supportsTransactionHistory">Filter only institutions which support extended transaction history. (optional)</param>
        /// <returns>ApiResponse of InstitutionsResponseBody</returns>
        public ApiResponse<InstitutionsResponseBody> ListInstitutionsWithHttpInfo(string name = default(string), int? page = default(int?), int? recordsPerPage = default(int?), bool? supportsAccountIdentification = default(bool?), bool? supportsAccountStatement = default(bool?), bool? supportsAccountVerification = default(bool?), bool? supportsTransactionHistory = default(bool?))
        {
            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (name != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "name", name));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (supportsAccountIdentification != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_account_identification", supportsAccountIdentification));
            }
            if (supportsAccountStatement != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_account_statement", supportsAccountStatement));
            }
            if (supportsAccountVerification != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_account_verification", supportsAccountVerification));
            }
            if (supportsTransactionHistory != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_transaction_history", supportsTransactionHistory));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<InstitutionsResponseBody>("/institutions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstitutions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List institutions This endpoint returns a list of institutions based on the specified search term or parameter.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="name">This will list only institutions in which the appended string appears. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="supportsAccountIdentification">Filter only institutions which support account identification. (optional)</param>
        /// <param name="supportsAccountStatement">Filter only institutions which support account statements. (optional)</param>
        /// <param name="supportsAccountVerification">Filter only institutions which support account verification. (optional)</param>
        /// <param name="supportsTransactionHistory">Filter only institutions which support extended transaction history. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of InstitutionsResponseBody</returns>
        public async System.Threading.Tasks.Task<InstitutionsResponseBody> ListInstitutionsAsync(string name = default(string), int? page = default(int?), int? recordsPerPage = default(int?), bool? supportsAccountIdentification = default(bool?), bool? supportsAccountStatement = default(bool?), bool? supportsAccountVerification = default(bool?), bool? supportsTransactionHistory = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<InstitutionsResponseBody> localVarResponse = await ListInstitutionsWithHttpInfoAsync(name, page, recordsPerPage, supportsAccountIdentification, supportsAccountStatement, supportsAccountVerification, supportsTransactionHistory, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List institutions This endpoint returns a list of institutions based on the specified search term or parameter.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="name">This will list only institutions in which the appended string appears. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="supportsAccountIdentification">Filter only institutions which support account identification. (optional)</param>
        /// <param name="supportsAccountStatement">Filter only institutions which support account statements. (optional)</param>
        /// <param name="supportsAccountVerification">Filter only institutions which support account verification. (optional)</param>
        /// <param name="supportsTransactionHistory">Filter only institutions which support extended transaction history. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (InstitutionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<InstitutionsResponseBody>> ListInstitutionsWithHttpInfoAsync(string name = default(string), int? page = default(int?), int? recordsPerPage = default(int?), bool? supportsAccountIdentification = default(bool?), bool? supportsAccountStatement = default(bool?), bool? supportsAccountVerification = default(bool?), bool? supportsTransactionHistory = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (name != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "name", name));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (supportsAccountIdentification != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_account_identification", supportsAccountIdentification));
            }
            if (supportsAccountStatement != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_account_statement", supportsAccountStatement));
            }
            if (supportsAccountVerification != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_account_verification", supportsAccountVerification));
            }
            if (supportsTransactionHistory != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "supports_transaction_history", supportsTransactionHistory));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<InstitutionsResponseBody>("/institutions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstitutions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed accounts Use this endpoint to retrieve a list of all the partner-managed accounts associated with the given partner-manage member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>AccountsResponseBody</returns>
        public AccountsResponseBody ListManagedAccounts(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<AccountsResponseBody> localVarResponse = ListManagedAccountsWithHttpInfo(userGuid, memberGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed accounts Use this endpoint to retrieve a list of all the partner-managed accounts associated with the given partner-manage member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of AccountsResponseBody</returns>
        public ApiResponse<AccountsResponseBody> ListManagedAccountsWithHttpInfo(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListManagedAccounts");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListManagedAccounts");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AccountsResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedAccounts", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed accounts Use this endpoint to retrieve a list of all the partner-managed accounts associated with the given partner-manage member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountsResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountsResponseBody> ListManagedAccountsAsync(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountsResponseBody> localVarResponse = await ListManagedAccountsWithHttpInfoAsync(userGuid, memberGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed accounts Use this endpoint to retrieve a list of all the partner-managed accounts associated with the given partner-manage member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountsResponseBody>> ListManagedAccountsWithHttpInfoAsync(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListManagedAccounts");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListManagedAccounts");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<AccountsResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedAccounts", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed institutions This endpoint returns a list of institutions which can be used to create partner-managed members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>InstitutionsResponseBody</returns>
        public InstitutionsResponseBody ListManagedInstitutions(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<InstitutionsResponseBody> localVarResponse = ListManagedInstitutionsWithHttpInfo(page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed institutions This endpoint returns a list of institutions which can be used to create partner-managed members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of InstitutionsResponseBody</returns>
        public ApiResponse<InstitutionsResponseBody> ListManagedInstitutionsWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<InstitutionsResponseBody>("/managed_institutions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedInstitutions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed institutions This endpoint returns a list of institutions which can be used to create partner-managed members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of InstitutionsResponseBody</returns>
        public async System.Threading.Tasks.Task<InstitutionsResponseBody> ListManagedInstitutionsAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<InstitutionsResponseBody> localVarResponse = await ListManagedInstitutionsWithHttpInfoAsync(page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed institutions This endpoint returns a list of institutions which can be used to create partner-managed members.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (InstitutionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<InstitutionsResponseBody>> ListManagedInstitutionsWithHttpInfoAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<InstitutionsResponseBody>("/managed_institutions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedInstitutions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed members This endpoint returns a list of all the partner-managed members associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>MembersResponseBody</returns>
        public MembersResponseBody ListManagedMembers(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<MembersResponseBody> localVarResponse = ListManagedMembersWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed members This endpoint returns a list of all the partner-managed members associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of MembersResponseBody</returns>
        public ApiResponse<MembersResponseBody> ListManagedMembersWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListManagedMembers");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MembersResponseBody>("/users/{user_guid}/managed_members", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedMembers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed members This endpoint returns a list of all the partner-managed members associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MembersResponseBody</returns>
        public async System.Threading.Tasks.Task<MembersResponseBody> ListManagedMembersAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MembersResponseBody> localVarResponse = await ListManagedMembersWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed members This endpoint returns a list of all the partner-managed members associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MembersResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MembersResponseBody>> ListManagedMembersWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListManagedMembers");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MembersResponseBody>("/users/{user_guid}/managed_members", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedMembers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed transactions This endpoint returns a list of all the partner-managed transactions associated with the specified &#x60;account&#x60;, scoped through a &#x60;user&#x60; and a &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>TransactionsResponseBody</returns>
        public TransactionsResponseBody ListManagedTransactions(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = ListManagedTransactionsWithHttpInfo(userGuid, memberGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed transactions This endpoint returns a list of all the partner-managed transactions associated with the specified &#x60;account&#x60;, scoped through a &#x60;user&#x60; and a &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of TransactionsResponseBody</returns>
        public ApiResponse<TransactionsResponseBody> ListManagedTransactionsWithHttpInfo(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListManagedTransactions");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListManagedTransactions");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionsResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedTransactions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List managed transactions This endpoint returns a list of all the partner-managed transactions associated with the specified &#x60;account&#x60;, scoped through a &#x60;user&#x60; and a &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionsResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionsResponseBody> ListManagedTransactionsAsync(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = await ListManagedTransactionsWithHttpInfoAsync(userGuid, memberGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List managed transactions This endpoint returns a list of all the partner-managed transactions associated with the specified &#x60;account&#x60;, scoped through a &#x60;user&#x60; and a &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionsResponseBody>> ListManagedTransactionsWithHttpInfoAsync(string userGuid, string memberGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListManagedTransactions");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListManagedTransactions");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionsResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListManagedTransactions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List member challenges Use this endpoint for information on what multi-factor authentication challenges need to be answered in order to aggregate a member. If the aggregation is not challenged, i.e., the member does not have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;204 No Content&#x60; will be returned. If the aggregation has been challenged, i.e., the member does have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;200 OK&#x60; will be returned - along with the corresponding credentials.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ChallengesResponseBody</returns>
        public ChallengesResponseBody ListMemberChallenges(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<ChallengesResponseBody> localVarResponse = ListMemberChallengesWithHttpInfo(memberGuid, userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List member challenges Use this endpoint for information on what multi-factor authentication challenges need to be answered in order to aggregate a member. If the aggregation is not challenged, i.e., the member does not have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;204 No Content&#x60; will be returned. If the aggregation has been challenged, i.e., the member does have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;200 OK&#x60; will be returned - along with the corresponding credentials.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of ChallengesResponseBody</returns>
        public ApiResponse<ChallengesResponseBody> ListMemberChallengesWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListMemberChallenges");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListMemberChallenges");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ChallengesResponseBody>("/users/{user_guid}/members/{member_guid}/challenges", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMemberChallenges", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List member challenges Use this endpoint for information on what multi-factor authentication challenges need to be answered in order to aggregate a member. If the aggregation is not challenged, i.e., the member does not have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;204 No Content&#x60; will be returned. If the aggregation has been challenged, i.e., the member does have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;200 OK&#x60; will be returned - along with the corresponding credentials.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ChallengesResponseBody</returns>
        public async System.Threading.Tasks.Task<ChallengesResponseBody> ListMemberChallengesAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<ChallengesResponseBody> localVarResponse = await ListMemberChallengesWithHttpInfoAsync(memberGuid, userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List member challenges Use this endpoint for information on what multi-factor authentication challenges need to be answered in order to aggregate a member. If the aggregation is not challenged, i.e., the member does not have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;204 No Content&#x60; will be returned. If the aggregation has been challenged, i.e., the member does have a connection status of &#x60;CHALLENGED&#x60;, then code &#x60;200 OK&#x60; will be returned - along with the corresponding credentials.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ChallengesResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ChallengesResponseBody>> ListMemberChallengesWithHttpInfoAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListMemberChallenges");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListMemberChallenges");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<ChallengesResponseBody>("/users/{user_guid}/members/{member_guid}/challenges", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMemberChallenges", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List member credentials This endpoint returns an array which contains information on every non-MFA credential associated with a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>CredentialsResponseBody</returns>
        public CredentialsResponseBody ListMemberCredentials(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<CredentialsResponseBody> localVarResponse = ListMemberCredentialsWithHttpInfo(memberGuid, userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List member credentials This endpoint returns an array which contains information on every non-MFA credential associated with a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of CredentialsResponseBody</returns>
        public ApiResponse<CredentialsResponseBody> ListMemberCredentialsWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListMemberCredentials");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListMemberCredentials");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CredentialsResponseBody>("/users/{user_guid}/members/{member_guid}/credentials", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMemberCredentials", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List member credentials This endpoint returns an array which contains information on every non-MFA credential associated with a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CredentialsResponseBody</returns>
        public async System.Threading.Tasks.Task<CredentialsResponseBody> ListMemberCredentialsAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CredentialsResponseBody> localVarResponse = await ListMemberCredentialsWithHttpInfoAsync(memberGuid, userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List member credentials This endpoint returns an array which contains information on every non-MFA credential associated with a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CredentialsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CredentialsResponseBody>> ListMemberCredentialsWithHttpInfoAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListMemberCredentials");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListMemberCredentials");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<CredentialsResponseBody>("/users/{user_guid}/members/{member_guid}/credentials", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMemberCredentials", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List members This endpoint returns an array which contains information on every member associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>MembersResponseBody</returns>
        public MembersResponseBody ListMembers(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<MembersResponseBody> localVarResponse = ListMembersWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List members This endpoint returns an array which contains information on every member associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of MembersResponseBody</returns>
        public ApiResponse<MembersResponseBody> ListMembersWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListMembers");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MembersResponseBody>("/users/{user_guid}/members", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMembers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List members This endpoint returns an array which contains information on every member associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MembersResponseBody</returns>
        public async System.Threading.Tasks.Task<MembersResponseBody> ListMembersAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MembersResponseBody> localVarResponse = await ListMembersWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List members This endpoint returns an array which contains information on every member associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MembersResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MembersResponseBody>> ListMembersWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListMembers");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MembersResponseBody>("/users/{user_guid}/members", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMembers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List merchants This endpoint returns a paginated list of all the merchants in the MX system.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>MerchantsResponseBody</returns>
        public MerchantsResponseBody ListMerchants(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<MerchantsResponseBody> localVarResponse = ListMerchantsWithHttpInfo(page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List merchants This endpoint returns a paginated list of all the merchants in the MX system.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of MerchantsResponseBody</returns>
        public ApiResponse<MerchantsResponseBody> ListMerchantsWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MerchantsResponseBody>("/merchants", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMerchants", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List merchants This endpoint returns a paginated list of all the merchants in the MX system.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MerchantsResponseBody</returns>
        public async System.Threading.Tasks.Task<MerchantsResponseBody> ListMerchantsAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MerchantsResponseBody> localVarResponse = await ListMerchantsWithHttpInfoAsync(page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List merchants This endpoint returns a paginated list of all the merchants in the MX system.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MerchantsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MerchantsResponseBody>> ListMerchantsWithHttpInfoAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MerchantsResponseBody>("/merchants", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMerchants", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List statements by member Use this endpoint to get an array of available statements.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>StatementsResponseBody</returns>
        public StatementsResponseBody ListStatementsByMember(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<StatementsResponseBody> localVarResponse = ListStatementsByMemberWithHttpInfo(memberGuid, userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List statements by member Use this endpoint to get an array of available statements.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of StatementsResponseBody</returns>
        public ApiResponse<StatementsResponseBody> ListStatementsByMemberWithHttpInfo(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListStatementsByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListStatementsByMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<StatementsResponseBody>("/users/{user_guid}/members/{member_guid}/statements", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListStatementsByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List statements by member Use this endpoint to get an array of available statements.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of StatementsResponseBody</returns>
        public async System.Threading.Tasks.Task<StatementsResponseBody> ListStatementsByMemberAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<StatementsResponseBody> localVarResponse = await ListStatementsByMemberWithHttpInfoAsync(memberGuid, userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List statements by member Use this endpoint to get an array of available statements.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (StatementsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<StatementsResponseBody>> ListStatementsByMemberWithHttpInfoAsync(string memberGuid, string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListStatementsByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListStatementsByMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<StatementsResponseBody>("/users/{user_guid}/members/{member_guid}/statements", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListStatementsByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List taggings Use this endpoint to retrieve a list of all the taggings associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>TaggingsResponseBody</returns>
        public TaggingsResponseBody ListTaggings(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<TaggingsResponseBody> localVarResponse = ListTaggingsWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List taggings Use this endpoint to retrieve a list of all the taggings associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of TaggingsResponseBody</returns>
        public ApiResponse<TaggingsResponseBody> ListTaggingsWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTaggings");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TaggingsResponseBody>("/users/{user_guid}/taggings", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTaggings", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List taggings Use this endpoint to retrieve a list of all the taggings associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TaggingsResponseBody</returns>
        public async System.Threading.Tasks.Task<TaggingsResponseBody> ListTaggingsAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TaggingsResponseBody> localVarResponse = await ListTaggingsWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List taggings Use this endpoint to retrieve a list of all the taggings associated with a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TaggingsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TaggingsResponseBody>> ListTaggingsWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTaggings");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TaggingsResponseBody>("/users/{user_guid}/taggings", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTaggings", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List tags Use this endpoint to list all tags associated with the specified &#x60;user&#x60;. Each user includes the &#x60;Business&#x60; tag by default.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>TagsResponseBody</returns>
        public TagsResponseBody ListTags(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<TagsResponseBody> localVarResponse = ListTagsWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List tags Use this endpoint to list all tags associated with the specified &#x60;user&#x60;. Each user includes the &#x60;Business&#x60; tag by default.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of TagsResponseBody</returns>
        public ApiResponse<TagsResponseBody> ListTagsWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTags");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TagsResponseBody>("/users/{user_guid}/tags", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTags", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List tags Use this endpoint to list all tags associated with the specified &#x60;user&#x60;. Each user includes the &#x60;Business&#x60; tag by default.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TagsResponseBody</returns>
        public async System.Threading.Tasks.Task<TagsResponseBody> ListTagsAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TagsResponseBody> localVarResponse = await ListTagsWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List tags Use this endpoint to list all tags associated with the specified &#x60;user&#x60;. Each user includes the &#x60;Business&#x60; tag by default.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TagsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TagsResponseBody>> ListTagsWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTags");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TagsResponseBody>("/users/{user_guid}/tags", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTags", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transaction rules Use this endpoint to read the attributes of all existing transaction rules belonging to the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>TransactionRulesResponseBody</returns>
        public TransactionRulesResponseBody ListTransactionRules(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<TransactionRulesResponseBody> localVarResponse = ListTransactionRulesWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transaction rules Use this endpoint to read the attributes of all existing transaction rules belonging to the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of TransactionRulesResponseBody</returns>
        public ApiResponse<TransactionRulesResponseBody> ListTransactionRulesWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionRules");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionRulesResponseBody>("/users/{user_guid}/transaction_rules", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionRules", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transaction rules Use this endpoint to read the attributes of all existing transaction rules belonging to the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionRulesResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionRulesResponseBody> ListTransactionRulesAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionRulesResponseBody> localVarResponse = await ListTransactionRulesWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transaction rules Use this endpoint to read the attributes of all existing transaction rules belonging to the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionRulesResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionRulesResponseBody>> ListTransactionRulesWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionRules");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionRulesResponseBody>("/users/{user_guid}/transaction_rules", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionRules", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions Requests to this endpoint return a list of transactions associated with the specified &#x60;user&#x60;, accross all members and accounts associated with that &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>TransactionsResponseBody</returns>
        public TransactionsResponseBody ListTransactions(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = ListTransactionsWithHttpInfo(userGuid, fromDate, page, recordsPerPage, toDate);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions Requests to this endpoint return a list of transactions associated with the specified &#x60;user&#x60;, accross all members and accounts associated with that &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>ApiResponse of TransactionsResponseBody</returns>
        public ApiResponse<TransactionsResponseBody> ListTransactionsWithHttpInfo(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactions");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionsResponseBody>("/users/{user_guid}/transactions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions Requests to this endpoint return a list of transactions associated with the specified &#x60;user&#x60;, accross all members and accounts associated with that &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionsResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionsResponseBody> ListTransactionsAsync(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = await ListTransactionsWithHttpInfoAsync(userGuid, fromDate, page, recordsPerPage, toDate, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions Requests to this endpoint return a list of transactions associated with the specified &#x60;user&#x60;, accross all members and accounts associated with that &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionsResponseBody>> ListTransactionsWithHttpInfoAsync(string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactions");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionsResponseBody>("/users/{user_guid}/transactions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions by account This endpoint returns a list of the last 90 days of transactions associated with the specified account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>TransactionsResponseBody</returns>
        public TransactionsResponseBody ListTransactionsByAccount(string accountGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = ListTransactionsByAccountWithHttpInfo(accountGuid, userGuid, fromDate, page, recordsPerPage, toDate);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions by account This endpoint returns a list of the last 90 days of transactions associated with the specified account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>ApiResponse of TransactionsResponseBody</returns>
        public ApiResponse<TransactionsResponseBody> ListTransactionsByAccountWithHttpInfo(string accountGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ListTransactionsByAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionsByAccount");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionsResponseBody>("/users/{user_guid}/accounts/{account_guid}/transactions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionsByAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions by account This endpoint returns a list of the last 90 days of transactions associated with the specified account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionsResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionsResponseBody> ListTransactionsByAccountAsync(string accountGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = await ListTransactionsByAccountWithHttpInfoAsync(accountGuid, userGuid, fromDate, page, recordsPerPage, toDate, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions by account This endpoint returns a list of the last 90 days of transactions associated with the specified account.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionsResponseBody>> ListTransactionsByAccountWithHttpInfoAsync(string accountGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ListTransactionsByAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionsByAccount");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionsResponseBody>("/users/{user_guid}/accounts/{account_guid}/transactions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionsByAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions by member Requests to this endpoint return a list of transactions associated with the specified &#x60;member&#x60;, accross all accounts associated with that &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>TransactionsResponseBody</returns>
        public TransactionsResponseBody ListTransactionsByMember(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = ListTransactionsByMemberWithHttpInfo(memberGuid, userGuid, fromDate, page, recordsPerPage, toDate);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions by member Requests to this endpoint return a list of transactions associated with the specified &#x60;member&#x60;, accross all accounts associated with that &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>ApiResponse of TransactionsResponseBody</returns>
        public ApiResponse<TransactionsResponseBody> ListTransactionsByMemberWithHttpInfo(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListTransactionsByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionsByMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionsResponseBody>("/users/{user_guid}/members/{member_guid}/transactions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionsByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions by member Requests to this endpoint return a list of transactions associated with the specified &#x60;member&#x60;, accross all accounts associated with that &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionsResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionsResponseBody> ListTransactionsByMemberAsync(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = await ListTransactionsByMemberWithHttpInfoAsync(memberGuid, userGuid, fromDate, page, recordsPerPage, toDate, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions by member Requests to this endpoint return a list of transactions associated with the specified &#x60;member&#x60;, accross all accounts associated with that &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionsResponseBody>> ListTransactionsByMemberWithHttpInfoAsync(string memberGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ListTransactionsByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionsByMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionsResponseBody>("/users/{user_guid}/members/{member_guid}/transactions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionsByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions by tag Use this endpoint to get a list of all transactions associated with a particular tag according to the tag’s unique GUID. In other words, a list of all transactions that have been assigned to a particular tag using the create a tagging endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>TransactionsResponseBody</returns>
        public TransactionsResponseBody ListTransactionsByTag(string tagGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = ListTransactionsByTagWithHttpInfo(tagGuid, userGuid, fromDate, page, recordsPerPage, toDate);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions by tag Use this endpoint to get a list of all transactions associated with a particular tag according to the tag’s unique GUID. In other words, a list of all transactions that have been assigned to a particular tag using the create a tagging endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <returns>ApiResponse of TransactionsResponseBody</returns>
        public ApiResponse<TransactionsResponseBody> ListTransactionsByTagWithHttpInfo(string tagGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string))
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->ListTransactionsByTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionsByTag");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionsResponseBody>("/users/{user_guid}/tags/{tag_guid}/transactions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionsByTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List transactions by tag Use this endpoint to get a list of all transactions associated with a particular tag according to the tag’s unique GUID. In other words, a list of all transactions that have been assigned to a particular tag using the create a tagging endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionsResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionsResponseBody> ListTransactionsByTagAsync(string tagGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionsResponseBody> localVarResponse = await ListTransactionsByTagWithHttpInfoAsync(tagGuid, userGuid, fromDate, page, recordsPerPage, toDate, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List transactions by tag Use this endpoint to get a list of all transactions associated with a particular tag according to the tag’s unique GUID. In other words, a list of all transactions that have been assigned to a particular tag using the create a tagging endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="fromDate">Filter transactions from this date. (optional)</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="toDate">Filter transactions to this date. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionsResponseBody>> ListTransactionsByTagWithHttpInfoAsync(string tagGuid, string userGuid, string fromDate = default(string), int? page = default(int?), int? recordsPerPage = default(int?), string toDate = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->ListTransactionsByTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListTransactionsByTag");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (fromDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "from_date", fromDate));
            }
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }
            if (toDate != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "to_date", toDate));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionsResponseBody>("/users/{user_guid}/tags/{tag_guid}/transactions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListTransactionsByTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List accounts This endpoint returns a list of all the accounts associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>AccountsResponseBody</returns>
        public AccountsResponseBody ListUserAccounts(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<AccountsResponseBody> localVarResponse = ListUserAccountsWithHttpInfo(userGuid, page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List accounts This endpoint returns a list of all the accounts associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of AccountsResponseBody</returns>
        public ApiResponse<AccountsResponseBody> ListUserAccountsWithHttpInfo(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListUserAccounts");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AccountsResponseBody>("/users/{user_guid}/accounts", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListUserAccounts", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List accounts This endpoint returns a list of all the accounts associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountsResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountsResponseBody> ListUserAccountsAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountsResponseBody> localVarResponse = await ListUserAccountsWithHttpInfoAsync(userGuid, page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List accounts This endpoint returns a list of all the accounts associated with the specified &#x60;user&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountsResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountsResponseBody>> ListUserAccountsWithHttpInfoAsync(string userGuid, int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ListUserAccounts");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<AccountsResponseBody>("/users/{user_guid}/accounts", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListUserAccounts", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List users Use this endpoint to list every user you&#39;ve created in the MX Platform API.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>UsersResponseBody</returns>
        public UsersResponseBody ListUsers(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            ApiResponse<UsersResponseBody> localVarResponse = ListUsersWithHttpInfo(page, recordsPerPage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List users Use this endpoint to list every user you&#39;ve created in the MX Platform API.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <returns>ApiResponse of UsersResponseBody</returns>
        public ApiResponse<UsersResponseBody> ListUsersWithHttpInfo(int? page = default(int?), int? recordsPerPage = default(int?))
        {
            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<UsersResponseBody>("/users", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListUsers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List users Use this endpoint to list every user you&#39;ve created in the MX Platform API.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of UsersResponseBody</returns>
        public async System.Threading.Tasks.Task<UsersResponseBody> ListUsersAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<UsersResponseBody> localVarResponse = await ListUsersWithHttpInfoAsync(page, recordsPerPage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List users Use this endpoint to list every user you&#39;ve created in the MX Platform API.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="page">Specify current page. (optional)</param>
        /// <param name="recordsPerPage">Specify records per page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (UsersResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<UsersResponseBody>> ListUsersWithHttpInfoAsync(int? page = default(int?), int? recordsPerPage = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (page != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "page", page));
            }
            if (recordsPerPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "records_per_page", recordsPerPage));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<UsersResponseBody>("/users", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListUsers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read account This endpoint returns the specified &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>AccountResponseBody</returns>
        public AccountResponseBody ReadAccount(string accountGuid, string userGuid)
        {
            ApiResponse<AccountResponseBody> localVarResponse = ReadAccountWithHttpInfo(accountGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read account This endpoint returns the specified &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of AccountResponseBody</returns>
        public ApiResponse<AccountResponseBody> ReadAccountWithHttpInfo(string accountGuid, string userGuid)
        {
            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ReadAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadAccount");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AccountResponseBody>("/users/{user_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read account This endpoint returns the specified &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountResponseBody> ReadAccountAsync(string accountGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountResponseBody> localVarResponse = await ReadAccountWithHttpInfoAsync(accountGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read account This endpoint returns the specified &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountResponseBody>> ReadAccountWithHttpInfoAsync(string accountGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ReadAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadAccount");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<AccountResponseBody>("/users/{user_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read a custom category Use this endpoint to read the attributes of either a default category or a custom category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>CategoryResponseBody</returns>
        public CategoryResponseBody ReadCategory(string categoryGuid, string userGuid)
        {
            ApiResponse<CategoryResponseBody> localVarResponse = ReadCategoryWithHttpInfo(categoryGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read a custom category Use this endpoint to read the attributes of either a default category or a custom category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of CategoryResponseBody</returns>
        public ApiResponse<CategoryResponseBody> ReadCategoryWithHttpInfo(string categoryGuid, string userGuid)
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->ReadCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadCategory");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CategoryResponseBody>("/users/{user_guid}/categories/{category_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read a custom category Use this endpoint to read the attributes of either a default category or a custom category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CategoryResponseBody</returns>
        public async System.Threading.Tasks.Task<CategoryResponseBody> ReadCategoryAsync(string categoryGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CategoryResponseBody> localVarResponse = await ReadCategoryWithHttpInfoAsync(categoryGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read a custom category Use this endpoint to read the attributes of either a default category or a custom category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CategoryResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CategoryResponseBody>> ReadCategoryWithHttpInfoAsync(string categoryGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->ReadCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadCategory");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<CategoryResponseBody>("/users/{user_guid}/categories/{category_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read a default category Use this endpoint to read the attributes of a default category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>CategoryResponseBody</returns>
        public CategoryResponseBody ReadDefaultCategory(string categoryGuid, string userGuid)
        {
            ApiResponse<CategoryResponseBody> localVarResponse = ReadDefaultCategoryWithHttpInfo(categoryGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read a default category Use this endpoint to read the attributes of a default category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of CategoryResponseBody</returns>
        public ApiResponse<CategoryResponseBody> ReadDefaultCategoryWithHttpInfo(string categoryGuid, string userGuid)
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->ReadDefaultCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadDefaultCategory");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CategoryResponseBody>("/categories/{category_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadDefaultCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read a default category Use this endpoint to read the attributes of a default category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CategoryResponseBody</returns>
        public async System.Threading.Tasks.Task<CategoryResponseBody> ReadDefaultCategoryAsync(string categoryGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CategoryResponseBody> localVarResponse = await ReadDefaultCategoryWithHttpInfoAsync(categoryGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read a default category Use this endpoint to read the attributes of a default category.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CategoryResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CategoryResponseBody>> ReadDefaultCategoryWithHttpInfoAsync(string categoryGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->ReadDefaultCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadDefaultCategory");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<CategoryResponseBody>("/categories/{category_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadDefaultCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read holding Use this endpoint to read the attributes of a specific &#x60;holding&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="holdingGuid">The unique id for a &#x60;holding&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>HoldingResponseBody</returns>
        public HoldingResponseBody ReadHolding(string holdingGuid, string userGuid)
        {
            ApiResponse<HoldingResponseBody> localVarResponse = ReadHoldingWithHttpInfo(holdingGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read holding Use this endpoint to read the attributes of a specific &#x60;holding&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="holdingGuid">The unique id for a &#x60;holding&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of HoldingResponseBody</returns>
        public ApiResponse<HoldingResponseBody> ReadHoldingWithHttpInfo(string holdingGuid, string userGuid)
        {
            // verify the required parameter 'holdingGuid' is set
            if (holdingGuid == null)
                throw new ApiException(400, "Missing required parameter 'holdingGuid' when calling MxPlatformApi->ReadHolding");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadHolding");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("holding_guid", ClientUtils.ParameterToString(holdingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<HoldingResponseBody>("/users/{user_guid}/holdings/{holding_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadHolding", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read holding Use this endpoint to read the attributes of a specific &#x60;holding&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="holdingGuid">The unique id for a &#x60;holding&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of HoldingResponseBody</returns>
        public async System.Threading.Tasks.Task<HoldingResponseBody> ReadHoldingAsync(string holdingGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<HoldingResponseBody> localVarResponse = await ReadHoldingWithHttpInfoAsync(holdingGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read holding Use this endpoint to read the attributes of a specific &#x60;holding&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="holdingGuid">The unique id for a &#x60;holding&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (HoldingResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<HoldingResponseBody>> ReadHoldingWithHttpInfoAsync(string holdingGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'holdingGuid' is set
            if (holdingGuid == null)
                throw new ApiException(400, "Missing required parameter 'holdingGuid' when calling MxPlatformApi->ReadHolding");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadHolding");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("holding_guid", ClientUtils.ParameterToString(holdingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<HoldingResponseBody>("/users/{user_guid}/holdings/{holding_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadHolding", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read institution This endpoint returns information about the institution specified by &#x60;institution_code&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <returns>InstitutionResponseBody</returns>
        public InstitutionResponseBody ReadInstitution(string institutionCode)
        {
            ApiResponse<InstitutionResponseBody> localVarResponse = ReadInstitutionWithHttpInfo(institutionCode);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read institution This endpoint returns information about the institution specified by &#x60;institution_code&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <returns>ApiResponse of InstitutionResponseBody</returns>
        public ApiResponse<InstitutionResponseBody> ReadInstitutionWithHttpInfo(string institutionCode)
        {
            // verify the required parameter 'institutionCode' is set
            if (institutionCode == null)
                throw new ApiException(400, "Missing required parameter 'institutionCode' when calling MxPlatformApi->ReadInstitution");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("institution_code", ClientUtils.ParameterToString(institutionCode)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<InstitutionResponseBody>("/institutions/{institution_code}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadInstitution", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read institution This endpoint returns information about the institution specified by &#x60;institution_code&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of InstitutionResponseBody</returns>
        public async System.Threading.Tasks.Task<InstitutionResponseBody> ReadInstitutionAsync(string institutionCode, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<InstitutionResponseBody> localVarResponse = await ReadInstitutionWithHttpInfoAsync(institutionCode, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read institution This endpoint returns information about the institution specified by &#x60;institution_code&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="institutionCode">The institution_code of the institution.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (InstitutionResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<InstitutionResponseBody>> ReadInstitutionWithHttpInfoAsync(string institutionCode, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'institutionCode' is set
            if (institutionCode == null)
                throw new ApiException(400, "Missing required parameter 'institutionCode' when calling MxPlatformApi->ReadInstitution");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("institution_code", ClientUtils.ParameterToString(institutionCode)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<InstitutionResponseBody>("/institutions/{institution_code}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadInstitution", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read managed account Use this endpoint to read the attributes of a partner-managed account according to its unique guid.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <returns>AccountResponseBody</returns>
        public AccountResponseBody ReadManagedAccount(string memberGuid, string userGuid, string accountGuid)
        {
            ApiResponse<AccountResponseBody> localVarResponse = ReadManagedAccountWithHttpInfo(memberGuid, userGuid, accountGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read managed account Use this endpoint to read the attributes of a partner-managed account according to its unique guid.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <returns>ApiResponse of AccountResponseBody</returns>
        public ApiResponse<AccountResponseBody> ReadManagedAccountWithHttpInfo(string memberGuid, string userGuid, string accountGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadManagedAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadManagedAccount");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ReadManagedAccount");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AccountResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read managed account Use this endpoint to read the attributes of a partner-managed account according to its unique guid.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountResponseBody> ReadManagedAccountAsync(string memberGuid, string userGuid, string accountGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountResponseBody> localVarResponse = await ReadManagedAccountWithHttpInfoAsync(memberGuid, userGuid, accountGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read managed account Use this endpoint to read the attributes of a partner-managed account according to its unique guid.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountResponseBody>> ReadManagedAccountWithHttpInfoAsync(string memberGuid, string userGuid, string accountGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadManagedAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadManagedAccount");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->ReadManagedAccount");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<AccountResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read managed member This endpoint returns the attributes of the specified partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody ReadManagedMember(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = ReadManagedMemberWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read managed member This endpoint returns the attributes of the specified partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> ReadManagedMemberWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadManagedMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadManagedMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MemberResponseBody>("/users/{user_guid}/managed_members/{member_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read managed member This endpoint returns the attributes of the specified partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> ReadManagedMemberAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await ReadManagedMemberWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read managed member This endpoint returns the attributes of the specified partner-managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> ReadManagedMemberWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadManagedMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadManagedMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MemberResponseBody>("/users/{user_guid}/managed_members/{member_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read managed transaction Requests to this endpoint will return the attributes of the specified partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <returns>TransactionResponseBody</returns>
        public TransactionResponseBody ReadManagedTransaction(string memberGuid, string userGuid, string transactionGuid)
        {
            ApiResponse<TransactionResponseBody> localVarResponse = ReadManagedTransactionWithHttpInfo(memberGuid, userGuid, transactionGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read managed transaction Requests to this endpoint will return the attributes of the specified partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <returns>ApiResponse of TransactionResponseBody</returns>
        public ApiResponse<TransactionResponseBody> ReadManagedTransactionWithHttpInfo(string memberGuid, string userGuid, string transactionGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadManagedTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadManagedTransaction");

            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->ReadManagedTransaction");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read managed transaction Requests to this endpoint will return the attributes of the specified partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionResponseBody> ReadManagedTransactionAsync(string memberGuid, string userGuid, string transactionGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionResponseBody> localVarResponse = await ReadManagedTransactionWithHttpInfoAsync(memberGuid, userGuid, transactionGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read managed transaction Requests to this endpoint will return the attributes of the specified partner-managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionResponseBody>> ReadManagedTransactionWithHttpInfoAsync(string memberGuid, string userGuid, string transactionGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadManagedTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadManagedTransaction");

            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->ReadManagedTransaction");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read member Use this endpoint to read the attributes of a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody ReadMember(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = ReadMemberWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read member Use this endpoint to read the attributes of a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> ReadMemberWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MemberResponseBody>("/users/{user_guid}/members/{member_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read member Use this endpoint to read the attributes of a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> ReadMemberAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await ReadMemberWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read member Use this endpoint to read the attributes of a specific member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> ReadMemberWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read member status This endpoint provides the status of the members most recent aggregation event. This is an important step in the aggregation process, and the results returned by this endpoint should determine what you do next in order to successfully aggregate a member. MX has introduced new, more detailed information on the current status of a members connection to a financial institution and the state of its aggregation - the connection_status field. These are intended to replace and expand upon the information provided in the status field, which will soon be deprecated; support for the status field remains for the time being.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberStatusResponseBody</returns>
        public MemberStatusResponseBody ReadMemberStatus(string memberGuid, string userGuid)
        {
            ApiResponse<MemberStatusResponseBody> localVarResponse = ReadMemberStatusWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read member status This endpoint provides the status of the members most recent aggregation event. This is an important step in the aggregation process, and the results returned by this endpoint should determine what you do next in order to successfully aggregate a member. MX has introduced new, more detailed information on the current status of a members connection to a financial institution and the state of its aggregation - the connection_status field. These are intended to replace and expand upon the information provided in the status field, which will soon be deprecated; support for the status field remains for the time being.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberStatusResponseBody</returns>
        public ApiResponse<MemberStatusResponseBody> ReadMemberStatusWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadMemberStatus");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadMemberStatus");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MemberStatusResponseBody>("/users/{user_guid}/members/{member_guid}/status", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMemberStatus", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read member status This endpoint provides the status of the members most recent aggregation event. This is an important step in the aggregation process, and the results returned by this endpoint should determine what you do next in order to successfully aggregate a member. MX has introduced new, more detailed information on the current status of a members connection to a financial institution and the state of its aggregation - the connection_status field. These are intended to replace and expand upon the information provided in the status field, which will soon be deprecated; support for the status field remains for the time being.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberStatusResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberStatusResponseBody> ReadMemberStatusAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberStatusResponseBody> localVarResponse = await ReadMemberStatusWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read member status This endpoint provides the status of the members most recent aggregation event. This is an important step in the aggregation process, and the results returned by this endpoint should determine what you do next in order to successfully aggregate a member. MX has introduced new, more detailed information on the current status of a members connection to a financial institution and the state of its aggregation - the connection_status field. These are intended to replace and expand upon the information provided in the status field, which will soon be deprecated; support for the status field remains for the time being.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberStatusResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberStatusResponseBody>> ReadMemberStatusWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadMemberStatus");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadMemberStatus");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MemberStatusResponseBody>("/users/{user_guid}/members/{member_guid}/status", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMemberStatus", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read merchant Returns information about a particular merchant, such as a logo, name, and website.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantGuid">The unique id for a &#x60;merchant&#x60;.</param>
        /// <returns>MerchantResponseBody</returns>
        public MerchantResponseBody ReadMerchant(string merchantGuid)
        {
            ApiResponse<MerchantResponseBody> localVarResponse = ReadMerchantWithHttpInfo(merchantGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read merchant Returns information about a particular merchant, such as a logo, name, and website.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantGuid">The unique id for a &#x60;merchant&#x60;.</param>
        /// <returns>ApiResponse of MerchantResponseBody</returns>
        public ApiResponse<MerchantResponseBody> ReadMerchantWithHttpInfo(string merchantGuid)
        {
            // verify the required parameter 'merchantGuid' is set
            if (merchantGuid == null)
                throw new ApiException(400, "Missing required parameter 'merchantGuid' when calling MxPlatformApi->ReadMerchant");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("merchant_guid", ClientUtils.ParameterToString(merchantGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MerchantResponseBody>("/merchants/{merchant_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMerchant", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read merchant Returns information about a particular merchant, such as a logo, name, and website.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantGuid">The unique id for a &#x60;merchant&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MerchantResponseBody</returns>
        public async System.Threading.Tasks.Task<MerchantResponseBody> ReadMerchantAsync(string merchantGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MerchantResponseBody> localVarResponse = await ReadMerchantWithHttpInfoAsync(merchantGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read merchant Returns information about a particular merchant, such as a logo, name, and website.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantGuid">The unique id for a &#x60;merchant&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MerchantResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MerchantResponseBody>> ReadMerchantWithHttpInfoAsync(string merchantGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'merchantGuid' is set
            if (merchantGuid == null)
                throw new ApiException(400, "Missing required parameter 'merchantGuid' when calling MxPlatformApi->ReadMerchant");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("merchant_guid", ClientUtils.ParameterToString(merchantGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MerchantResponseBody>("/merchants/{merchant_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMerchant", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read merchant location This endpoint returns the specified merchant_location resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantLocationGuid">The unique id for a &#x60;merchant_location&#x60;.</param>
        /// <returns>MerchantLocationResponseBody</returns>
        public MerchantLocationResponseBody ReadMerchantLocation(string merchantLocationGuid)
        {
            ApiResponse<MerchantLocationResponseBody> localVarResponse = ReadMerchantLocationWithHttpInfo(merchantLocationGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read merchant location This endpoint returns the specified merchant_location resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantLocationGuid">The unique id for a &#x60;merchant_location&#x60;.</param>
        /// <returns>ApiResponse of MerchantLocationResponseBody</returns>
        public ApiResponse<MerchantLocationResponseBody> ReadMerchantLocationWithHttpInfo(string merchantLocationGuid)
        {
            // verify the required parameter 'merchantLocationGuid' is set
            if (merchantLocationGuid == null)
                throw new ApiException(400, "Missing required parameter 'merchantLocationGuid' when calling MxPlatformApi->ReadMerchantLocation");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("merchant_location_guid", ClientUtils.ParameterToString(merchantLocationGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<MerchantLocationResponseBody>("/merchant_locations/{merchant_location_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMerchantLocation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read merchant location This endpoint returns the specified merchant_location resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantLocationGuid">The unique id for a &#x60;merchant_location&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MerchantLocationResponseBody</returns>
        public async System.Threading.Tasks.Task<MerchantLocationResponseBody> ReadMerchantLocationAsync(string merchantLocationGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MerchantLocationResponseBody> localVarResponse = await ReadMerchantLocationWithHttpInfoAsync(merchantLocationGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read merchant location This endpoint returns the specified merchant_location resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="merchantLocationGuid">The unique id for a &#x60;merchant_location&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MerchantLocationResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MerchantLocationResponseBody>> ReadMerchantLocationWithHttpInfoAsync(string merchantLocationGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'merchantLocationGuid' is set
            if (merchantLocationGuid == null)
                throw new ApiException(400, "Missing required parameter 'merchantLocationGuid' when calling MxPlatformApi->ReadMerchantLocation");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("merchant_location_guid", ClientUtils.ParameterToString(merchantLocationGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<MerchantLocationResponseBody>("/merchant_locations/{merchant_location_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadMerchantLocation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read statement by member Use this endpoint to read a JSON representation of the statement.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>StatementResponseBody</returns>
        public StatementResponseBody ReadStatementByMember(string memberGuid, string statementGuid, string userGuid)
        {
            ApiResponse<StatementResponseBody> localVarResponse = ReadStatementByMemberWithHttpInfo(memberGuid, statementGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read statement by member Use this endpoint to read a JSON representation of the statement.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of StatementResponseBody</returns>
        public ApiResponse<StatementResponseBody> ReadStatementByMemberWithHttpInfo(string memberGuid, string statementGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadStatementByMember");

            // verify the required parameter 'statementGuid' is set
            if (statementGuid == null)
                throw new ApiException(400, "Missing required parameter 'statementGuid' when calling MxPlatformApi->ReadStatementByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadStatementByMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("statement_guid", ClientUtils.ParameterToString(statementGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<StatementResponseBody>("/users/{user_guid}/members/{member_guid}/statements/{statement_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadStatementByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read statement by member Use this endpoint to read a JSON representation of the statement.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of StatementResponseBody</returns>
        public async System.Threading.Tasks.Task<StatementResponseBody> ReadStatementByMemberAsync(string memberGuid, string statementGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<StatementResponseBody> localVarResponse = await ReadStatementByMemberWithHttpInfoAsync(memberGuid, statementGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read statement by member Use this endpoint to read a JSON representation of the statement.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="statementGuid">The unique id for a &#x60;statement&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (StatementResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<StatementResponseBody>> ReadStatementByMemberWithHttpInfoAsync(string memberGuid, string statementGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ReadStatementByMember");

            // verify the required parameter 'statementGuid' is set
            if (statementGuid == null)
                throw new ApiException(400, "Missing required parameter 'statementGuid' when calling MxPlatformApi->ReadStatementByMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadStatementByMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("statement_guid", ClientUtils.ParameterToString(statementGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<StatementResponseBody>("/users/{user_guid}/members/{member_guid}/statements/{statement_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadStatementByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read tag Use this endpoint to read the attributes of a particular tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>TagResponseBody</returns>
        public TagResponseBody ReadTag(string tagGuid, string userGuid)
        {
            ApiResponse<TagResponseBody> localVarResponse = ReadTagWithHttpInfo(tagGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read tag Use this endpoint to read the attributes of a particular tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of TagResponseBody</returns>
        public ApiResponse<TagResponseBody> ReadTagWithHttpInfo(string tagGuid, string userGuid)
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->ReadTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTag");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TagResponseBody>("/users/{user_guid}/tags/{tag_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read tag Use this endpoint to read the attributes of a particular tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TagResponseBody</returns>
        public async System.Threading.Tasks.Task<TagResponseBody> ReadTagAsync(string tagGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TagResponseBody> localVarResponse = await ReadTagWithHttpInfoAsync(tagGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read tag Use this endpoint to read the attributes of a particular tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TagResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TagResponseBody>> ReadTagWithHttpInfoAsync(string tagGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->ReadTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTag");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TagResponseBody>("/users/{user_guid}/tags/{tag_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read tagging Use this endpoint to read the attributes of a &#x60;tagging&#x60; according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>TaggingResponseBody</returns>
        public TaggingResponseBody ReadTagging(string taggingGuid, string userGuid)
        {
            ApiResponse<TaggingResponseBody> localVarResponse = ReadTaggingWithHttpInfo(taggingGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read tagging Use this endpoint to read the attributes of a &#x60;tagging&#x60; according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of TaggingResponseBody</returns>
        public ApiResponse<TaggingResponseBody> ReadTaggingWithHttpInfo(string taggingGuid, string userGuid)
        {
            // verify the required parameter 'taggingGuid' is set
            if (taggingGuid == null)
                throw new ApiException(400, "Missing required parameter 'taggingGuid' when calling MxPlatformApi->ReadTagging");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTagging");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tagging_guid", ClientUtils.ParameterToString(taggingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TaggingResponseBody>("/users/{user_guid}/taggings/{tagging_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read tagging Use this endpoint to read the attributes of a &#x60;tagging&#x60; according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TaggingResponseBody</returns>
        public async System.Threading.Tasks.Task<TaggingResponseBody> ReadTaggingAsync(string taggingGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TaggingResponseBody> localVarResponse = await ReadTaggingWithHttpInfoAsync(taggingGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read tagging Use this endpoint to read the attributes of a &#x60;tagging&#x60; according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TaggingResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TaggingResponseBody>> ReadTaggingWithHttpInfoAsync(string taggingGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'taggingGuid' is set
            if (taggingGuid == null)
                throw new ApiException(400, "Missing required parameter 'taggingGuid' when calling MxPlatformApi->ReadTagging");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTagging");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tagging_guid", ClientUtils.ParameterToString(taggingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TaggingResponseBody>("/users/{user_guid}/taggings/{tagging_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read transaction Requests to this endpoint will return the attributes of the specified &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>TransactionResponseBody</returns>
        public TransactionResponseBody ReadTransaction(string transactionGuid, string userGuid)
        {
            ApiResponse<TransactionResponseBody> localVarResponse = ReadTransactionWithHttpInfo(transactionGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read transaction Requests to this endpoint will return the attributes of the specified &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of TransactionResponseBody</returns>
        public ApiResponse<TransactionResponseBody> ReadTransactionWithHttpInfo(string transactionGuid, string userGuid)
        {
            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->ReadTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTransaction");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionResponseBody>("/users/{user_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read transaction Requests to this endpoint will return the attributes of the specified &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionResponseBody> ReadTransactionAsync(string transactionGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionResponseBody> localVarResponse = await ReadTransactionWithHttpInfoAsync(transactionGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read transaction Requests to this endpoint will return the attributes of the specified &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionResponseBody>> ReadTransactionWithHttpInfoAsync(string transactionGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->ReadTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTransaction");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionResponseBody>("/users/{user_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read transaction rule Use this endpoint to read the attributes of an existing transaction rule based on the rule’s unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>TransactionRuleResponseBody</returns>
        public TransactionRuleResponseBody ReadTransactionRule(string transactionRuleGuid, string userGuid)
        {
            ApiResponse<TransactionRuleResponseBody> localVarResponse = ReadTransactionRuleWithHttpInfo(transactionRuleGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read transaction rule Use this endpoint to read the attributes of an existing transaction rule based on the rule’s unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of TransactionRuleResponseBody</returns>
        public ApiResponse<TransactionRuleResponseBody> ReadTransactionRuleWithHttpInfo(string transactionRuleGuid, string userGuid)
        {
            // verify the required parameter 'transactionRuleGuid' is set
            if (transactionRuleGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleGuid' when calling MxPlatformApi->ReadTransactionRule");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTransactionRule");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_rule_guid", ClientUtils.ParameterToString(transactionRuleGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<TransactionRuleResponseBody>("/users/{user_guid}/transaction_rules/{transaction_rule_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read transaction rule Use this endpoint to read the attributes of an existing transaction rule based on the rule’s unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionRuleResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionRuleResponseBody> ReadTransactionRuleAsync(string transactionRuleGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionRuleResponseBody> localVarResponse = await ReadTransactionRuleWithHttpInfoAsync(transactionRuleGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read transaction rule Use this endpoint to read the attributes of an existing transaction rule based on the rule’s unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionRuleResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionRuleResponseBody>> ReadTransactionRuleWithHttpInfoAsync(string transactionRuleGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'transactionRuleGuid' is set
            if (transactionRuleGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleGuid' when calling MxPlatformApi->ReadTransactionRule");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadTransactionRule");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_rule_guid", ClientUtils.ParameterToString(transactionRuleGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<TransactionRuleResponseBody>("/users/{user_guid}/transaction_rules/{transaction_rule_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read user Use this endpoint to read the attributes of a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>UserResponseBody</returns>
        public UserResponseBody ReadUser(string userGuid)
        {
            ApiResponse<UserResponseBody> localVarResponse = ReadUserWithHttpInfo(userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read user Use this endpoint to read the attributes of a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of UserResponseBody</returns>
        public ApiResponse<UserResponseBody> ReadUserWithHttpInfo(string userGuid)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadUser");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<UserResponseBody>("/users/{user_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read user Use this endpoint to read the attributes of a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of UserResponseBody</returns>
        public async System.Threading.Tasks.Task<UserResponseBody> ReadUserAsync(string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<UserResponseBody> localVarResponse = await ReadUserWithHttpInfoAsync(userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read user Use this endpoint to read the attributes of a specific user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (UserResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<UserResponseBody>> ReadUserWithHttpInfoAsync(string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ReadUser");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<UserResponseBody>("/users/{user_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Request connect widget url This endpoint will return a URL for an embeddable version of MX Connect.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="connectWidgetRequestBody">Optional config options for WebView (is_mobile_webview, current_institution_code, current_member_guid, update_credentials) (optional)</param>
        /// <returns>ConnectWidgetResponseBody</returns>
        public ConnectWidgetResponseBody RequestConnectWidgetURL(string userGuid, ConnectWidgetRequestBody connectWidgetRequestBody = default(ConnectWidgetRequestBody))
        {
            ApiResponse<ConnectWidgetResponseBody> localVarResponse = RequestConnectWidgetURLWithHttpInfo(userGuid, connectWidgetRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Request connect widget url This endpoint will return a URL for an embeddable version of MX Connect.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="connectWidgetRequestBody">Optional config options for WebView (is_mobile_webview, current_institution_code, current_member_guid, update_credentials) (optional)</param>
        /// <returns>ApiResponse of ConnectWidgetResponseBody</returns>
        public ApiResponse<ConnectWidgetResponseBody> RequestConnectWidgetURLWithHttpInfo(string userGuid, ConnectWidgetRequestBody connectWidgetRequestBody = default(ConnectWidgetRequestBody))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->RequestConnectWidgetURL");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = connectWidgetRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<ConnectWidgetResponseBody>("/users/{user_guid}/connect_widget_url", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RequestConnectWidgetURL", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Request connect widget url This endpoint will return a URL for an embeddable version of MX Connect.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="connectWidgetRequestBody">Optional config options for WebView (is_mobile_webview, current_institution_code, current_member_guid, update_credentials) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ConnectWidgetResponseBody</returns>
        public async System.Threading.Tasks.Task<ConnectWidgetResponseBody> RequestConnectWidgetURLAsync(string userGuid, ConnectWidgetRequestBody connectWidgetRequestBody = default(ConnectWidgetRequestBody), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<ConnectWidgetResponseBody> localVarResponse = await RequestConnectWidgetURLWithHttpInfoAsync(userGuid, connectWidgetRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Request connect widget url This endpoint will return a URL for an embeddable version of MX Connect.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="connectWidgetRequestBody">Optional config options for WebView (is_mobile_webview, current_institution_code, current_member_guid, update_credentials) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ConnectWidgetResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ConnectWidgetResponseBody>> RequestConnectWidgetURLWithHttpInfoAsync(string userGuid, ConnectWidgetRequestBody connectWidgetRequestBody = default(ConnectWidgetRequestBody), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->RequestConnectWidgetURL");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = connectWidgetRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<ConnectWidgetResponseBody>("/users/{user_guid}/connect_widget_url", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RequestConnectWidgetURL", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Request oauth window uri This endpoint will generate an &#x60;oauth_window_uri&#x60; for the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="referralSource">Must be either &#x60;BROWSER&#x60; or &#x60;APP&#x60; depending on the implementation. Defaults to &#x60;BROWSER&#x60;. (optional)</param>
        /// <param name="uiMessageWebviewUrlScheme">A scheme for routing the user back to the application state they were previously in. (optional)</param>
        /// <param name="skipAggregation">Setting this parameter to &#x60;true&#x60; will prevent the member from automatically aggregating after being redirected from the authorization page. (optional)</param>
        /// <returns>OAuthWindowResponseBody</returns>
        public OAuthWindowResponseBody RequestOAuthWindowURI(string memberGuid, string userGuid, string referralSource = default(string), string uiMessageWebviewUrlScheme = default(string), bool? skipAggregation = default(bool?))
        {
            ApiResponse<OAuthWindowResponseBody> localVarResponse = RequestOAuthWindowURIWithHttpInfo(memberGuid, userGuid, referralSource, uiMessageWebviewUrlScheme, skipAggregation);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Request oauth window uri This endpoint will generate an &#x60;oauth_window_uri&#x60; for the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="referralSource">Must be either &#x60;BROWSER&#x60; or &#x60;APP&#x60; depending on the implementation. Defaults to &#x60;BROWSER&#x60;. (optional)</param>
        /// <param name="uiMessageWebviewUrlScheme">A scheme for routing the user back to the application state they were previously in. (optional)</param>
        /// <param name="skipAggregation">Setting this parameter to &#x60;true&#x60; will prevent the member from automatically aggregating after being redirected from the authorization page. (optional)</param>
        /// <returns>ApiResponse of OAuthWindowResponseBody</returns>
        public ApiResponse<OAuthWindowResponseBody> RequestOAuthWindowURIWithHttpInfo(string memberGuid, string userGuid, string referralSource = default(string), string uiMessageWebviewUrlScheme = default(string), bool? skipAggregation = default(bool?))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->RequestOAuthWindowURI");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->RequestOAuthWindowURI");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (referralSource != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "referral_source", referralSource));
            }
            if (uiMessageWebviewUrlScheme != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "ui_message_webview_url_scheme", uiMessageWebviewUrlScheme));
            }
            if (skipAggregation != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "skip_aggregation", skipAggregation));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<OAuthWindowResponseBody>("/users/{user_guid}/members/{member_guid}/oauth_window_uri", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RequestOAuthWindowURI", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Request oauth window uri This endpoint will generate an &#x60;oauth_window_uri&#x60; for the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="referralSource">Must be either &#x60;BROWSER&#x60; or &#x60;APP&#x60; depending on the implementation. Defaults to &#x60;BROWSER&#x60;. (optional)</param>
        /// <param name="uiMessageWebviewUrlScheme">A scheme for routing the user back to the application state they were previously in. (optional)</param>
        /// <param name="skipAggregation">Setting this parameter to &#x60;true&#x60; will prevent the member from automatically aggregating after being redirected from the authorization page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of OAuthWindowResponseBody</returns>
        public async System.Threading.Tasks.Task<OAuthWindowResponseBody> RequestOAuthWindowURIAsync(string memberGuid, string userGuid, string referralSource = default(string), string uiMessageWebviewUrlScheme = default(string), bool? skipAggregation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<OAuthWindowResponseBody> localVarResponse = await RequestOAuthWindowURIWithHttpInfoAsync(memberGuid, userGuid, referralSource, uiMessageWebviewUrlScheme, skipAggregation, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Request oauth window uri This endpoint will generate an &#x60;oauth_window_uri&#x60; for the specified &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="referralSource">Must be either &#x60;BROWSER&#x60; or &#x60;APP&#x60; depending on the implementation. Defaults to &#x60;BROWSER&#x60;. (optional)</param>
        /// <param name="uiMessageWebviewUrlScheme">A scheme for routing the user back to the application state they were previously in. (optional)</param>
        /// <param name="skipAggregation">Setting this parameter to &#x60;true&#x60; will prevent the member from automatically aggregating after being redirected from the authorization page. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (OAuthWindowResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<OAuthWindowResponseBody>> RequestOAuthWindowURIWithHttpInfoAsync(string memberGuid, string userGuid, string referralSource = default(string), string uiMessageWebviewUrlScheme = default(string), bool? skipAggregation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->RequestOAuthWindowURI");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->RequestOAuthWindowURI");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (referralSource != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "referral_source", referralSource));
            }
            if (uiMessageWebviewUrlScheme != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "ui_message_webview_url_scheme", uiMessageWebviewUrlScheme));
            }
            if (skipAggregation != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "skip_aggregation", skipAggregation));
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<OAuthWindowResponseBody>("/users/{user_guid}/members/{member_guid}/oauth_window_uri", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RequestOAuthWindowURI", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Request widget url This endpoint allows partners to get a URL by passing the &#x60;widget_type&#x60; in the request body, as well as configuring it in several different ways. In the case of Connect, that means setting the &#x60;widget_type&#x60; to &#x60;connect_widget&#x60;. Partners may also pass an optional &#x60;Accept-Language&#x60; header as well as a number of configuration options. Note that this is a &#x60;POST&#x60; request.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="widgetRequestBody">The widget url configuration options.</param>
        /// <param name="acceptLanguage">The desired language of the widget. (optional)</param>
        /// <returns>WidgetResponseBody</returns>
        public WidgetResponseBody RequestWidgetURL(string userGuid, WidgetRequestBody widgetRequestBody, string acceptLanguage = default(string))
        {
            ApiResponse<WidgetResponseBody> localVarResponse = RequestWidgetURLWithHttpInfo(userGuid, widgetRequestBody, acceptLanguage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Request widget url This endpoint allows partners to get a URL by passing the &#x60;widget_type&#x60; in the request body, as well as configuring it in several different ways. In the case of Connect, that means setting the &#x60;widget_type&#x60; to &#x60;connect_widget&#x60;. Partners may also pass an optional &#x60;Accept-Language&#x60; header as well as a number of configuration options. Note that this is a &#x60;POST&#x60; request.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="widgetRequestBody">The widget url configuration options.</param>
        /// <param name="acceptLanguage">The desired language of the widget. (optional)</param>
        /// <returns>ApiResponse of WidgetResponseBody</returns>
        public ApiResponse<WidgetResponseBody> RequestWidgetURLWithHttpInfo(string userGuid, WidgetRequestBody widgetRequestBody, string acceptLanguage = default(string))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->RequestWidgetURL");

            // verify the required parameter 'widgetRequestBody' is set
            if (widgetRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'widgetRequestBody' when calling MxPlatformApi->RequestWidgetURL");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }
            localVarRequestOptions.Data = widgetRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<WidgetResponseBody>("/users/{user_guid}/widget_urls", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RequestWidgetURL", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Request widget url This endpoint allows partners to get a URL by passing the &#x60;widget_type&#x60; in the request body, as well as configuring it in several different ways. In the case of Connect, that means setting the &#x60;widget_type&#x60; to &#x60;connect_widget&#x60;. Partners may also pass an optional &#x60;Accept-Language&#x60; header as well as a number of configuration options. Note that this is a &#x60;POST&#x60; request.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="widgetRequestBody">The widget url configuration options.</param>
        /// <param name="acceptLanguage">The desired language of the widget. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of WidgetResponseBody</returns>
        public async System.Threading.Tasks.Task<WidgetResponseBody> RequestWidgetURLAsync(string userGuid, WidgetRequestBody widgetRequestBody, string acceptLanguage = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<WidgetResponseBody> localVarResponse = await RequestWidgetURLWithHttpInfoAsync(userGuid, widgetRequestBody, acceptLanguage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Request widget url This endpoint allows partners to get a URL by passing the &#x60;widget_type&#x60; in the request body, as well as configuring it in several different ways. In the case of Connect, that means setting the &#x60;widget_type&#x60; to &#x60;connect_widget&#x60;. Partners may also pass an optional &#x60;Accept-Language&#x60; header as well as a number of configuration options. Note that this is a &#x60;POST&#x60; request.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="widgetRequestBody">The widget url configuration options.</param>
        /// <param name="acceptLanguage">The desired language of the widget. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (WidgetResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<WidgetResponseBody>> RequestWidgetURLWithHttpInfoAsync(string userGuid, WidgetRequestBody widgetRequestBody, string acceptLanguage = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->RequestWidgetURL");

            // verify the required parameter 'widgetRequestBody' is set
            if (widgetRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'widgetRequestBody' when calling MxPlatformApi->RequestWidgetURL");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }
            localVarRequestOptions.Data = widgetRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<WidgetResponseBody>("/users/{user_guid}/widget_urls", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RequestWidgetURL", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Resume aggregation This endpoint answers the challenges needed when a member has been challenged by multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberResumeRequestBody">Member object with MFA challenge answers</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody ResumeAggregation(string memberGuid, string userGuid, MemberResumeRequestBody memberResumeRequestBody)
        {
            ApiResponse<MemberResponseBody> localVarResponse = ResumeAggregationWithHttpInfo(memberGuid, userGuid, memberResumeRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Resume aggregation This endpoint answers the challenges needed when a member has been challenged by multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberResumeRequestBody">Member object with MFA challenge answers</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> ResumeAggregationWithHttpInfo(string memberGuid, string userGuid, MemberResumeRequestBody memberResumeRequestBody)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ResumeAggregation");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ResumeAggregation");

            // verify the required parameter 'memberResumeRequestBody' is set
            if (memberResumeRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'memberResumeRequestBody' when calling MxPlatformApi->ResumeAggregation");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = memberResumeRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/resume", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ResumeAggregation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Resume aggregation This endpoint answers the challenges needed when a member has been challenged by multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberResumeRequestBody">Member object with MFA challenge answers</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> ResumeAggregationAsync(string memberGuid, string userGuid, MemberResumeRequestBody memberResumeRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await ResumeAggregationWithHttpInfoAsync(memberGuid, userGuid, memberResumeRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Resume aggregation This endpoint answers the challenges needed when a member has been challenged by multi-factor authentication.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberResumeRequestBody">Member object with MFA challenge answers</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> ResumeAggregationWithHttpInfoAsync(string memberGuid, string userGuid, MemberResumeRequestBody memberResumeRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->ResumeAggregation");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->ResumeAggregation");

            // verify the required parameter 'memberResumeRequestBody' is set
            if (memberResumeRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'memberResumeRequestBody' when calling MxPlatformApi->ResumeAggregation");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = memberResumeRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/resume", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ResumeAggregation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update account by member This endpoint allows you to update certain attributes of an &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="accountUpdateRequestBody">Account object to be created with optional parameters (is_hidden)</param>
        /// <returns>AccountResponseBody</returns>
        public AccountResponseBody UpdateAccountByMember(string userGuid, string memberGuid, string accountGuid, AccountUpdateRequestBody accountUpdateRequestBody)
        {
            ApiResponse<AccountResponseBody> localVarResponse = UpdateAccountByMemberWithHttpInfo(userGuid, memberGuid, accountGuid, accountUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update account by member This endpoint allows you to update certain attributes of an &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="accountUpdateRequestBody">Account object to be created with optional parameters (is_hidden)</param>
        /// <returns>ApiResponse of AccountResponseBody</returns>
        public ApiResponse<AccountResponseBody> UpdateAccountByMemberWithHttpInfo(string userGuid, string memberGuid, string accountGuid, AccountUpdateRequestBody accountUpdateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateAccountByMember");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateAccountByMember");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->UpdateAccountByMember");

            // verify the required parameter 'accountUpdateRequestBody' is set
            if (accountUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'accountUpdateRequestBody' when calling MxPlatformApi->UpdateAccountByMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.Data = accountUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<AccountResponseBody>("/users/{user_guid}/members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAccountByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update account by member This endpoint allows you to update certain attributes of an &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="accountUpdateRequestBody">Account object to be created with optional parameters (is_hidden)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountResponseBody> UpdateAccountByMemberAsync(string userGuid, string memberGuid, string accountGuid, AccountUpdateRequestBody accountUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountResponseBody> localVarResponse = await UpdateAccountByMemberWithHttpInfoAsync(userGuid, memberGuid, accountGuid, accountUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update account by member This endpoint allows you to update certain attributes of an &#x60;account&#x60; resource.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="accountUpdateRequestBody">Account object to be created with optional parameters (is_hidden)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountResponseBody>> UpdateAccountByMemberWithHttpInfoAsync(string userGuid, string memberGuid, string accountGuid, AccountUpdateRequestBody accountUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateAccountByMember");

            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateAccountByMember");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->UpdateAccountByMember");

            // verify the required parameter 'accountUpdateRequestBody' is set
            if (accountUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'accountUpdateRequestBody' when calling MxPlatformApi->UpdateAccountByMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.Data = accountUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<AccountResponseBody>("/users/{user_guid}/members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAccountByMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update category Use this endpoint to update the attributes of a custom category according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryUpdateRequestBody">Category object to be updated (While no single parameter is required, the &#x60;category&#x60; object cannot be empty)</param>
        /// <returns>CategoryResponseBody</returns>
        public CategoryResponseBody UpdateCategory(string categoryGuid, string userGuid, CategoryUpdateRequestBody categoryUpdateRequestBody)
        {
            ApiResponse<CategoryResponseBody> localVarResponse = UpdateCategoryWithHttpInfo(categoryGuid, userGuid, categoryUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update category Use this endpoint to update the attributes of a custom category according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryUpdateRequestBody">Category object to be updated (While no single parameter is required, the &#x60;category&#x60; object cannot be empty)</param>
        /// <returns>ApiResponse of CategoryResponseBody</returns>
        public ApiResponse<CategoryResponseBody> UpdateCategoryWithHttpInfo(string categoryGuid, string userGuid, CategoryUpdateRequestBody categoryUpdateRequestBody)
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->UpdateCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateCategory");

            // verify the required parameter 'categoryUpdateRequestBody' is set
            if (categoryUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'categoryUpdateRequestBody' when calling MxPlatformApi->UpdateCategory");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = categoryUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<CategoryResponseBody>("/users/{user_guid}/categories/{category_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update category Use this endpoint to update the attributes of a custom category according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryUpdateRequestBody">Category object to be updated (While no single parameter is required, the &#x60;category&#x60; object cannot be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CategoryResponseBody</returns>
        public async System.Threading.Tasks.Task<CategoryResponseBody> UpdateCategoryAsync(string categoryGuid, string userGuid, CategoryUpdateRequestBody categoryUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<CategoryResponseBody> localVarResponse = await UpdateCategoryWithHttpInfoAsync(categoryGuid, userGuid, categoryUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update category Use this endpoint to update the attributes of a custom category according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="categoryGuid">The unique id for a &#x60;category&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="categoryUpdateRequestBody">Category object to be updated (While no single parameter is required, the &#x60;category&#x60; object cannot be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CategoryResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<CategoryResponseBody>> UpdateCategoryWithHttpInfoAsync(string categoryGuid, string userGuid, CategoryUpdateRequestBody categoryUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'categoryGuid' is set
            if (categoryGuid == null)
                throw new ApiException(400, "Missing required parameter 'categoryGuid' when calling MxPlatformApi->UpdateCategory");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateCategory");

            // verify the required parameter 'categoryUpdateRequestBody' is set
            if (categoryUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'categoryUpdateRequestBody' when calling MxPlatformApi->UpdateCategory");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("category_guid", ClientUtils.ParameterToString(categoryGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = categoryUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<CategoryResponseBody>("/users/{user_guid}/categories/{category_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateCategory", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update managed account Use this endpoint to update the attributes of a partner-managed account according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="managedAccountUpdateRequestBody">Managed account object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>AccountResponseBody</returns>
        public AccountResponseBody UpdateManagedAccount(string memberGuid, string userGuid, string accountGuid, ManagedAccountUpdateRequestBody managedAccountUpdateRequestBody)
        {
            ApiResponse<AccountResponseBody> localVarResponse = UpdateManagedAccountWithHttpInfo(memberGuid, userGuid, accountGuid, managedAccountUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update managed account Use this endpoint to update the attributes of a partner-managed account according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="managedAccountUpdateRequestBody">Managed account object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>ApiResponse of AccountResponseBody</returns>
        public ApiResponse<AccountResponseBody> UpdateManagedAccountWithHttpInfo(string memberGuid, string userGuid, string accountGuid, ManagedAccountUpdateRequestBody managedAccountUpdateRequestBody)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateManagedAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateManagedAccount");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->UpdateManagedAccount");

            // verify the required parameter 'managedAccountUpdateRequestBody' is set
            if (managedAccountUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedAccountUpdateRequestBody' when calling MxPlatformApi->UpdateManagedAccount");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.Data = managedAccountUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<AccountResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update managed account Use this endpoint to update the attributes of a partner-managed account according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="managedAccountUpdateRequestBody">Managed account object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AccountResponseBody</returns>
        public async System.Threading.Tasks.Task<AccountResponseBody> UpdateManagedAccountAsync(string memberGuid, string userGuid, string accountGuid, ManagedAccountUpdateRequestBody managedAccountUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<AccountResponseBody> localVarResponse = await UpdateManagedAccountWithHttpInfoAsync(memberGuid, userGuid, accountGuid, managedAccountUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update managed account Use this endpoint to update the attributes of a partner-managed account according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="accountGuid">The unique id for an &#x60;account&#x60;.</param>
        /// <param name="managedAccountUpdateRequestBody">Managed account object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AccountResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AccountResponseBody>> UpdateManagedAccountWithHttpInfoAsync(string memberGuid, string userGuid, string accountGuid, ManagedAccountUpdateRequestBody managedAccountUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateManagedAccount");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateManagedAccount");

            // verify the required parameter 'accountGuid' is set
            if (accountGuid == null)
                throw new ApiException(400, "Missing required parameter 'accountGuid' when calling MxPlatformApi->UpdateManagedAccount");

            // verify the required parameter 'managedAccountUpdateRequestBody' is set
            if (managedAccountUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedAccountUpdateRequestBody' when calling MxPlatformApi->UpdateManagedAccount");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("account_guid", ClientUtils.ParameterToString(accountGuid)); // path parameter
            localVarRequestOptions.Data = managedAccountUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<AccountResponseBody>("/users/{user_guid}/managed_members/{member_guid}/accounts/{account_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateManagedAccount", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update managed member Use this endpoint to update the attributes of the specified partner_managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberUpdateRequestBody">Managed member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody UpdateManagedMember(string memberGuid, string userGuid, ManagedMemberUpdateRequestBody managedMemberUpdateRequestBody)
        {
            ApiResponse<MemberResponseBody> localVarResponse = UpdateManagedMemberWithHttpInfo(memberGuid, userGuid, managedMemberUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update managed member Use this endpoint to update the attributes of the specified partner_managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberUpdateRequestBody">Managed member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> UpdateManagedMemberWithHttpInfo(string memberGuid, string userGuid, ManagedMemberUpdateRequestBody managedMemberUpdateRequestBody)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateManagedMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateManagedMember");

            // verify the required parameter 'managedMemberUpdateRequestBody' is set
            if (managedMemberUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedMemberUpdateRequestBody' when calling MxPlatformApi->UpdateManagedMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = managedMemberUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<MemberResponseBody>("/users/{user_guid}/managed_members/{member_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update managed member Use this endpoint to update the attributes of the specified partner_managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberUpdateRequestBody">Managed member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> UpdateManagedMemberAsync(string memberGuid, string userGuid, ManagedMemberUpdateRequestBody managedMemberUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await UpdateManagedMemberWithHttpInfoAsync(memberGuid, userGuid, managedMemberUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update managed member Use this endpoint to update the attributes of the specified partner_managed &#x60;member&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="managedMemberUpdateRequestBody">Managed member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> UpdateManagedMemberWithHttpInfoAsync(string memberGuid, string userGuid, ManagedMemberUpdateRequestBody managedMemberUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateManagedMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateManagedMember");

            // verify the required parameter 'managedMemberUpdateRequestBody' is set
            if (managedMemberUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedMemberUpdateRequestBody' when calling MxPlatformApi->UpdateManagedMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = managedMemberUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<MemberResponseBody>("/users/{user_guid}/managed_members/{member_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateManagedMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update managed transaction Use this endpoint to update the attributes of the specified partner_managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="managedTransactionUpdateRequestBody">Managed transaction object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>TransactionResponseBody</returns>
        public TransactionResponseBody UpdateManagedTransaction(string memberGuid, string userGuid, string transactionGuid, ManagedTransactionUpdateRequestBody managedTransactionUpdateRequestBody)
        {
            ApiResponse<TransactionResponseBody> localVarResponse = UpdateManagedTransactionWithHttpInfo(memberGuid, userGuid, transactionGuid, managedTransactionUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update managed transaction Use this endpoint to update the attributes of the specified partner_managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="managedTransactionUpdateRequestBody">Managed transaction object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>ApiResponse of TransactionResponseBody</returns>
        public ApiResponse<TransactionResponseBody> UpdateManagedTransactionWithHttpInfo(string memberGuid, string userGuid, string transactionGuid, ManagedTransactionUpdateRequestBody managedTransactionUpdateRequestBody)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateManagedTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateManagedTransaction");

            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->UpdateManagedTransaction");

            // verify the required parameter 'managedTransactionUpdateRequestBody' is set
            if (managedTransactionUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedTransactionUpdateRequestBody' when calling MxPlatformApi->UpdateManagedTransaction");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter
            localVarRequestOptions.Data = managedTransactionUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<TransactionResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update managed transaction Use this endpoint to update the attributes of the specified partner_managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="managedTransactionUpdateRequestBody">Managed transaction object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionResponseBody> UpdateManagedTransactionAsync(string memberGuid, string userGuid, string transactionGuid, ManagedTransactionUpdateRequestBody managedTransactionUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionResponseBody> localVarResponse = await UpdateManagedTransactionWithHttpInfoAsync(memberGuid, userGuid, transactionGuid, managedTransactionUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update managed transaction Use this endpoint to update the attributes of the specified partner_managed &#x60;transaction&#x60;.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="managedTransactionUpdateRequestBody">Managed transaction object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionResponseBody>> UpdateManagedTransactionWithHttpInfoAsync(string memberGuid, string userGuid, string transactionGuid, ManagedTransactionUpdateRequestBody managedTransactionUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateManagedTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateManagedTransaction");

            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->UpdateManagedTransaction");

            // verify the required parameter 'managedTransactionUpdateRequestBody' is set
            if (managedTransactionUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'managedTransactionUpdateRequestBody' when calling MxPlatformApi->UpdateManagedTransaction");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter
            localVarRequestOptions.Data = managedTransactionUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<TransactionResponseBody>("/users/{user_guid}/managed_members/{member_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateManagedTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update member Use this endpoint to update a members attributes. Only the credentials, id, and metadata parameters can be updated. To get a list of the required credentials for the member, use the list member credentials endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberUpdateRequestBody">Member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody UpdateMember(string memberGuid, string userGuid, MemberUpdateRequestBody memberUpdateRequestBody)
        {
            ApiResponse<MemberResponseBody> localVarResponse = UpdateMemberWithHttpInfo(memberGuid, userGuid, memberUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update member Use this endpoint to update a members attributes. Only the credentials, id, and metadata parameters can be updated. To get a list of the required credentials for the member, use the list member credentials endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberUpdateRequestBody">Member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> UpdateMemberWithHttpInfo(string memberGuid, string userGuid, MemberUpdateRequestBody memberUpdateRequestBody)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateMember");

            // verify the required parameter 'memberUpdateRequestBody' is set
            if (memberUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'memberUpdateRequestBody' when calling MxPlatformApi->UpdateMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = memberUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<MemberResponseBody>("/users/{user_guid}/members/{member_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update member Use this endpoint to update a members attributes. Only the credentials, id, and metadata parameters can be updated. To get a list of the required credentials for the member, use the list member credentials endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberUpdateRequestBody">Member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> UpdateMemberAsync(string memberGuid, string userGuid, MemberUpdateRequestBody memberUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await UpdateMemberWithHttpInfoAsync(memberGuid, userGuid, memberUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update member Use this endpoint to update a members attributes. Only the credentials, id, and metadata parameters can be updated. To get a list of the required credentials for the member, use the list member credentials endpoint.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="memberUpdateRequestBody">Member object to be updated (While no single parameter is required, the request body can&#39;t be empty)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> UpdateMemberWithHttpInfoAsync(string memberGuid, string userGuid, MemberUpdateRequestBody memberUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->UpdateMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateMember");

            // verify the required parameter 'memberUpdateRequestBody' is set
            if (memberUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'memberUpdateRequestBody' when calling MxPlatformApi->UpdateMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = memberUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update tag Use this endpoint to update the name of a specific tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagUpdateRequestBody">Tag object to be updated with required parameter (tag_guid)</param>
        /// <returns>TagResponseBody</returns>
        public TagResponseBody UpdateTag(string tagGuid, string userGuid, TagUpdateRequestBody tagUpdateRequestBody)
        {
            ApiResponse<TagResponseBody> localVarResponse = UpdateTagWithHttpInfo(tagGuid, userGuid, tagUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update tag Use this endpoint to update the name of a specific tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagUpdateRequestBody">Tag object to be updated with required parameter (tag_guid)</param>
        /// <returns>ApiResponse of TagResponseBody</returns>
        public ApiResponse<TagResponseBody> UpdateTagWithHttpInfo(string tagGuid, string userGuid, TagUpdateRequestBody tagUpdateRequestBody)
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->UpdateTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTag");

            // verify the required parameter 'tagUpdateRequestBody' is set
            if (tagUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'tagUpdateRequestBody' when calling MxPlatformApi->UpdateTag");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = tagUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<TagResponseBody>("/users/{user_guid}/tags/{tag_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update tag Use this endpoint to update the name of a specific tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagUpdateRequestBody">Tag object to be updated with required parameter (tag_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TagResponseBody</returns>
        public async System.Threading.Tasks.Task<TagResponseBody> UpdateTagAsync(string tagGuid, string userGuid, TagUpdateRequestBody tagUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TagResponseBody> localVarResponse = await UpdateTagWithHttpInfoAsync(tagGuid, userGuid, tagUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update tag Use this endpoint to update the name of a specific tag according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="tagGuid">The unique id for a &#x60;tag&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="tagUpdateRequestBody">Tag object to be updated with required parameter (tag_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TagResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TagResponseBody>> UpdateTagWithHttpInfoAsync(string tagGuid, string userGuid, TagUpdateRequestBody tagUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'tagGuid' is set
            if (tagGuid == null)
                throw new ApiException(400, "Missing required parameter 'tagGuid' when calling MxPlatformApi->UpdateTag");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTag");

            // verify the required parameter 'tagUpdateRequestBody' is set
            if (tagUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'tagUpdateRequestBody' when calling MxPlatformApi->UpdateTag");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tag_guid", ClientUtils.ParameterToString(tagGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = tagUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<TagResponseBody>("/users/{user_guid}/tags/{tag_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTag", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update tagging Use this endpoint to update a tagging.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingUpdateRequestBody">Tagging object to be updated with required parameter (tag_guid)</param>
        /// <returns>TaggingResponseBody</returns>
        public TaggingResponseBody UpdateTagging(string taggingGuid, string userGuid, TaggingUpdateRequestBody taggingUpdateRequestBody)
        {
            ApiResponse<TaggingResponseBody> localVarResponse = UpdateTaggingWithHttpInfo(taggingGuid, userGuid, taggingUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update tagging Use this endpoint to update a tagging.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingUpdateRequestBody">Tagging object to be updated with required parameter (tag_guid)</param>
        /// <returns>ApiResponse of TaggingResponseBody</returns>
        public ApiResponse<TaggingResponseBody> UpdateTaggingWithHttpInfo(string taggingGuid, string userGuid, TaggingUpdateRequestBody taggingUpdateRequestBody)
        {
            // verify the required parameter 'taggingGuid' is set
            if (taggingGuid == null)
                throw new ApiException(400, "Missing required parameter 'taggingGuid' when calling MxPlatformApi->UpdateTagging");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTagging");

            // verify the required parameter 'taggingUpdateRequestBody' is set
            if (taggingUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'taggingUpdateRequestBody' when calling MxPlatformApi->UpdateTagging");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tagging_guid", ClientUtils.ParameterToString(taggingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = taggingUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<TaggingResponseBody>("/users/{user_guid}/taggings/{tagging_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update tagging Use this endpoint to update a tagging.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingUpdateRequestBody">Tagging object to be updated with required parameter (tag_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TaggingResponseBody</returns>
        public async System.Threading.Tasks.Task<TaggingResponseBody> UpdateTaggingAsync(string taggingGuid, string userGuid, TaggingUpdateRequestBody taggingUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TaggingResponseBody> localVarResponse = await UpdateTaggingWithHttpInfoAsync(taggingGuid, userGuid, taggingUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update tagging Use this endpoint to update a tagging.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="taggingGuid">The unique id for a &#x60;tagging&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="taggingUpdateRequestBody">Tagging object to be updated with required parameter (tag_guid)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TaggingResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TaggingResponseBody>> UpdateTaggingWithHttpInfoAsync(string taggingGuid, string userGuid, TaggingUpdateRequestBody taggingUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'taggingGuid' is set
            if (taggingGuid == null)
                throw new ApiException(400, "Missing required parameter 'taggingGuid' when calling MxPlatformApi->UpdateTagging");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTagging");

            // verify the required parameter 'taggingUpdateRequestBody' is set
            if (taggingUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'taggingUpdateRequestBody' when calling MxPlatformApi->UpdateTagging");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("tagging_guid", ClientUtils.ParameterToString(taggingGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = taggingUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<TaggingResponseBody>("/users/{user_guid}/taggings/{tagging_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTagging", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update transaction Use this endpoint to update the &#x60;description&#x60; of a specific transaction according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionUpdateRequestBody">Transaction object to be updated with a new description</param>
        /// <returns>TransactionResponseBody</returns>
        public TransactionResponseBody UpdateTransaction(string transactionGuid, string userGuid, TransactionUpdateRequestBody transactionUpdateRequestBody)
        {
            ApiResponse<TransactionResponseBody> localVarResponse = UpdateTransactionWithHttpInfo(transactionGuid, userGuid, transactionUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update transaction Use this endpoint to update the &#x60;description&#x60; of a specific transaction according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionUpdateRequestBody">Transaction object to be updated with a new description</param>
        /// <returns>ApiResponse of TransactionResponseBody</returns>
        public ApiResponse<TransactionResponseBody> UpdateTransactionWithHttpInfo(string transactionGuid, string userGuid, TransactionUpdateRequestBody transactionUpdateRequestBody)
        {
            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->UpdateTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTransaction");

            // verify the required parameter 'transactionUpdateRequestBody' is set
            if (transactionUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'transactionUpdateRequestBody' when calling MxPlatformApi->UpdateTransaction");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = transactionUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<TransactionResponseBody>("/users/{user_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update transaction Use this endpoint to update the &#x60;description&#x60; of a specific transaction according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionUpdateRequestBody">Transaction object to be updated with a new description</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionResponseBody> UpdateTransactionAsync(string transactionGuid, string userGuid, TransactionUpdateRequestBody transactionUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionResponseBody> localVarResponse = await UpdateTransactionWithHttpInfoAsync(transactionGuid, userGuid, transactionUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update transaction Use this endpoint to update the &#x60;description&#x60; of a specific transaction according to its unique GUID.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionGuid">The unique id for a &#x60;transaction&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionUpdateRequestBody">Transaction object to be updated with a new description</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionResponseBody>> UpdateTransactionWithHttpInfoAsync(string transactionGuid, string userGuid, TransactionUpdateRequestBody transactionUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'transactionGuid' is set
            if (transactionGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionGuid' when calling MxPlatformApi->UpdateTransaction");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTransaction");

            // verify the required parameter 'transactionUpdateRequestBody' is set
            if (transactionUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'transactionUpdateRequestBody' when calling MxPlatformApi->UpdateTransaction");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_guid", ClientUtils.ParameterToString(transactionGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = transactionUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<TransactionResponseBody>("/users/{user_guid}/transactions/{transaction_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTransaction", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update transaction_rule Use this endpoint to update the attributes of a specific transaction rule based on its unique GUID. The API will respond with the updated transaction_rule object. Any attributes not provided will be left unchanged.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleUpdateRequestBody">TransactionRule object to be updated</param>
        /// <returns>TransactionRuleResponseBody</returns>
        public TransactionRuleResponseBody UpdateTransactionRule(string transactionRuleGuid, string userGuid, TransactionRuleUpdateRequestBody transactionRuleUpdateRequestBody)
        {
            ApiResponse<TransactionRuleResponseBody> localVarResponse = UpdateTransactionRuleWithHttpInfo(transactionRuleGuid, userGuid, transactionRuleUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update transaction_rule Use this endpoint to update the attributes of a specific transaction rule based on its unique GUID. The API will respond with the updated transaction_rule object. Any attributes not provided will be left unchanged.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleUpdateRequestBody">TransactionRule object to be updated</param>
        /// <returns>ApiResponse of TransactionRuleResponseBody</returns>
        public ApiResponse<TransactionRuleResponseBody> UpdateTransactionRuleWithHttpInfo(string transactionRuleGuid, string userGuid, TransactionRuleUpdateRequestBody transactionRuleUpdateRequestBody)
        {
            // verify the required parameter 'transactionRuleGuid' is set
            if (transactionRuleGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleGuid' when calling MxPlatformApi->UpdateTransactionRule");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTransactionRule");

            // verify the required parameter 'transactionRuleUpdateRequestBody' is set
            if (transactionRuleUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleUpdateRequestBody' when calling MxPlatformApi->UpdateTransactionRule");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_rule_guid", ClientUtils.ParameterToString(transactionRuleGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = transactionRuleUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<TransactionRuleResponseBody>("/users/{user_guid}/transaction_rules/{transaction_rule_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update transaction_rule Use this endpoint to update the attributes of a specific transaction rule based on its unique GUID. The API will respond with the updated transaction_rule object. Any attributes not provided will be left unchanged.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleUpdateRequestBody">TransactionRule object to be updated</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TransactionRuleResponseBody</returns>
        public async System.Threading.Tasks.Task<TransactionRuleResponseBody> UpdateTransactionRuleAsync(string transactionRuleGuid, string userGuid, TransactionRuleUpdateRequestBody transactionRuleUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<TransactionRuleResponseBody> localVarResponse = await UpdateTransactionRuleWithHttpInfoAsync(transactionRuleGuid, userGuid, transactionRuleUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update transaction_rule Use this endpoint to update the attributes of a specific transaction rule based on its unique GUID. The API will respond with the updated transaction_rule object. Any attributes not provided will be left unchanged.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionRuleGuid">The unique id for a &#x60;transaction_rule&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="transactionRuleUpdateRequestBody">TransactionRule object to be updated</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TransactionRuleResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TransactionRuleResponseBody>> UpdateTransactionRuleWithHttpInfoAsync(string transactionRuleGuid, string userGuid, TransactionRuleUpdateRequestBody transactionRuleUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'transactionRuleGuid' is set
            if (transactionRuleGuid == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleGuid' when calling MxPlatformApi->UpdateTransactionRule");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateTransactionRule");

            // verify the required parameter 'transactionRuleUpdateRequestBody' is set
            if (transactionRuleUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'transactionRuleUpdateRequestBody' when calling MxPlatformApi->UpdateTransactionRule");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("transaction_rule_guid", ClientUtils.ParameterToString(transactionRuleGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = transactionRuleUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<TransactionRuleResponseBody>("/users/{user_guid}/transaction_rules/{transaction_rule_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateTransactionRule", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update user Use this endpoint to update the attributes of a specific user. The MX Platform API will respond with the updated user object. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill. To disable a user, update it and set the is_disabled parameter to true. Set it to false to re-enable the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="userUpdateRequestBody">User object to be updated (None of these parameters are required, but the user object cannot be empty.)</param>
        /// <returns>UserResponseBody</returns>
        public UserResponseBody UpdateUser(string userGuid, UserUpdateRequestBody userUpdateRequestBody)
        {
            ApiResponse<UserResponseBody> localVarResponse = UpdateUserWithHttpInfo(userGuid, userUpdateRequestBody);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update user Use this endpoint to update the attributes of a specific user. The MX Platform API will respond with the updated user object. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill. To disable a user, update it and set the is_disabled parameter to true. Set it to false to re-enable the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="userUpdateRequestBody">User object to be updated (None of these parameters are required, but the user object cannot be empty.)</param>
        /// <returns>ApiResponse of UserResponseBody</returns>
        public ApiResponse<UserResponseBody> UpdateUserWithHttpInfo(string userGuid, UserUpdateRequestBody userUpdateRequestBody)
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateUser");

            // verify the required parameter 'userUpdateRequestBody' is set
            if (userUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'userUpdateRequestBody' when calling MxPlatformApi->UpdateUser");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = userUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<UserResponseBody>("/users/{user_guid}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update user Use this endpoint to update the attributes of a specific user. The MX Platform API will respond with the updated user object. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill. To disable a user, update it and set the is_disabled parameter to true. Set it to false to re-enable the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="userUpdateRequestBody">User object to be updated (None of these parameters are required, but the user object cannot be empty.)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of UserResponseBody</returns>
        public async System.Threading.Tasks.Task<UserResponseBody> UpdateUserAsync(string userGuid, UserUpdateRequestBody userUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<UserResponseBody> localVarResponse = await UpdateUserWithHttpInfoAsync(userGuid, userUpdateRequestBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update user Use this endpoint to update the attributes of a specific user. The MX Platform API will respond with the updated user object. Disabling a user means that accounts and transactions associated with it will not be updated in the background by MX. It will also restrict access to that users data until they are no longer disabled. Users who are disabled for the entirety of an MX Platform API billing period will not be factored into that months bill. To disable a user, update it and set the is_disabled parameter to true. Set it to false to re-enable the user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="userUpdateRequestBody">User object to be updated (None of these parameters are required, but the user object cannot be empty.)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (UserResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<UserResponseBody>> UpdateUserWithHttpInfoAsync(string userGuid, UserUpdateRequestBody userUpdateRequestBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->UpdateUser");

            // verify the required parameter 'userUpdateRequestBody' is set
            if (userUpdateRequestBody == null)
                throw new ApiException(400, "Missing required parameter 'userUpdateRequestBody' when calling MxPlatformApi->UpdateUser");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter
            localVarRequestOptions.Data = userUpdateRequestBody;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<UserResponseBody>("/users/{user_guid}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Verify member The verify endpoint begins a verification process for a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>MemberResponseBody</returns>
        public MemberResponseBody VerifyMember(string memberGuid, string userGuid)
        {
            ApiResponse<MemberResponseBody> localVarResponse = VerifyMemberWithHttpInfo(memberGuid, userGuid);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Verify member The verify endpoint begins a verification process for a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <returns>ApiResponse of MemberResponseBody</returns>
        public ApiResponse<MemberResponseBody> VerifyMemberWithHttpInfo(string memberGuid, string userGuid)
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->VerifyMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->VerifyMember");

            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/verify", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("VerifyMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Verify member The verify endpoint begins a verification process for a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberResponseBody</returns>
        public async System.Threading.Tasks.Task<MemberResponseBody> VerifyMemberAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            ApiResponse<MemberResponseBody> localVarResponse = await VerifyMemberWithHttpInfoAsync(memberGuid, userGuid, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Verify member The verify endpoint begins a verification process for a member.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="memberGuid">The unique id for a &#x60;member&#x60;.</param>
        /// <param name="userGuid">The unique id for a &#x60;user&#x60;.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberResponseBody)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<MemberResponseBody>> VerifyMemberWithHttpInfoAsync(string memberGuid, string userGuid, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'memberGuid' is set
            if (memberGuid == null)
                throw new ApiException(400, "Missing required parameter 'memberGuid' when calling MxPlatformApi->VerifyMember");

            // verify the required parameter 'userGuid' is set
            if (userGuid == null)
                throw new ApiException(400, "Missing required parameter 'userGuid' when calling MxPlatformApi->VerifyMember");


            RequestOptions localVarRequestOptions = new RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/vnd.mx.api.v1+json"
            };


            var localVarContentType = ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("member_guid", ClientUtils.ParameterToString(memberGuid)); // path parameter
            localVarRequestOptions.PathParameters.Add("user_guid", ClientUtils.ParameterToString(userGuid)); // path parameter

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<MemberResponseBody>("/users/{user_guid}/members/{member_guid}/verify", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("VerifyMember", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
