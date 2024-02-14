mergeInto(LibraryManager.library, {
    EmbedImage: function(url) {
        if (window.CreateEmbeddedURL) {
            window.CreateEmbeddedURL(url);
        }
    },
    ClipboardWriter: function(newClipText) {
        window.open(newClipText, '_blank');
    } 
});