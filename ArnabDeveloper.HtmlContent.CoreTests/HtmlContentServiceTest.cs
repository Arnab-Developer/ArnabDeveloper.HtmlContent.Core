using ArnabDeveloper.HtmlContent.Core.Models;
using ArnabDeveloper.HtmlContent.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ArnabDeveloper.HtmlContent.CoreTests
{
    public class HtmlContentServiceTest
    {
        private readonly IHtmlContentService _htmlContentService;

        public HtmlContentServiceTest()
        {
            _htmlContentService = new HtmlContentService();
        }

        [Fact]
        public void Can_GetContent_ReturnProperData()
        {
            AddUrls();
            IEnumerable<WebSiteDataModel> webSiteDataModels = _htmlContentService.GetContent();
            CheckResults(webSiteDataModels);
        }

        [Fact]
        public void Can_GetContent_ThrowExceptionIfUrlIsEmpty()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => _htmlContentService.GetContent());
            Assert.Equal("Url collection is empty", ex.Message);
        }

        [Fact]
        public async void Can_GetContentAsync_ReturnProperData()
        {
            AddUrls();
            IEnumerable<WebSiteDataModel> webSiteDataModels = await _htmlContentService.GetContentAsync();
            CheckResults(webSiteDataModels);
        }

        [Fact]
        public async void Can_GetContentAsync_ThrowExceptionIfUrlIsEmpty()
        {
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(() => _htmlContentService.GetContentAsync());
            Assert.Equal("Url collection is empty", ex.Message);
        }

        [Fact]
        public async void Can_GetContentAsyncStream_ReturnProperData()
        {
            AddUrls();
            await foreach (ProgressDataModel progressDataModel in
                _htmlContentService.GetContentAsyncStream())
            {
                CheckProgressResult(progressDataModel);
            }
        }

        [Fact]
        public async void Can_GetContentAsyncStream_ThrowExceptionIfUrlIsEmpty()
        {
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await foreach (ProgressDataModel progressDataModel in
                    _htmlContentService.GetContentAsyncStream())
                {
                    CheckProgressResult(progressDataModel);
                }
            });
            Assert.Equal("Url collection is empty", ex.Message);
        }

        [Fact]
        public async void Can_GetContentParallelAsync_ReturnProperData()
        {
            AddUrls();
            IEnumerable<WebSiteDataModel> webSiteDataModels = await _htmlContentService.GetContentParallelAsync();
            CheckResults(webSiteDataModels);
        }

        [Fact]
        public async void Can_GetContentParallelAsync_ThrowExceptionIfUrlIsEmpty()
        {
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(() => _htmlContentService.GetContentParallelAsync());
            Assert.Equal("Url collection is empty", ex.Message);
        }

        [Fact]
        public async void Can_GetContentParallelForEachAsync_ReturnProperData()
        {
            AddUrls();
            IEnumerable<WebSiteDataModel> webSiteDataModels = await _htmlContentService.GetContentParallelForEachAsync();
            CheckResults(webSiteDataModels);
        }

        [Fact]
        public async void Can_GetContentParallelForEachAsync_ThrowExceptionIfUrlIsEmpty()
        {
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(() => _htmlContentService.GetContentParallelForEachAsync());
            Assert.Equal("Url collection is empty", ex.Message);
        }

        [Fact]
        public async void Can_GetContentParallelForEachProgressAsync_ReturnProperData()
        {
            AddUrls();
            Progress<ProgressDataModel> progress = new(progressDataModel =>
            {
                CheckProgressResult(progressDataModel);
            });
            IEnumerable<WebSiteDataModel> webSiteDataModels =
                await _htmlContentService.GetContentParallelForEachProgressAsync(progress);
            CheckResults(webSiteDataModels);
        }

        [Fact]
        public async void Can_GetContentParallelForEachProgressAsync_ThrowExceptionIfUrlIsEmpty()
        {
            Progress<ProgressDataModel> progress = new();
            ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(
                () => _htmlContentService.GetContentParallelForEachProgressAsync(progress));
            Assert.Equal("Url collection is empty", ex.Message);
        }

        private void AddUrls()
        {
            _htmlContentService.Urls.Add("http://google.com");
            _htmlContentService.Urls.Add("http://microsoft.com");
            _htmlContentService.Urls.Add("http://github.com");
            _htmlContentService.Urls.Add("http://bitbucket.com");
            _htmlContentService.Urls.Add("http://gmail.com");
            _htmlContentService.Urls.Add("http://office.com");
            _htmlContentService.Urls.Add("http://outlook.com");
            _htmlContentService.Urls.Add("http://www.businessinsider.com");
        }

        private void CheckResults(IEnumerable<WebSiteDataModel> webSiteDataModels)
        {
            Assert.Equal(8, webSiteDataModels.Count());
            foreach (WebSiteDataModel webSiteDataModel in webSiteDataModels)
            {
                Assert.NotNull(webSiteDataModel.WebsiteUrl);
                Assert.True
                (
                    webSiteDataModel.WebsiteUrl == "http://google.com" ||
                    webSiteDataModel.WebsiteUrl == "http://microsoft.com" ||
                    webSiteDataModel.WebsiteUrl == "http://github.com" ||
                    webSiteDataModel.WebsiteUrl == "http://bitbucket.com" ||
                    webSiteDataModel.WebsiteUrl == "http://gmail.com" ||
                    webSiteDataModel.WebsiteUrl == "http://office.com" ||
                    webSiteDataModel.WebsiteUrl == "http://outlook.com" ||
                    webSiteDataModel.WebsiteUrl == "http://www.businessinsider.com"
                );
                Assert.NotNull(webSiteDataModel.WebsiteData);
                Assert.True(webSiteDataModel.WebsiteData.Length != 0);
            }
        }

        private void CheckProgressResult(ProgressDataModel progressDataModel)
        {
            Assert.True(progressDataModel.ProgressValue != 0);
            Assert.NotNull(progressDataModel.Data);
            Assert.True(progressDataModel.Data!.WebsiteData.Length != 0);
            Assert.NotNull(progressDataModel.Data!.WebsiteUrl);
            Assert.True
            (
                progressDataModel.Data!.WebsiteUrl == "http://google.com" ||
                progressDataModel.Data!.WebsiteUrl == "http://microsoft.com" ||
                progressDataModel.Data!.WebsiteUrl == "http://github.com" ||
                progressDataModel.Data!.WebsiteUrl == "http://bitbucket.com" ||
                progressDataModel.Data!.WebsiteUrl == "http://gmail.com" ||
                progressDataModel.Data!.WebsiteUrl == "http://office.com" ||
                progressDataModel.Data!.WebsiteUrl == "http://outlook.com" ||
                progressDataModel.Data!.WebsiteUrl == "http://www.businessinsider.com"
            );
            Assert.NotNull(progressDataModel.Data!.WebsiteData);
            Assert.True(progressDataModel.Data!.WebsiteData.Length != 0);
        }
    }
}
