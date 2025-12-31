using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace ImageMerger.Pages;

public partial class Home
{
    private string? _previewAUrl;
    private string? _previewBUrl;

    private async Task<string> GetImageDataUrl(IBrowserFile file)
    {
        using var stream = file.OpenReadStream(maxAllowedSize: 20 * 1024 * 1024);
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        return $"data:{file.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}";
    }

    private async Task HandleFileA(InputFileChangeEventArgs e)
    {
        _previewAUrl = await GetImageDataUrl(e.File);
    }

    private async Task HandleFileB(InputFileChangeEventArgs e)
    {
        _previewBUrl = await GetImageDataUrl(e.File);
    }

    private async Task ClearImages()
    {
        _previewAUrl = null;
        _previewBUrl = null;
        await Js.InvokeVoidAsync("fileReset.clear", "fileA");
        await Js.InvokeVoidAsync("fileReset.clear", "fileB");
    }
}