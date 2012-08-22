﻿using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MediaBrowser.Controller.Events;
using MediaBrowser.Controller.Xml;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Providers
{
    [Export(typeof(BaseMetadataProvider))]
    public class FolderProviderFromXml : BaseMetadataProvider
    {
        public override bool Supports(BaseEntity item)
        {
            return item is Folder;
        }

        public override MetadataProviderPriority Priority
        {
            get { return MetadataProviderPriority.First; }
        }

        public override Task FetchAsync(BaseEntity item, ItemResolveEventArgs args)
        {
            var metadataFile = args.GetFileSystemEntryByName("folder.xml");

            if (metadataFile.HasValue)
            {
                return Task.Run(() => { new FolderXmlParser().Fetch(item as Folder, metadataFile.Value.Path); });
            }

            return Task.FromResult<object>(null);
        }
    }
}
