using System;
using System.Collections.Generic;
using System.IO;
using Telerik.Documents.ImageUtils;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.InteractiveForms;

namespace e_me.Core.Helpers
{
    public static class DocumentProcessing
    {
        public static void FillDocumentFormFieldsByFieldNames(RadFixedDocument document, Dictionary<string, string> data)
        {
            foreach (var field in document.AcroForm.FormFields)
            {
                switch (field.FieldType)
                {
                    case FormFieldType.CheckBox:
                        break;
                    case FormFieldType.TextBox:
                        {
                            var textField = (TextBoxField)field;
                            if (!string.IsNullOrWhiteSpace(textField.Name))
                            {
                                if (data.ContainsKey(textField.Name))
                                {
                                    textField.Value = data[textField.Name];
                                    textField.IsReadOnly = true;
                                }
                                else if (textField.Name.Equals("DOCUMENT_DATE"))
                                {
                                    textField.Value = DateTime.Now.ToString("dd/MM/yyyy");
                                    textField.IsReadOnly = true;
                                }
                            }
                            break;
                        }
                    case FormFieldType.PushButton:
                        break;
                    case FormFieldType.RadioButton:
                        break;
                    case FormFieldType.CombTextBox:
                        break;
                    case FormFieldType.ComboBox:
                        break;
                    case FormFieldType.ListBox:
                        break;
                    case FormFieldType.Signature:
                        break;
                }
            }
        }

        public static RadFixedDocument GetFixedDocumentFromBytes(byte[] bytes)
        {
            var pdfProvider = new PdfFormatProvider();
            var document = pdfProvider.Import(bytes);
            return document;
        }

        public static byte[] GetBytesFromFixedDocument(RadFixedDocument document)
        {
            FixedExtensibilityManager.JpegImageConverter = new JpegImageConverter();
            var pdfProvider = new PdfFormatProvider();
            var bytes = pdfProvider.Export(document);
            return bytes;
        }
    }
}
