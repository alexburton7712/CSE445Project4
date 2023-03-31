using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Web.Services;

[WebService(Namespace = "http://yournamespace.com/")]
public class VerificationService : WebService
{

static void Main(string[] args) {  
    var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;  
    XmlSchemaSet schema = new XmlSchemaSet();  
    schema.Add("", path + "\\input.xsd");  
    XmlReader rd = XmlReader.Create(path + "\\input.xml");  
    XDocument doc = XDocument.Load(rd);  
    doc.Validate(schema, ValidationEventHandler);  
}  
static void ValidationEventHandler(object sender, ValidationEventArgs e) {  
    XmlSeverityType type = XmlSeverityType.Warning;  
    if (Enum.TryParse < XmlSeverityType > ("Error", out type)) {  
        if (type == XmlSeverityType.Error) throw new Exception(e.Message);  
    }  
} 



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
