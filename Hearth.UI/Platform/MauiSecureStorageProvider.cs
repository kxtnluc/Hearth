using Hearth.Services.Interfaces;

namespace Hearth.UI.Platform;

public class MauiSecureStorageProvider : ISecureStorageProvider
{
    public async Task<string?> GetAsync(string key)
    {
        try
        {
            return await SecureStorage.GetAsync(key);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"SecureStorage read failed: {ex}");
            return null;
        }
    }

    public async Task SetAsync(string key, string value)
    {
        await SecureStorage.SetAsync(key, value);
    }

    public void Remove(string key)
    {
        SecureStorage.Remove(key);
    }
}