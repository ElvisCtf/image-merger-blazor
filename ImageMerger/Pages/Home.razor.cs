using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace ImageMerger.Pages;

public partial class Home
{
    private string? _previewAUrl;
    private string? _previewBUrl;
    private bool IsMissingInputImages => string.IsNullOrEmpty(_previewAUrl) || string.IsNullOrEmpty(_previewBUrl);
    
    private string _orientation = "vertical";
    private string _alignment = "center";
    private int _spacing = 0;
    private string _background = "#ffffff";
    private bool _transparent = false;
    
    private bool _isMerging = false;
    private string? _mergedResult;

    
    private async Task<string> GetImageDataUrl(IBrowserFile file)
    {
        await using var stream = file.OpenReadStream(maxAllowedSize: 20 * 1024 * 1024);
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
        await Js.InvokeVoidAsync("imageMerge.clear");
    }

    private async Task SwapImage()
    {
        (_previewAUrl, _previewBUrl) = (_previewBUrl, _previewAUrl);
        await Js.InvokeVoidAsync("imageMerge.clear");
    }

    private async Task MergeImages()
    {
        if (string.IsNullOrEmpty(_previewAUrl) || string.IsNullOrEmpty(_previewBUrl)) return;
        _isMerging = true;
        _mergedResult = await Js.InvokeAsync<string>(
            "imageMerge.merge", 
            _previewAUrl, 
            _previewBUrl, 
            _orientation,
            _spacing, 
            _background, 
            _transparent,
            _alignment);
        _isMerging = false;
    }

    private async Task DownloadImage()
    {
        await Js.InvokeVoidAsync("imageMerge.download", "resultCanvas", "merged.png");
    }
}