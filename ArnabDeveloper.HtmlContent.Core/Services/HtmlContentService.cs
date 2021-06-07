using ArnabDeveloper.HtmlContent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ArnabDeveloper.HtmlContent.Core.Services
{
    public class HtmlContentService : IHtmlContentService
    {
        private IList<string> _urls;
        IList<string> IHtmlContentService.Urls
        {
            get => _urls;
            set => _urls = value;
        }

        public HtmlContentService()
        {
            _urls = new List<string>();
        }

        /// <summary>
        /// Blocks the UI thread.
        /// </summary>
        IEnumerable<WebSiteDataModel> IHtmlContentService.GetContent()
        {
            if (!((IHtmlContentService)this).Urls.Any())
            {
                throw new ArgumentException("Url collection is empty");
            }

            List<WebSiteDataModel> webSiteDataModels = new();
            foreach (string url in ((IHtmlContentService)this).Urls)
            {
                WebSiteDataModel webSiteDataModel = DownloadString(url);
                webSiteDataModels.Add(webSiteDataModel);
            }
            return webSiteDataModels;
        }

        /// <summary>
        /// Same time took as GetContent() but do not block the UI thread.
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentAsync()
        {
            if (!((IHtmlContentService)this).Urls.Any())
            {
                throw new ArgumentException("Url collection is empty");
            }

            List<WebSiteDataModel> webSiteDataModels = new();
            foreach (string url in ((IHtmlContentService)this).Urls)
            {
                WebSiteDataModel webSiteDataModel = await DownloadStringTaskAsync(url);
                webSiteDataModels.Add(webSiteDataModel);
            }
            return webSiteDataModels;
        }

        /// <summary>
        /// Use of async stream.
        /// </summary>
        async IAsyncEnumerable<ProgressDataModel> IHtmlContentService.GetContentAsyncStream()
        {
            if (!((IHtmlContentService)this).Urls.Any())
            {
                throw new ArgumentException("Url collection is empty");
            }

            List<WebSiteDataModel> webSiteDataModels = new();
            foreach (string url in ((IHtmlContentService)this).Urls)
            {
                WebSiteDataModel webSiteDataModel = await DownloadStringTaskAsync(url);
                webSiteDataModels.Add(webSiteDataModel);

                int progressPercentage = webSiteDataModels.Count * 100 / ((IHtmlContentService)this).Urls.Count;
                ProgressDataModel progressDataModel = new(progressPercentage, webSiteDataModel);

                yield return progressDataModel;
            }
        }

        /// <summary>
        /// Much faster than GetContent() and GetContentAsync() because 
        /// works in parallel and do not block the UI thread.
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentParallelAsync()
        {
            if (!((IHtmlContentService)this).Urls.Any())
            {
                throw new ArgumentException("Url collection is empty");
            }

            List<Task<WebSiteDataModel>> tasks = new();
            foreach (string url in ((IHtmlContentService)this).Urls)
            {
                Task<WebSiteDataModel> t = DownloadStringTaskAsync(url);
                tasks.Add(t);
            }
            WebSiteDataModel[] webSiteDataModels = await Task.WhenAll(tasks);
            return webSiteDataModels;
        }

        /// <summary>
        /// Do not block the UI thread because it is inside Task.Run() otherwise
        /// it block UI thread. Same fast as GetContentParallelAsync() because it
        /// works in parallel. Async operation inside Parallel.ForEach() is not 
        /// wait the UI thread. So normal method needs to be used inside 
        /// Parallel.ForEach().
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentParallelForEachAsync()
        {
            if (!((IHtmlContentService)this).Urls.Any())
            {
                throw new ArgumentException("Url collection is empty");
            }

            List<WebSiteDataModel> webSiteDataModels = new();

            // Wait UI thread
            await Task.Run(() => Parallel.ForEach(((IHtmlContentService)this).Urls, url =>
            {
                WebSiteDataModel webSiteDataModel = DownloadString(url);
                webSiteDataModels.Add(webSiteDataModel);
            }));

            // Do not wait UI thread            
            //await Task.Run(() => Parallel.ForEach(((IDemoMethods)this).Urls, async url =>
            //{
            //    WebSiteDataModel webSiteDataModel = await DownloadAsync(url);
            //    webSiteDataModels.Add(webSiteDataModel);
            //}));

            return webSiteDataModels;
        }

        /// <summary>
        /// Same as GetContentParallelAsyncV2() but with progress notification.
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentParallelForEachProgressAsync(
            IProgress<ProgressDataModel> progress)
        {
            if (!((IHtmlContentService)this).Urls.Any())
            {
                throw new ArgumentException("Url collection is empty");
            }

            List<WebSiteDataModel> webSiteDataModels = new();

            await Task.Run(() => Parallel.ForEach(((IHtmlContentService)this).Urls, url =>
            {
                WebSiteDataModel webSiteDataModel = DownloadString(url);
                webSiteDataModels.Add(webSiteDataModel);

                int progressPercentage = webSiteDataModels.Count * 100 / ((IHtmlContentService)this).Urls.Count;
                ProgressDataModel progressDataModel = new(progressPercentage, webSiteDataModel);
                progress.Report(progressDataModel);
            }));

            return webSiteDataModels;
        }

        private WebSiteDataModel DownloadString(string url)
        {
            using WebClient webClient = new();
            WebSiteDataModel webSiteDataModel = new(url, webClient.DownloadString(url));
            return webSiteDataModel;
        }

        private async Task<WebSiteDataModel> DownloadStringTaskAsync(string url)
        {
            using WebClient webClient = new();
            WebSiteDataModel webSiteDataModel = new(url, await webClient.DownloadStringTaskAsync(url));
            return webSiteDataModel;
        }
    }
}
