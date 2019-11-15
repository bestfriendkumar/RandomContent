using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Options;
using RandomContent.Entities;
using RandomContent.Helpers;

namespace RandomContent.Services
{
    public interface IContentService
    {
        Content GetRandomDog();
        Content GetPerfectDog();
    }

    public class ContentService : IContentService
    {

        private readonly AppSettings _appSettings;

        public ContentService(IOptions<AppSettings> appSettings)
        {
            this._random = new Random();
            _appSettings = appSettings.Value;
        }

        public Content GetRandomDog()
        {
            return GetRandomContent();
        }

        public Content GetPerfectDog()
        {
            return GetSpecificContent();
        }

        private static readonly IList<Content> ContentList = new ReadOnlyCollection<Content>
        (new List<Content> {
            new Content(){ContentUrl = "https://i.imgur.com/2OeV9bF.png"},
            new Content(){ContentUrl = "https://i.imgur.com/gwhV4yx.png"},
            new Content(){ContentUrl = "https://i.imgur.com/Kvcks85.png"},
            new Content(){ContentUrl = "https://i.imgur.com/hsndY1D.png"},
            new Content(){ContentUrl = "https://i.imgur.com/2dhxgBu.png"},
            new Content(){ContentUrl = "https://i.imgur.com/o2bvDJJ.png"},
            new Content(){ContentUrl = "https://i.imgur.com/VDzXArT.png"},
            new Content(){ContentUrl = "https://i.imgur.com/jIdjQSF.png"},
            new Content(){ContentUrl = "https://i.imgur.com/frjluEY.png"},
            new Content(){ContentUrl = "https://i.imgur.com/kQ7y4Ya.png"},
            new Content(){ContentUrl = "https://i.imgur.com/j54lNWA.png"},
            new Content(){ContentUrl = "https://i.imgur.com/Jdcj0vu.png"}
        });

        private readonly Random _random;

        /// <summary>
        /// Retrieves random element of content list
        /// </summary>
        /// <returns></returns>
        private Content GetRandomContent()
        {
            var index = _random.Next(ContentList.Count);
            return ContentList[index];
        }

        /// <summary>
        /// Returns a specific prebuilt content object
        /// </summary>
        /// <returns></returns>
        private Content GetSpecificContent()
        {
            return new Content()
            {
                ContentUrl = "https://i.imgur.com/ZLlirKT.jpg"
            };
        }
    }
}