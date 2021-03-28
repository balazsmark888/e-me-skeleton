using System;
using System.Collections.Generic;
using System.IO;
using Telerik.Documents.ImageUtils;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.InteractiveForms;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.Model.Editing;


namespace TelerikReporting
{
    public class Program
    {
        public static Client TestClient => new Client
        {
            BirthDay = "02",
            BirthMonth = "07",
            BirthPlace = "Gheorgheni, HR",
            BirthYear = "1999",
            FirstName = "Mark",
            FullAddress = "str.Orban Balazs, nr.77, cod 537250, Remetea, HR",
            LastName = "Balazs",
            Residence = "str.Teleorman, nr.77, cod 400573, Cluj Napoca, CJ"
        };

        public static Dictionary<string, string> ClientDictionary => TestClient.MapToDictionary();

        public static void Main(string[] args)
        {
            //Test1();
            Test2();
        }

        public static void Test1()
        {
            using var inputStream = File.OpenRead("Templates\\temp1.html");
            var htmlProvider = new HtmlFormatProvider();
            var document = htmlProvider.Import(inputStream);
            var flowEditor = new RadFlowDocumentEditor(document);
            flowEditor.ReplaceText("###NAME###", "Balazs Mark");
            flowEditor.ReplaceText("###DOB###", DateTime.Now.ToString("D"));
            using var outputStream = File.OpenWrite("test1.pdf");
            var pdfProvider = new PdfFormatProvider();
            pdfProvider.Export(document, outputStream);
        }

        public static void Test2()
        {
            using var inputStream = File.OpenRead("Templates\\temp4.pdf");
            var pdfProvider = new Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.PdfFormatProvider();
            var document = pdfProvider.Import(inputStream);
            FillFormFieldsOfDocument(document);
            using var outputStream = File.OpenWrite("test2.pdf");
            FixedExtensibilityManager.JpegImageConverter = new JpegImageConverter();
            pdfProvider.Export(document, outputStream);
        }

        public static void FillFormFieldsOfDocument(RadFixedDocument document)
        {
            foreach (var field in document.AcroForm.FormFields)
            {
                switch (field.FieldType)
                {
                    case FormFieldType.TextBox:
                        {
                            var textField = (TextBoxField)field;
                            if (!string.IsNullOrWhiteSpace(textField.Value))
                            {
                                if (ClientDictionary.ContainsKey(textField.Value))
                                {
                                    textField.Value = ClientDictionary[textField.Value];
                                }
                                else
                                {
                                    if (textField.Value.Equals("###DOCUMENT.DATE###"))
                                    {
                                        textField.Value = DateTime.Now.ToString("dd.MM.yyyy");
                                    }
                                }
                            }
                            field.IsReadOnly = true;
                            break;
                        }
                }
            }
        }
    }
}
