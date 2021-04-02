using System;
using System.Collections.Generic;
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
                                }
                                else if (textField.Name.Equals("DOCUMENT.DATE"))
                                {
                                    textField.Value = DateTime.Now.ToString("dd/MM/yyyy");
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
    }
}
