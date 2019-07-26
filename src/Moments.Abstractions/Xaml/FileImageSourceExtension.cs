using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XmlnsDefinition("http://xamarin.com/schemas/2014/forms", "Moments.Xaml")]
namespace Moments.Xaml
{
    [AcceptEmptyServiceProvider]
    [ContentProperty(nameof(File))]
    public class FileImageSourceExtension : IMarkupExtension<ImageSource>
    {
        public string File { get; set; }

        public ImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            var imageSource = ImageSource.FromFile(File);
            return imageSource;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
}
