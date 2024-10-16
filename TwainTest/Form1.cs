using System;
using System.Windows.Forms;
using TwainDotNet.WinFroms;
using TwainDotNet;
using System.Drawing.Imaging;
using System.Drawing;

namespace TwainScanApp
{
	public partial class Form1 : Form
	{
		private Twain _twain;
		private ScanSettings _settings;
		public Form1()
		{
			InitializeComponent();
			
			_twain = new Twain(new WinFormsWindowMessageHook(this));
			_twain.TransferImage += Twain_TransferImage;
			_twain.ScanningComplete += Twain_ScanningComplete;

			_settings = new ScanSettings
			{
				UseDocumentFeeder = false,  // Czy skanować z podajnika
				ShowTwainUI = true,         // Czy pokazać interfejs użytkownika skanera
				ShowProgressIndicatorUI = true,
				UseDuplex = false,
				Resolution = ResolutionSettings.Photocopier, // Możesz zmienić na lepszą jakość
				Area = null,
				ShouldTransferAllPages = false
			};

			panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom);
			pictureBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left);

			// Ustaw PictureBox w taki sposób, żeby nie kolidował z panelem
			pictureBox1.Left = panel1.Right;	// Lewa krawędź PictureBoxa powinna zaczynać się po prawej stronie panelu
			pictureBox1.Width = this.ClientSize.Width - panel1.Width;  // Dopasowanie szerokości PictureBoxa

		}

		private void btnScan_Click(object sender, EventArgs e)
		{
			_twain.StartScanning(_settings);
		}

		private void Twain_TransferImage(object sender, TwainDotNet.TransferImageEventArgs e)
		{
			if (e.Image != null)
			{
				pictureBox1.Image = e.Image; // Wyświetlanie zeskanowanego obrazu w PictureBox
			}
		}
		private void Twain_ScanningComplete(object sender, TwainDotNet.ScanningCompleteEventArgs e)
		{
			// Skanowanie zakończone
			MessageBox.Show("Skanowanie zakończone.");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Exif exif = new Exif();	
			if (pictureBox1.Image != null)
			{
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
					saveFileDialog.Title = "Zapisz zeskanowany obraz z EXIF";
					saveFileDialog.ShowDialog();

					if (!string.IsNullOrEmpty(saveFileDialog.FileName))
					{
						// Sprawdzenie formatu pliku
						ImageFormat format = saveFileDialog.FilterIndex == 1 ?
							ImageFormat.Jpeg : ImageFormat.Png;

						// Pracujemy tylko z obrazami JPEG, PNG nie wspiera EXIF
						if (format == ImageFormat.Jpeg)
						{
							// Tworzenie kopii obrazu, aby zapisać metadane EXIF
							using (Image imageWithExif = (Image)pictureBox1.Image.Clone())
							{
								// Dodaj dane EXIF
								exif.AddExifData(imageWithExif, "Zyberyusz Kowalsky", "Dokument 1", "Zeskanowany obraz");

								// Zapisz obraz z metadanymi EXIF
								imageWithExif.Save(saveFileDialog.FileName, format);
							}
						}
						else
						{
							// Zapis bez EXIF (PNG nie wspiera EXIF)
							pictureBox1.Image.Save(saveFileDialog.FileName, format);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Brak obrazu do zapisania.");
			}
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
		}

	}
}
