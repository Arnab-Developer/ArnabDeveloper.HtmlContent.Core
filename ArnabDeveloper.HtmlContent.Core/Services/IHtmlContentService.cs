using ArnabDeveloper.HtmlContent.Core.Models;

namespace ArnabDeveloper.HtmlContent.Core.Services;

/// <summary>
/// Get html contents.
/// </summary>
public interface IHtmlContentService
{
    /// <summary>
    /// Collection of urls to get contents.
    /// </summary>
    IList<string> Urls { get; set; }

    /// <summary>
    /// Get html contents synchronously.
    /// </summary>
    /// <returns>Collection of WebSiteDataModel.</returns>
    /// <exception cref="System.ArgumentException">
    /// Throws ArgumentException if urls is empty.
    /// </exception>
    IEnumerable<WebSiteDataModel> GetContent();

    /// <summary>
    /// Get html contents asynchronously.
    /// </summary>
    /// <returns>A task having a collection of WebSiteDataModel.</returns>
    /// <exception cref="System.ArgumentException">
    /// Throws ArgumentException if urls is empty.
    /// </exception>
    Task<IEnumerable<WebSiteDataModel>> GetContentAsync();

    /// <summary>
    /// Get html contents asynchronously but start to return contents 
    /// as they are ready before all are complete.
    /// </summary>
    /// <returns>IAsyncEnumerable of ProgressDataModel.</returns>
    /// <exception cref="System.ArgumentException">
    /// Throws ArgumentException if urls is empty.
    /// </exception>
    IAsyncEnumerable<ProgressDataModel> GetContentAsyncStream();

    /// <summary>
    /// Get html contents asynchronously in parallel.
    /// </summary>
    /// <returns>Collection of WebSiteDataModel.</returns>
    /// <exception cref="System.ArgumentException">
    /// Throws ArgumentException if urls is empty.
    /// </exception>
    Task<IEnumerable<WebSiteDataModel>> GetContentParallelAsync();

    /// <summary>
    /// Get html contents asynchronously in parallel using 'Parallel.ForEach()'.
    /// </summary>
    /// <returns>Collection of WebSiteDataModel.</returns>
    /// <exception cref="System.ArgumentException">
    /// Throws ArgumentException if urls is empty.
    /// </exception>
    Task<IEnumerable<WebSiteDataModel>> GetContentParallelForEachAsync();

    /// <summary>
    /// Get html contents asynchronously in parallel using 'Parallel.ForEach()'
    /// and start to return contents as they are ready before all are complete
    /// with progress data.
    /// </summary>
    /// <returns>Collection of WebSiteDataModel.</returns>
    /// <exception cref="System.ArgumentException">
    /// Throws ArgumentException if urls is empty.
    /// </exception>
    Task<IEnumerable<WebSiteDataModel>> GetContentParallelForEachProgressAsync(
        IProgress<ProgressDataModel> progress);
}