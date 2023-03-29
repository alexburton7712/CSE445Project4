using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Web.Services;

[WebService(Namespace = "http://yournamespace.com/")]
public class VerificationService : WebService
{
    [WebMethod]
    public string Verification(string xmlUrl, string xsdUrl)
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

        XmlSchema schema;
        using (XmlReader schemaReader = XmlReader.Create(xsdUrl))
        {
            schema = XmlSchema.Read(schemaReader, ValidationCallback);
        }

        settings.Schemas.Add(schema);

        string result = "No Error";

        using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
        {
            while (reader.Read()) { }
        }

        return result;
    }

    private void ValidationCallback(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error)
        {
            throw new XmlException($"{e.Message} ({e.LineNumber}:{e.LinePosition})");
        }
    }
}