using System;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;

namespace TwainScanApp
{
	public class Exif
	{
		public void AddExifData(Image image, string author, string title, string description)
		{
			// Tworzenie nowego PropertyItem za pomocą refleksji (brak publicznego konstruktora)
			PropertyItem propertyItem = (PropertyItem)typeof(PropertyItem)
				.GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
								null, new Type[0], null)
				?.Invoke(null);

			if (propertyItem == null)
			{
				MessageBox.Show("Nie udało się utworzyć PropertyItem");
				return;
			}

			// EXIF: Tag 0x010E = Image Title (ASCII encoded)
			propertyItem.Id = 0x010E;
			propertyItem.Type = 2; // ASCII string
			propertyItem.Value = System.Text.Encoding.ASCII.GetBytes(title + "\0"); // Dodaj null terminator
			propertyItem.Len = propertyItem.Value.Length;
			image.SetPropertyItem(propertyItem);

			// EXIF: Tag 0x0131 = Software (ASCII encoded)
			propertyItem.Id = 0x0131;
			propertyItem.Value = System.Text.Encoding.ASCII.GetBytes("TwainScanTest App\0");
			propertyItem.Len = propertyItem.Value.Length;
			image.SetPropertyItem(propertyItem);

			// EXIF: Tag 0x010F = Artist (ASCII encoded)
			propertyItem.Id = 0x010F;
			propertyItem.Value = System.Text.Encoding.ASCII.GetBytes(author + "\0");
			propertyItem.Len = propertyItem.Value.Length;
			image.SetPropertyItem(propertyItem);

			// EXIF: Tag 0x9286 = User Comment (ASCII encoded)
			propertyItem.Id = 0x9286;
			propertyItem.Value = System.Text.Encoding.ASCII.GetBytes(description + "\0");
			propertyItem.Len = propertyItem.Value.Length;
			image.SetPropertyItem(propertyItem);
		}
	}
}
