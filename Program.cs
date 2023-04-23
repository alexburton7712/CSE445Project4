using System.Xml;

/*
https://we.tl/t-zUqL0eahsW

https://we.tl/t-UgfgvoKIR3

protected void Page_Load(object sender, EventArgs e)
        {
            // check for cookies 
            if (Request.Cookies["ssncookie"] != null)
            {
                Console.WriteLine("cookies");
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
*/

// Load the existing XML file
XmlDocument doc = new XmlDocument();
doc.Load("existingFile.xml");

// Create a new element with the correct namespace
XmlElement newElement = doc.CreateElement("Cruise", "http://yournamespace.com/");

// Create child elements with their values
XmlElement nameElement = doc.CreateElement("Name");
nameElement.InnerText = "Example Cruise";
newElement.AppendChild(nameElement);

XmlElement parentCompanyElement = doc.CreateElement("Parent_Company");
parentCompanyElement.InnerText = "Example Company";
newElement.AppendChild(parentCompanyElement);

XmlElement foundedInElement = doc.CreateElement("FoundedIn");
foundedInElement.InnerText = "2000";
newElement.AppendChild(foundedInElement);

XmlElement departurePortsElement = doc.CreateElement("Departure_Ports");
departurePortsElement.InnerText = "Port A, Port B";
newElement.AppendChild(departurePortsElement);

XmlElement reservationElement = doc.CreateElement("Reservation");
XmlElement phoneElement = doc.CreateElement("Phone");
phoneElement.InnerText = "555-1234";
reservationElement.AppendChild(phoneElement);
XmlElement urlElement = doc.CreateElement("Url");
urlElement.InnerText = "http://www.example.com";
reservationElement.AppendChild(urlElement);
newElement.AppendChild(reservationElement);

XmlElement headquarterElement = doc.CreateElement("Headquarter");
XmlElement cityElement = doc.CreateElement("City");
cityElement.InnerText = "Example City";
headquarterElement.AppendChild(cityElement);
XmlElement stateElement = doc.CreateElement("State");
stateElement.InnerText = "Example State";
headquarterElement.AppendChild(stateElement);
XmlElement zipElement = doc.CreateElement("Zip");
zipElement.InnerText = "12345";
headquarterElement.AppendChild(zipElement);
newElement.AppendChild(headquarterElement);

// Add the new element to the document's root element
XmlElement rootElement = doc.DocumentElement;
rootElement.AppendChild(newElement);

// Save the modified document back to the file
doc.Save("existingFile.xml");

