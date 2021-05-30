using ArnabDeveloper.AsyncAwaitDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ArnabDeveloper.AsyncAwaitDemo.Core.Services
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
            List<WebSiteDataModel> webSiteDataModels = new();
            foreach (string url in ((IHtmlContentService)this).Urls)
            {
                WebSiteDataModel webSiteDataModel = DownloadNormal(url);
                webSiteDataModels.Add(webSiteDataModel);
            }
            return webSiteDataModels;
        }

        /// <summary>
        /// Same time took as RunDownloadNormal but do not block the UI thread.
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentAsync()
        {
            List<WebSiteDataModel> webSiteDataModels = new();
            foreach (string url in ((IHtmlContentService)this).Urls)
            {
                WebSiteDataModel webSiteDataModel = await DownloadAsync(url);
                webSiteDataModels.Add(webSiteDataModel);
            }
            return webSiteDataModels;
        }

        /// <summary>
        /// Much faster than RunDownloadNormal and RunDownloadAsync because 
        /// works in parallel and do not block the UI thread.
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentParallelAsync()
        {
            List<Task<WebSiteDataModel>> tasks = new();
            foreach (string url in ((IHtmlContentService)this).Urls)
            {
                Task<WebSiteDataModel> t = DownloadAsync(url);
                tasks.Add(t);
            }
            WebSiteDataModel[] webSiteDataModels = await Task.WhenAll(tasks);
            return webSiteDataModels;
        }

        /// <summary>
        /// Do not block the UI thread because it is inside Task.Run() otherwise
        /// it block UI thread. Same fast as RunDownloadParallelAsync because it
        /// works in parallel. Async operation inside Parallel.ForEach() is not 
        /// wait the UI thread. So normal method needs to be used inside 
        /// Parallel.ForEach().
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentParallelAsyncV2()
        {
            List<WebSiteDataModel> webSiteDataModels = new();

            // Wait UI thread
            await Task.Run(() => Parallel.ForEach(((IHtmlContentService)this).Urls, url =>
            {
                WebSiteDataModel webSiteDataModel = DownloadNormal(url);
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
        /// Same as RunDownloadParallelV2Async() but with progress bar and it
        /// show result as they are available.
        /// </summary>
        async Task<IEnumerable<WebSiteDataModel>> IHtmlContentService.GetContentParallelAsyncV2WithProgress(
            IProgress<ProgressDataModel> progress)
        {
            List<WebSiteDataModel> webSiteDataModels = new();

            await Task.Run(() => Parallel.ForEach(((IHtmlContentService)this).Urls, url =>
            {
                WebSiteDataModel webSiteDataModel = DownloadNormal(url);
                webSiteDataModels.Add(webSiteDataModel);

                int progressPercentage = webSiteDataModels.Count * 100 / ((IHtmlContentService)this).Urls.Count;
                ProgressDataModel progressDataModel = new(progressPercentage, webSiteDataModel);
                progress.Report(progressDataModel);
            }));

            return webSiteDataModels;
        }

        private WebSiteDataModel DownloadNormal(string url)
        {
            using WebClient webClient = new();
            WebSiteDataModel webSiteDataModel = new(url, webClient.DownloadString(url));
            return webSiteDataModel;
        }

        private async Task<WebSiteDataModel> DownloadAsync(string url)
        {
            using WebClient webClient = new();
            WebSiteDataModel webSiteDataModel = new(url, await webClient.DownloadStringTaskAsync(url));
            return webSiteDataModel;
        }
    }
}
