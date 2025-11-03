using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.ComponentModel.Design.Serialization;
using System.Xml.Linq;

// Load the document using HTMLAgilityPack as normal


var html = new HtmlDocument();
html.LoadHtml(@"
  <html>
      <head></head>
      <body>
        <div>
          <p class='content'>Fizzler</p>
          <p>CSS Selector Engine</p></div>
      </body>
  </html>");

var webPage = new HtmlWeb().Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/laptops");

HtmlNode root = webPage.DocumentNode;

int rootLevel = 0;
string tagToFind = "li";
string idToFind = "#navbar-top";
string classToFind = ".ws-icon";

//DisplayTree(root, rootLevel);
//FindElementByTag(root, tagToFind);
//FindElementByID(root, idToFind);
FindElementByClass(root, classToFind);


void FindElementByTag(HtmlNode node, string tagToFind)
{
    if(node.NodeType == HtmlNodeType.Element && node.Name == tagToFind)
    {
        Console.WriteLine(node.InnerHtml.Trim());
    }
    foreach (var child in node.ChildNodes)
    {
        FindElementByTag(child, tagToFind);
    }
}

void FindElementByID (HtmlNode node, string idToFind)
{
    if (!idToFind.StartsWith("#")) throw new ArgumentException("Element id must begin with #");
    if (node.NodeType == HtmlNodeType.Element &&
        node.Attributes.Where(attr => attr.Name == "id" && attr.Value == idToFind[1..]).Count() > 0) 
    {
        Console.WriteLine(node.InnerHtml.Trim());
    }
    foreach (var child in node.ChildNodes)
    {
        FindElementByID(child, idToFind);
    }
}

void FindElementByClass(HtmlNode node, string classToFind)
{
    if (!classToFind.StartsWith(".")) throw new ArgumentException("Class selector must begin with '.'");
    if (node.NodeType == HtmlNodeType.Element &&
        node.Attributes.Contains("class") &&
        node.Attributes["class"].Value.Split(' ').Contains(classToFind[1..]))
    {
        Console.WriteLine(node.InnerHtml);
    }
    foreach (var child in node.ChildNodes)
    {
        FindElementByClass(child, classToFind);
    }
}

void DisplayTree(HtmlNode node, int level)
{
    if (node.NodeType != HtmlNodeType.Element && node.NodeType != HtmlNodeType.Document) return;
    Console.WriteLine(new String('\t',level) + node.Name);
    foreach (var child in node.ChildNodes)
    {
        DisplayTree(child, level + 1);
    }
}


/*
// Fizzler for HtmlAgilityPack is implemented as the
// QuerySelectorAll extension method on HtmlNode

var document = html.DocumentNode;

// yields: [<p class="content">Fizzler</p>]
document.QuerySelectorAll(".content");

// yields: [<p class="content">Fizzler</p>,<p>CSS Selector Engine</p>]
document.QuerySelectorAll("p");

// yields empty sequence
document.QuerySelectorAll("body>p");

// yields [<p class="content">Fizzler</p>,<p>CSS Selector Engine</p>]
document.QuerySelectorAll("body p");

// yields [<p class="content">Fizzler</p>]
document.QuerySelectorAll("p:first-child");
*/