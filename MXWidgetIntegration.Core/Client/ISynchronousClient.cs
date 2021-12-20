namespace MXWidgetIntegration.Core.Client
{
    public interface ISynchronousClient
    {
        ApiResponse<T> Get<T>(string path, RequestOptions options, IReadableConfiguration configuration = null);
        ApiResponse<T> Post<T>(string path, RequestOptions options, IReadableConfiguration configuration = null);
        ApiResponse<T> Put<T>(string path, RequestOptions options, IReadableConfiguration configuration = null);
        ApiResponse<T> Delete<T>(string path, RequestOptions options, IReadableConfiguration configuration = null);
        ApiResponse<T> Head<T>(string path, RequestOptions options, IReadableConfiguration configuration = null);
        ApiResponse<T> Options<T>(string path, RequestOptions options, IReadableConfiguration configuration = null);
        ApiResponse<T> Patch<T>(string path, RequestOptions options, IReadableConfiguration configuration = null);
    }
}