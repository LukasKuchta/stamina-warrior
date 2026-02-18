namespace MobileApp.Services;

/// <summary>
/// Modal Error Handler.
/// </summary>
public sealed class ModalErrorHandler : IErrorHandler, IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public void Dispose()
    {
        _semaphore.Dispose();
    }

    /// <summary>
    /// Handle error in UI.
    /// </summary>
    /// <param name="ex">Exception.</param>
    public void HandleError(Exception ex)
    {
        _ = DisplayAlertAsync(ex).FireAndForgetSafeAsync();
    }

    private async Task DisplayAlertAsync(Exception ex)
    {
        try
        {
            await _semaphore.WaitAsync();
            if (Shell.Current is Shell shell)
            {
                await shell.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
